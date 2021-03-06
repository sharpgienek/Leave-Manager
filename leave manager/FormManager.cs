﻿using System;
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
    public partial class FormManager : LeaveManagerForm
    {
        /// <summary>
        /// Konstruktor bezargumentowy. 
        /// </summary>
        public FormManager() { }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connection">Połączenie z bazą danych. Powinno być otwarte.</param>
        public FormManager(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            comboBoxContentSelection.SelectedIndex = 0;
            //Pierwsze uzupełnienie tabeli zawierającej zgłoszenia wymagające rozpatrzenia przez kierownika.
            LoadDataGridViewNeedsAction();
            LoadAllPositionComboBox();
            RefreshReplacments();
        }

        private void LoadAllPositionComboBox()
        {
            List<String> positions;
            try
            {
                positions = this.GetPositionsList();
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
                this.Close();
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
                int positionId = (int)row["Position_ID"];
                if (filter)
                    if (positionId != positionFilter)
                        continue;
                if (dateStart < this.GetServerTimeNow())
                    dateStart = this.GetServerTimeNow();
                while (dateStart <= dateEnd)
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
        /// Metoda wczytywania zawartości tabeli zawierającej zgłoszenia wymagające rozpatrzenia przez kierownika.
        /// Wywołuje bezparametrową metodę o tej samej nazwie. Stworzona celem umożliwienia podpięcia jej
        /// do obsługi zdarzenia zamknięcia formularza.
        /// </summary>
        /// <param name="o">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void LoadDataGridViewNeedsAction(object o, FormClosedEventArgs e)
        {
            LoadDataGridViewNeedsAction();
        }

        /// <summary>
        /// Metoda wczytywania zawartości tabeli zawierającej zgłoszenia wymagające rozpatrzenia przez kierownika.
        /// </summary>
        private void LoadDataGridViewNeedsAction()
        {
            try
            {
                dataGridViewNeedsAction.DataSource = this.GetNeedsAction();
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
        /// Odświerza tabelę zawierającą raporty.
        /// </summary>
        private void RefreshDataGridViewReport()
        {
            try
            {
                switch (comboBoxContentSelection.SelectedIndex)
                {
                    case 0:
                        dataGridViewReport.DataSource = this.GetLeavesHistory();
                        break;
                    case 1:
                        dataGridViewReport.DataSource = this.GetEmployeesCurrentlyOnLeave();
                        break;
                }
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
        /// Metoda wczytywania zawartości tabeli zawierającej pracowników.
        /// Wywołuje bezargumentową metodę o tej samej nazwie.
        /// Stworzona celem umożliwienia podpięcia do obsługi zdarzenia.
        /// </summary>
        /// <param name="o">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void RefreshDataGridViewEmployees(Object o, EventArgs e)
        {
            RefreshDataGridViewEmployees();
        }

        /// <summary>
        /// Metoda wczytywania zawartości tabeli zawierającej pracowników.
        /// </summary>
        private void RefreshDataGridViewEmployees()
        {
            try
            {
                dataGridViewEmployees.DataSource = this.GetEmployees();
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
        /// Metoda obsługi wciśnięcia guzika dodawania pracownika.
        /// Wyświetla formularz dodawania pracownika.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonEmployeesAdd_Click(object sender, EventArgs e)
        {
            FormAddOrEditEmployee form = new FormAddOrEditEmployee(connection);
            form.Show();
            form.FormClosed += new FormClosedEventHandler(this.RefreshDataGridViewEmployees);
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
                RefreshDataGridViewEmployees();
            else
                //Jeżeli zaznaczono zakładkę ze zgłoszeniami wymagającymi działania, to odśwież tam dane.
                if (tabControl.SelectedTab.Text.Equals("Needs your action"))
                    LoadDataGridViewNeedsAction();
                else
                    if (tabControl.SelectedTab.Text.Equals("Report"))
                        RefreshDataGridViewReport();
                    else
                        if (tabControl.SelectedTab.Text.Equals("Replacments"))
                            RefreshReplacments();
        }
        
        /// <summary>
        /// Metoda obsługi wciśnięcia guzika rozważenia zgłoszenia urlopowego.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonConsider_Click(object sender, EventArgs e)
        {
            //Dla każdej zaznaczonej komórki w tabeli zgłoszeń wymagających działania zaznacz jej cały wiersz.
            foreach (DataGridViewCell cell in dataGridViewNeedsAction.SelectedCells)
            {
                if (cell.Value != null)
                    dataGridViewNeedsAction.Rows[cell.RowIndex].Selected = true;
            }
            //Dla każdego zaznaczonego wiersza w tabeli zgłoszeń wymagających działania.
            foreach (DataGridViewRow row in dataGridViewNeedsAction.SelectedRows)
            {
                //Stwórz formularz rozważenia zgłoszenia.
                FormLeaveConsideration form = new FormLeaveConsideration(this, connection,
                    new Leave((int)row.Cells["Leave Id"].Value, (int)row.Cells["Employee Id"].Value, row.Cells["Type"].Value.ToString(), row.Cells["Status"].Value.ToString(),
                        (DateTime)row.Cells["First day"].Value, (DateTime)row.Cells["Last day"].Value, row.Cells["Remarks"].Value.ToString(), (int)row.Cells["Used days"].Value));
                //Dodaj do obsługi zdarzenia zamknięcia formularza metodę wczytywania zgłoszeń wymagających działania.
                form.FormClosed += new FormClosedEventHandler(LoadDataGridViewNeedsAction);
                //Wyświetl formularz rozważenia zgłoszenia.
                form.Show();
            }
        }

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
                form.FormClosed += new FormClosedEventHandler(RefreshDataGridViewEmployees);
                //Wyświetlenie formularza danych pracownika.
                form.Show();
            }           
        }

        private void buttonReject_Click(object sender, EventArgs e)
        {
            //Dla każdej zaznaczonej komórki zaznaczamy jej wiersz.
            foreach (DataGridViewCell cell in dataGridViewNeedsAction.SelectedCells)
            {
                if (cell.Value != null)
                    dataGridViewNeedsAction.Rows[cell.RowIndex].Selected = true;
            }
            //Dla każdego zaznaczonego wiersza.
            foreach (DataGridViewRow row in dataGridViewNeedsAction.SelectedRows)
            {
                try
                {
                    this.RejectLeave((int)row.Cells["Leave id"].Value);
                    LoadDataGridViewNeedsAction();
                }
                catch (SqlException)
                {
                    MessageBox.Show("SQL Error. This form will be close. Please try again later.");
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Invalid operation. This form will be close. Please try again later");
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
            }
            catch (SqlException)
            {
                MessageBox.Show("SQL Error. This form will be close. Please try again later.");
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Invalid operation. This form will be close. Please try again later");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown exception has occured" + ex.Message);
            }
        }

        /// <summary>
        /// Metoda obsługi przyciśnięcia guzika edycji pracownika 
        /// </summary>
        /// <param name="sender">Obiekt wysyłający</param>
        /// <param name="e">Argumenty</param>
        private void buttonEmployeeEdit_Click(object sender, EventArgs e)
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
                FormAddOrEditEmployee form = new FormAddOrEditEmployee(connection, (int)row.Cells["Employee id"].Value);
                /* Dodana zostaje metoda odświeżania tabeli oczekujących aplikacji urlopowych do obsługi
                 * zdarzenia zamknięcia formularza. Powodem tego jest umożliwienie w formularzu danych 
                 * pracownika zmiany właściwości jego aplikacji urlopowych.
                 */
                form.FormClosed += new FormClosedEventHandler(RefreshDataGridViewEmployees);
                //Wyświetlenie formularza danych pracownika.
                form.Show();
            }   
        }

        private void buttonEditSchedule_Click(object sender, EventArgs e)
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
                FormWorkSchedule form = new FormWorkSchedule(this.connection, (int)row.Cells["Employee id"].Value);
                form.FormClosed += new FormClosedEventHandler(RefreshDataGridViewEmployees);
                form.Show();
            }   
        }

        private void buttonEmployeesDetailedData_Click_1(object sender, EventArgs e)
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
                form.FormClosed += new FormClosedEventHandler(RefreshDataGridViewEmployees);
                //Wyświetlenie formularza danych pracownika.
                form.Show();
            }           
        }

        private void comboBoxContentSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDataGridViewReport();
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
                FormReplacement form = new FormReplacement((string)row.Cells["Position"].Value, (DateTime)row.Cells["Date"].Value, connection);
                /* Dodana zostaje metoda odświeżania tabeli oczekujących aplikacji urlopowych do obsługi
                 * zdarzenia zamknięcia formularza. Powodem tego jest umożliwienie w formularzu danych 
                 * pracownika zmiany właściwości jego aplikacji urlopowych.
                 */
                form.FormClosed += new FormClosedEventHandler(RefreshReplacments);
                //Wyświetlenie formularza danych pracownika.
                form.Show();
            }
        }
   
    }
}
