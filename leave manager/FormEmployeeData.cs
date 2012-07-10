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
            if (parent.GetType() == new FormAssistant().GetType())
            {
                this.buttonEdit.Text = "Edit remarks";
                this.buttonDelete.Visible = false;
            }
            if (parent.GetType() == new FormManager().GetType())
            {
                this.buttonEdit.Text = "Edit leave entry";
            }
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
            catch 
            {
                this.RollbackTransaction();
                throw new NotImplementedException();
            }//todo obsługa wszystkich wyjątków
        }

        //todo wyeliminować tą metodę?
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
                FormLeaveApplication form = new FormLeaveApplication(this.parent, connection, this.GetLeave((int)row.Cells["Leave id"].Value));
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
    }
}
