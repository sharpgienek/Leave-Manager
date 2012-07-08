using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

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
            catch//todo obsługa wszystkich rodzajów wyjątków.
            {
                throw new NotImplementedException();
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
            catch 
            {
                throw new NotImplementedException();
            }//todo obsługa wszystkich rodzajów błędów + czyszczenie datagridview
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
    }
}
