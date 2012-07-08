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
            //Pierwsze uzupełnienie tabeli zawierającej zgłoszenia wymagające ropatrzenia przez kierownika.
            LoadDataGridViewNeedsAction();
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
            catch//todo obsługa wszystkich rodzajów wyjątków.
            { }
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
            catch { }//todo obsługa wszystkich wyjątków.           
        }

        /// <summary>
        /// Metoda obsługi wciśnięcia guzika dodawania pracownika.
        /// Wyświetla formularz dodawania pracownika.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonEmployeesAdd_Click(object sender, EventArgs e)
        {
            FormAddEmployee form = new FormAddEmployee(connection);
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
            //Jeżeli zaznaczono zakładkę ze zgłoszeniami wymagającymi działania, to odśwież tam dane.
            if (tabControl.SelectedTab.Text.Equals("Needs your action"))
                LoadDataGridViewNeedsAction();
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
    }
}
