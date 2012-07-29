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
    /// Klasa formularza zawierającego szczegółowe dane odnośnie urlopów pracownika.
    /// </summary>
    public partial class FormEmployeeData : LeaveManagerForm
    {
        /// <summary>
        /// Id pracownika, którego dotyczy formularz.
        /// </summary>
        private int employeeId;

        /// <summary>
        /// Referencja do obiektu rodzica.
        /// </summary>
        private object parent;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="parent">Obiekt rodzica.</param>
        /// <param name="connection">Połączenie z bazą danych. Powinno być otwarte.</param>
        /// <param name="employeeId">Id pracownika, którego dotyczy formularz.</param>
        /// <param name="name">Imię pracownika.</param>
        /// <param name="surname">Nazwisko pracownika.</param>
        /// <param name="position">Pozycja pracownika.</param>
        public FormEmployeeData(object parent, SqlConnection connection, int employeeId, String name, String surname, String position)
        {
            InitializeComponent();
            this.connection = connection;
            this.employeeId = employeeId;
            this.parent = parent;
            labelNameValue.Text = name + " " + surname;
            labelPositionValue.Text = position;
            RefreshData();
        }

        /// <summary>
        /// Metoda wczytująca aktualne dane pracownika, którego dotyczy formularz.
        /// Korzysta z bezargumentowej metody o tej samej nazwie.
        /// Stworzona celem umożliwienia podpięcia tej metody pod obsługę
        /// zdarzenia zamknięcia formularza.
        /// </summary>
        /// <param name="o">Obiekt wysyłąjący.</param>
        /// <param name="e">Argumenty.</param>
        private void RefreshData(object o, FormClosedEventArgs e)
        {
            RefreshData();
        }

        /// <summary>
        /// Metoda wczytująca aktualne dane pracownika, którego dotyczy formularz.
        /// </summary>
        private void RefreshData()
        {
            //Zmienne do których zostanie zczytana liczba dostępnych dni.
            int leaveDays = 0;
            int oldLeaveDays = 0;
            //Transakcja celem zczytania zgodnych danych.
            this.BeginTransaction(IsolationLevel.RepeatableRead);
            try
            {
                //Zczytanie dostępnych dni.
                this.GetDays(employeeId, ref leaveDays, ref oldLeaveDays);
                DataTable data = new DataTable();
                //Wczytanie urlopów.
                dataGridView.DataSource = this.GetLeaves(employeeId);
                //Zaktualizowanie etykiety mówiącej o liczbie dostępnych dni.
                labelDaysToUseValue.Text = (leaveDays + oldLeaveDays).ToString();
                this.CommitTransaction();
            }
            catch (SqlException)
            {
                MessageBox.Show("SQL error. Please try connection to database or try again later");
                this.RollbackTransaction();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Invalid operation. Please try again later.");
                this.RollbackTransaction();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown exception has occured" + ex.Message);
                this.RollbackTransaction();
            }
        }

        /// <summary>
        /// Metoda obsługi wciśnięcia przycisku edycji urlopu.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell cell in dataGridView.SelectedCells)
            {
                dataGridView.Rows[cell.RowIndex].Selected = true;
            }
            foreach (DataGridViewRow row in dataGridView.SelectedRows)
            {
                Leave editedLeave = this.GetLeave((int)row.Cells["Leave id"].Value);
                FormLeaveApplication form = new FormLeaveApplication(connection, editedLeave);
                form.FormClosed += new FormClosedEventHandler(RefreshData);
                form.Show();
            }
        }

        /// <summary>
        /// Metoda obsługująca wciśnięcie przycisku dodawania nowego urlopu.
        /// Powoduje pojawienie się formularza dodawania nowego zgłoszenia urlopowego.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //Formularz zgłoszenia urlopowego.
            FormLeaveApplication form = new FormLeaveApplication(parent, connection, employeeId);
            /* Dodanie metody odświeżenia danych formularza do obsługi zdarzenia
             * zamknięcia formularza zgłoszenia urlopowego.
             */
            form.FormClosed += new FormClosedEventHandler(RefreshData);
            form.Show();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
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
            this.BeginTransaction(IsolationLevel.RepeatableRead);
            try
            {
                //Dla każdego zaznaczonego wiersza.
                foreach (DataGridViewRow row in dataGridView.SelectedRows)
                {
                    //Jeżeli status urlopu jest różny od zatwierdzonego.
                    if (!row.Cells["Status"].Value.ToString().Equals("Approved"))
                    {
                        this.DeleteLeave((int)row.Cells["Leave Id"].Value);
                    }
                    else
                    {
                        if (parent is FormManager)
                        {
                            this.DeleteLeave((int)row.Cells["Leave Id"].Value);
                        }
                        else
                        {
                            MessageBox.Show("You can not delete approved leaves.\n");
                            this.RollbackTransaction();
                            return;
                        }
                    }
                }
                this.CommitTransaction();
                RefreshData();
            }
            catch (SqlException)
            {
                MessageBox.Show("SQL error. Please try connection to database or try again later");
                this.RollbackTransaction();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Invalid operation. Please try again later.");
                this.RollbackTransaction();
            }
            catch (IsolationLevelException)
            {
                MessageBox.Show("Isolation level error. Please try again later or contact administrator");
                this.RollbackTransaction();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown exception has occured" + ex.Message);
                this.RollbackTransaction();
            }
        }
    }
}
