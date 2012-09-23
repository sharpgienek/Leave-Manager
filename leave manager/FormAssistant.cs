using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using leave_manager.Exceptions;
namespace leave_manager
{
    /// <summary>
    /// Klasa formularza użytkownika z uprawnieniamy rejestratorki.
    /// </summary>
    public partial class FormAssistant : LeaveManagerForm
    {
        /// <summary>
        /// Konstruktor bezargumentowy.
        /// </summary>
        public FormAssistant()
        { }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connection">Połączenie do bazy danych. Powinno być otwarte.</param>
        public FormAssistant(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            //Wczytanie danych do tabeli ze zgłoszeniami wymagającymi działania.
            RefreshDataGridViewPendingAplications();
            //Wczytanie wszystkich pracowników do tabeli z pracownikami.
            LoadAllDataGridViewEmployees();
            LoadAllPositionComboBox();
            RefreshReplacments();
        }

        /// <summary>
        /// Metoda stworzona do obsługi zdarzeń, odświerza tabliće zastępstw
        /// </summary>
        /// <param name="o">Obiekt zgłaszający zdarzenie</param>
        /// <param name="e">Argumenty</param>
        private void RefreshReplacments(object o, FormClosedEventArgs e)
        {
            RefreshReplacments();
        }

        /// <summary>
        /// Odświerza tablicę zastępstw
        /// </summary>
        private void RefreshReplacments()
        {
            DataTable leaves = this.GetLeavesForFuture();
            DateTime dateStart, dateEnd, previousEnd = new DateTime();
            dataGridViewReplacements.Rows.Clear();
            string positionDesc = (string)comboBoxReplacementsPosition.SelectedValue;
            bool filter = false;
            int positionFilter = -1;
            if (positionDesc != null && positionDesc != "")
            {
                filter = true;
                positionFilter = this.GetPositionID(positionDesc);
            }
            foreach (DataRow row in leaves.Rows)
            {
                dateStart = (DateTime)row["First_day"];
                dateEnd = (DateTime)row["Last_day"];
                if (dateStart < previousEnd)
                    dateStart = previousEnd.AddDays(1);
                int positionId = (int) row["Position_ID"];
                if (filter)
                    if (positionId != positionFilter)
                        continue;
                if (dateStart < this.GetServerTimeNow())
                    dateStart = this.GetServerTimeNow();
                while(dateStart <= dateEnd)
                {
                    if (!IsHoliday(dateStart))
                    {
                        int availableWorkers = this.GetAvailableWorkerCount(positionId, dateStart);
                        int neededWorkers = this.GetNeededEmployeesNo(dateStart);
                        if (availableWorkers < neededWorkers)
                        {
                            dataGridViewReplacements.Rows.Add(dateStart.Date, neededWorkers, availableWorkers, this.GetPositionDescription(positionId));
                        }
                    }
                    dateStart = dateStart.AddDays(1);
                }
                previousEnd = dateEnd;
            }
        }

        /// <summary>
        /// Metoda sprawdza czy podana data jest świętem.
        /// </summary>
        /// <param name="date">Data do sprawdzenia</param>
        /// <returns>Prawdę jeśli w danym dniu jest święto lub fałsz jeśli nie</returns>
        private bool IsHoliday(DateTime date)
        {
            List<DateTime> holidays;
            try
            {
                holidays = this.GetPublicHolidays();
            }
            catch (Exception)
            {
                return false;
            }
            foreach (DateTime tmp in holidays)
            {
                if (tmp == date)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Metoda odpowiedzialna za wczytanie z bazy danych informacji o możliwych pozycjach pracowników i umieszczenie ich w odpowiednim
        /// combo boxie.
        /// </summary>
        private void LoadAllPositionComboBox()
        {
            List<String> positions;
            try
            {
                positions =  this.GetPositionsList();
                positions.Insert(0, "");
                comboBoxEmployeesPosition.DataSource = positions;
                comboBoxReplacementsPosition.DataSource = positions;
            }
            catch (SqlException)
            {
                MessageBox.Show("SQL Error. This form will be close. Please try again later.");
                this.Close();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Invalid operation. This form will be close. Please try again later");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown exception has occured" + ex.Message);
            }

        }
        /// <summary>
        /// Metoda odpowiedzialna za odświeżenie tabeli ze zgłoszeniami wymagającymi
        /// działania. Korzysta z metody bezparametrowej o tej samej nazwie.
        /// Stworzona po to, aby można było ją podpiąć pod obsługę zdarzenia.
        /// </summary>
        /// <param name="o">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void RefreshDataGridViewPendingAplications(object o, FormClosedEventArgs e)
        {
            RefreshDataGridViewPendingAplications();
        }

        /// <summary>
        /// Metoda odpowiedzialna za odświeżenie tabeli ze zgłoszeniami wymagającymi
        /// działania.
        /// </summary>
        private void RefreshDataGridViewPendingAplications()
        {
            try
            {                
                dataGridViewPendingAplications.DataSource = this.GetNeedsAction();
            }
            catch (SqlException)
            {
                MessageBox.Show("SQL Error. Please try again later.");
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Invalid operation. Please try again later");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown exception has occured" + ex.Message);
            }

        }

        /// <summary>
        /// Metoda odpowiedzialna za wczytanie danych do tabeli zawierającej
        /// informacje o pracownikach.
        /// </summary>
        private void LoadAllDataGridViewEmployees()
        {
            try
            {
                dataGridViewEmployees.DataSource = this.GetEmployees();
                dataGridViewEmployees.Columns["Employee id"].Visible = false;
            }
            catch (SqlException)
            {
                MessageBox.Show("SQL Error. This form will be close. Please try again later.");
                this.Close();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Invalid operation. This form will be close. Please try again later");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown exception has occured" + ex.Message);
            }
        }        

        /// <summary>
        /// Metoda obsługująca przyciśnięcie guzika rozważenia zaznaczonych 
        /// aplikacji o urlop.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonConsiderPendingAplication_Click(object sender, EventArgs e)
        {
            //Dla każdej zaznaczonej komórki zaznaczamy cały wiersz.
            foreach (DataGridViewCell cell in dataGridViewPendingAplications.SelectedCells)
            {
                if (cell.Value != null)
                    dataGridViewPendingAplications.Rows[cell.RowIndex].Selected = true;
            }
            //Dla każdego zaznaczonego wiersza.
            foreach (DataGridViewRow row in dataGridViewPendingAplications.SelectedRows)
            {
                //Tworzymy formularz rozważenia aplikacji.
                FormLeaveConsideration form = new FormLeaveConsideration(this, connection,
                    new Leave((int)row.Cells["Leave Id"].Value, (int)row.Cells["Employee Id"].Value, row.Cells["Type"].Value.ToString(), row.Cells["Status"].Value.ToString(),
                        (DateTime)row.Cells["First day"].Value, (DateTime)row.Cells["Last day"].Value, row.Cells["Remarks"].Value.ToString(), (int)row.Cells["Used days"].Value));
                /* Dodajemy metodę odświeżania oczekujących aplikacji do obsługi zdarzenia zamknięcia
                 * formularza rozważania aplikacji.
                 */
                form.FormClosed += new FormClosedEventHandler(RefreshDataGridViewPendingAplications);
                //Wyświetlenie formularza rozważenia aplikacji.
                form.Show();
            }
        }

        /// <summary>
        /// Metoda obsługi wciśnięcia guzika pokazania wszystkich pracowników.
        /// W tabeli pracowników wyświetla aktualne dane o znajdujących 
        /// się w bazie danych pracownikach.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonEmployeesShowAll_Click(object sender, EventArgs e)
        {
            LoadAllDataGridViewEmployees();
        }

        /// <summary>
        /// Metoda obsługi wciśnięcia guzika wyświtlenia szczegółowych danych 
        /// o zaznaczonych pracownikach.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonEmployeesDetailedData_Click(object sender, EventArgs e)
        {
            //Dla każdej zaznaczonej komórki zaznaczamy jej wiersz.
            foreach (DataGridViewCell cell in dataGridViewEmployees.SelectedCells)
            {
                if (cell.Value != null)
                    dataGridViewEmployees.Rows[cell.RowIndex].Selected = true;
            }
            //Dla każdego zaznaczonego wiersza.
            foreach (DataGridViewRow row in dataGridViewEmployees.SelectedRows)
            {
                //Tworzymy formularz danych pracownika.
                FormEmployeeData form = new FormEmployeeData(this, connection, (int)row.Cells["Employee id"].Value,
                    row.Cells["Name"].Value.ToString(), row.Cells["Surname"].Value.ToString(),
                    row.Cells["Position"].Value.ToString());
                /* Dodana zostaje metoda odświeżania tabeli oczekujących aplikacji urlopowych do obsługi
                 * zdarzenia zamknięcia formularza. Powodem tego jest umożliwienie w formularzu danych 
                 * pracownika zmiany właściwości jego aplikacji urlopowych.
                 */
                form.FormClosed += new FormClosedEventHandler(RefreshDataGridViewPendingAplications);
                //Wyświetlenie formularza danych pracownika.
                form.Show();
            }           
        }

        private void buttonRejectPendingAplication_Click(object sender, EventArgs e)
        {
            //Dla każdej zaznaczonej komórki zaznaczamy jej wiersz.
            foreach (DataGridViewCell cell in  dataGridViewPendingAplications.SelectedCells)
            {
                if (cell.Value != null)
                    dataGridViewPendingAplications.Rows[cell.RowIndex].Selected = true;
            }
            //Dla każdego zaznaczonego wiersza.
            foreach (DataGridViewRow row in dataGridViewPendingAplications.SelectedRows)
            {
                try
                {
                    this.RejectLeave((int)row.Cells["Leave id"].Value);
                    RefreshDataGridViewPendingAplications();
                }
                catch (SqlException)
                {
                    MessageBox.Show("SQL error. Please try connection to database or try again later");
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Invalid operation. Please try again later.");
                }
                catch (IsolationLevelException)
                {
                    MessageBox.Show("Isolation level error. Please try again later or contact administrator");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unknown exception has occured" + ex.Message);
                }
            }      
        }

        /// <summary>
        /// Metoda obsługujące naciśnięcie przycisku Search (wyszukuje pracownika) 
        /// </summary>
        /// <param name="sender">Obiekt wysyłający</param>
        /// <param name="e">Argumenty</param>
        private void buttonEmployeesSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewEmployees.DataSource = this.SearchEmployee(textBoxEmployeesName.Text,
                    textBoxEmployeesSurname.Text, "", comboBoxEmployeesPosition.SelectedItem.ToString());
                dataGridViewEmployees.Columns["Employee id"].Visible = false;
            }
            catch (SqlException)
            {
                MessageBox.Show("SQL error. Please try connection to database or try again later");
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Invalid operation. Please try again later.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown exception has occured" + ex.Message);
            }
        }

        private void buttonReplacementsManage_Click(object sender, EventArgs e)
        {
            //Dla każdej zaznaczonej komórki zaznaczamy jej wiersz.
            foreach (DataGridViewCell cell in dataGridViewReplacements.SelectedCells)
            {
                if (cell.Value != null)
                    dataGridViewReplacements.Rows[cell.RowIndex].Selected = true;
            }
            //Dla każdego zaznaczonego wiersza.
            foreach (DataGridViewRow row in dataGridViewReplacements.SelectedRows)
            {
                //Tworzymy formularz danych pracownika.
                FormReplacement form = new FormReplacement((string) row.Cells["Position"].Value, (DateTime) row.Cells["Date"].Value, connection);
                /* Dodana zostaje metoda odświeżania tabeli oczekujących aplikacji urlopowych do obsługi
                 * zdarzenia zamknięcia formularza. Powodem tego jest umożliwienie w formularzu danych 
                 * pracownika zmiany właściwości jego aplikacji urlopowych.
                 */
                form.FormClosed += new FormClosedEventHandler(RefreshReplacments);
                //Wyświetlenie formularza danych pracownika.
                form.Show();
            }
        }

        /// <summary>
        /// Metoda obsługi zdarzenia zmiany zaznaczenia zakładki. W razie potrzeby
        /// odświeża odpowiednie dane.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Jeżeli zaznaczono zakładkę pracowników to odśwież tabele pracowników.
            if (tabControl.SelectedTab.Text.Equals("Employees"))
                LoadAllDataGridViewEmployees();
            else
                //Jeżeli zaznaczono zakładkę ze zgłoszeniami wymagającymi działania, to odśwież tam dane.
                if (tabControl.SelectedTab.Text.Equals("Needs your action"))
                    RefreshDataGridViewPendingAplications();
                else
                    if (tabControl.SelectedTab.Text.Equals("Replacments"))
                        RefreshReplacments();
        }

        private void comboBoxReplacementsPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshReplacments();
        }
    }
}
