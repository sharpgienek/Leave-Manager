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
    /// Klasa formularza użytkownika z uprawnieniami pracownika.
    /// </summary>
    public partial class FormEmployee : LeaveManagerForm
    {
        /// <summary>
        /// Atrybut zawierający informacje o zalogowanym pracowniku.
        /// </summary>
        private Employee employee;

        /// <summary>
        /// Konstruktor bezargumentowy.
        /// </summary>
        public FormEmployee()
        { }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connection">Połączenie z bazą danych. Powinno być otwarte.</param>
        /// <param name="employee"></param>
        public FormEmployee(SqlConnection connection, Employee employee)
        {
            InitializeComponent();
            this.connection = connection;
            this.employee = employee;
            labelNameValue.Text = employee.Name + " " + employee.Surname;
            labelPositionValue.Text = employee.Position;
            refreshData();
        }

        /// <summary>
        /// Metoda odpowiedzialna za wczytanie aktualnych danych użytkownika.
        /// Korzysta z metody bezargumentowej o tej samej nazwie.
        /// Stworzona celem umożliwienia podpięcia jej jako obsługi
        /// zdarzenia zamknięcia formularza.
        /// </summary>
        /// <param name="o">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void refreshData(object o, FormClosedEventArgs e)
        {
            refreshData();
        }

        /// <summary>
        /// Metoda odpowiedzialna za ponowne wczytanie aktualnych danych użytkownika.
        /// </summary>
        private void refreshData()
        {
            try
            {
                //Wczytanie danych urlopowych.
                dataGridView.DataSource = this.getLeaves(employee.EmployeeId);
                /* Zmienne do których zczytane zostaną odpowiednio liczba dostępnych
                 * dni urlopu z bieżącego roku i liczba dostępnych dni zaległych.
                 */
                int leaveDays = 0;
                int oldLeaveDays = 0;        
                //Zczytanie wartości dostępnych dni.
                this.getDays(employee.EmployeeId, ref leaveDays, ref oldLeaveDays);
                employee.LeaveDays = leaveDays;
                employee.OldLeaveDays = oldLeaveDays;
                //Przypisanie do etykiety dni dostępnych.
                labelDaysToUseValue.Text = (leaveDays + oldLeaveDays).ToString();
            }
            catch 
            {
                throw new NotImplementedException();
            }//todo obsługa wszystkich wyjątków
        }

        /// <summary>
        /// Metoda obsługująca wciśnięcie przycisku zmiany loginu i/lub hasła.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonChangeLoginOrPassword_Click(object sender, EventArgs e)
        {
            //Stworzenie i wywołanie formularza zmiany loginu i/lub hasła.
            FormChangeLoginOrPassword form = new FormChangeLoginOrPassword(connection, employee.EmployeeId);
            form.Show();
        }

        /// <summary>
        /// Metoda obsługująca wciśnięcie przycisku złożenia wniosku o urlop.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonTakeLeave_Click(object sender, EventArgs e)
        {
            //Formularz aplikacji o urlop.
            FormLeaveApplication form = new FormLeaveApplication(this, connection, employee.EmployeeId);
            /* Dodanie metody odświeżającej dane formularza związane z urlopami do obsługi
             * wydarzenia zamknięcia formularza aplikacji o urlop.
             */
            form.FormClosed += new FormClosedEventHandler(refreshData);
            form.Show();
        }

        /// <summary>
        /// Metoda obsługująca wciśnięcie przycisku usunięcia urlopu.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        /// <remarks>Urlop zatwierdzony nie może zostać usunięty.</remarks>
        private void buttonDeleteLeave_Click(object sender, EventArgs e)
        {
            //Dla każdej zaznaczonej komórki w tabeli urlopów zaznacz jej cały wiersz.
            foreach (DataGridViewCell cell in dataGridView.SelectedCells)
            {
                dataGridView.Rows[cell.RowIndex].Selected = true;
            }
            /* Dla każdego wiersza zostaje wywołana metoda usunięcia urlopu.
             * Usuwanie jest objęte transakcją: Jeżeli choć jedna operacja 
             * usuwania nie powiedzie się, to żaden urlop nie będzie usunięty.
             */
            this.BeginTransaction();
            try
            {
                //Dla każdego zaznaczonego wiersza.
                foreach (DataGridViewRow row in dataGridView.SelectedRows)
                {
                    //Jeżeli status urlopu jest różny od zatwierdzonego.
                    if (!row.Cells["Status"].Value.ToString().Equals("Approved"))
                    {
                        this.DeleteLeave(employee.EmployeeId, (DateTime)row.Cells["First day"].Value, (DateTime)row.Cells["Last day"].Value);                        
                    }
                    else
                    {                         
                        MessageBox.Show("You can not delete approved leaves.\n");
                        this.RollbackTransaction();
                        return;
                    }
                }
                this.CommitTransaction();
                refreshData();
            }
            catch//todo obsługa wszystkich wyjątków.
            {
                this.RollbackTransaction();
                throw new NotImplementedException();
            }
        }
    }
}
