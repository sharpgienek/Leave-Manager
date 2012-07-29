using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace leave_manager
{
    /// <summary>
    /// Klasa formularza użytkownika z uprawnieniami administratora.
    /// </summary>
    public partial class FormAdmin : LeaveManagerForm
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connection">Połączenie z bazą danych. Powinno być otwarte.</param>
        public FormAdmin(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            /* Pierwsze wczytanie danych do tabeli zawierającej pracowników nie poinformowanych
             * o loginie i haśle.
             */
            RefreshDataGridViewNewEmployees();
            // Pierwsze wczytanie danych do tabeli zawierającej rodzaje możliwych pozycji pracowników.
            RefreshDataGridViewPositions();
        }

        /// <summary>
        /// Metoda odświeżania zawartości tabeli zawierającej rodzaje urlopów.
        /// Wywołuje bezargumentową metodę o tej samej nazwie. 
        /// Stworzona aby można było ją wywołać jako obsługę zdarzenia
        /// zamknięcia formularza, który zmienia dane wykorzystywane w tabeli.
        /// </summary>
        /// <param name="o">Objekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void RefreshDataGridViewLeaveTypes(object o, FormClosedEventArgs e)
        {
            RefreshDataGridViewLeaveTypes();
        }

        /// <summary>
        /// Metoda odświeżania zawartości tabeli zawierającej rodzaje urlopów.
        /// </summary>
        private void RefreshDataGridViewLeaveTypes()
        {
            try
            {
                //Ustawienie jako źródła danych obiektu zawierającego dane aktualne.
                dataGridViewLeaveTypes.DataSource = this.GetLeaveTypesTable();
                //Schowanie kolumny z numerem id typu urlopu.
                dataGridViewLeaveTypes.Columns["Leave type id"].Visible = false;
            }
            catch//todo obsługa wszystkich wyjątków.
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Metoda odświeżania zawartości tabeli zawierającej rodzaje pozycji pracowników.
        /// Wywołuje bezargumentową metodę o tej samej nazwie. 
        /// Stworzona aby można było ją wywołać jako obsługę zdarzenia
        /// zamknięcia formularza, który zmienia dane wykorzystywane w tabeli.
        /// </summary>
        /// <param name="o">Objekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void RefreshDataGridViewPositions(object o, FormClosedEventArgs e)
        {
            RefreshDataGridViewPositions();
        }

        /// <summary>
        /// Metoda odświeżania zawartości tabeli zawierającej rodzaje pozycji pracowników.
        /// </summary>
        private void RefreshDataGridViewPositions()
        {
            try
            {
                //Ustawienie jako źródła danych obiektu zawierającego dane aktualne.
                dataGridViewPositions.DataSource = this.GetPositionsTable();
            }
            catch//todo obsługa wszystkich wyjątków
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Metoda odświeżania zawartości tabeli zawierającej pracowników nie poinformowanych o 
        /// loginie i haśle.
        /// </summary>
        private void RefreshDataGridViewNewEmployees()
        {
            try
            {
                //Ustawienie jako źródła danych obiektu zawierającego dane aktualne.
                dataGridViewNewEmployees.DataSource = this.GetUninformedEmployees();
            }
            catch
            {
                throw new NotImplementedException();
            }//todo obsłużyć wszystkie wyjątki.
        }

        /// <summary>
        /// Metoda wywoływana w przypadku zmiany zaznaczenia radioButtonDataSourceLocal.
        /// Odpowiada ona za wyświetlanie i chowanie elementów interfejsu użytkownika 
        /// odpowiednich dla zaznaczonych opcji.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void radioButtonDataSourceLocal_CheckedChanged(object sender, EventArgs e)
        {
            //Jeżeli baza danych jest plikiem.
            if (radioButtonDataSourceLocal.Checked)
            {
                //Wyświetl grupę odpowiednią dla pliku.
                groupBoxDataSourceLocal.Visible = true;
                //Schowaj grupę odpowiednią dla bazy zdalnej.
                groupBoxDataSourceRemote.Visible = false;
            }
            else
            {
                groupBoxDataSourceRemote.Visible = true;
                groupBoxDataSourceLocal.Visible = false;
            }
        }

        /// <summary>
        /// Metoda odpowiedzialna za obsługę przycisku przeglądania dysku w poszukiwaniu
        /// pliku z bazą danych. Powoduje uzupełnienie texBox'a ścieżki do pliku.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonDataSourceLocalBrowse_Click(object sender, EventArgs e)
        {
            //Obiekt okna wyboru pliku.
            OpenFileDialog browseDialog = new OpenFileDialog();
            //Jeżeli zatwierdzono wybór uzupełniamy textBox.
            if (browseDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxDataSourceLocalPath.Text = browseDialog.FileName;
            }
        }

        /// <summary>
        /// Metoda odpowiedzialna za obsługę przycisku testu połączenia z bazą danych.
        /// Wyświetla informację o wyniku próby połączenia.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonDataSourceTestConnection_Click(object sender, EventArgs e)
        {
            //Jeżeli źródłem bazy danych jest plik na dysku.
            if (radioButtonDataSourceLocal.Checked)
                //Jeżeli ścieżka do pliku jest niepusta.
                if (textBoxDataSourceLocalPath.Text.Length != 0)
                {
                    if (new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=" + "\"" + textBoxDataSourceLocalPath.Text +
                        "\"" + ";Integrated Security=True;User Instance=True;").TestConnection())
                    {
                        MessageBox.Show("Test went ok.");
                    }
                    else
                    {
                        MessageBox.Show("Connection can not be established.");
                    }
                }
                else
                {
                    MessageBox.Show("Empty Path!");//todo ładny tekst
                }
            else//todo
                throw new NotImplementedException();
        }

        /// <summary>
        /// Metoda odpowiedzialna za obsługę guzika akceptacji nowych ustawień połączenia z bazą danych.
        /// Sprawdza możliwość połączenia z bazą i jeżeli połączenie powiedzie się to zapisuje
        /// nowe ustawienia w pliku "config.ini".
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonDataSourceAccept_Click(object sender, EventArgs e)
        {
            //Jeżeli ustawiono bazę danych z pliku.
            if (radioButtonDataSourceLocal.Checked)
                //Sprawdzenie, czy ścieżka do pliku bazy jest niepusta.
                if (textBoxDataSourceLocalPath.Text.Length != 0)
                {
                    if (new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=" + "\"" +
                        textBoxDataSourceLocalPath.Text + "\"" +
                        ";Integrated Security=True;User Instance=True;").TestConnection())
                    {
                        using (StreamWriter outfile = new StreamWriter(Path.GetDirectoryName(Application.ExecutablePath) + @"\config.ini"))
                        {
                            //Zapisanie ustawień do pliku.
                            outfile.Write(@"Data Source=.\SQLEXPRESS;AttachDbFilename=" + "\"" +
                                textBoxDataSourceLocalPath.Text + "\"" + ";Integrated Security=True;User Instance=True;");
                            MessageBox.Show("You need to reset your client in order to load new configuration.");
                        }
                    }
                    else
                        MessageBox.Show("Connection can not be established.");
                }
                else
                    MessageBox.Show("Empty Path!");//todo ładny tekst
            else//todo
                throw new NotImplementedException();
        }

        /// <summary>
        /// Metoda odpowiedzialna za obsługę przycisku usuwającego zaznaczonych pracowników
        /// z listy pracowników nie poinformowanych o loginie i haśle.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonNewEmployeesInformed_Click(object sender, EventArgs e)
        {
            //Dla każdej zaznaczonej komórki zaznaczamy jej cały wiersz.
            foreach (DataGridViewCell cell in dataGridViewNewEmployees.SelectedCells)
            {
                dataGridViewNewEmployees.Rows[cell.RowIndex].Selected = true;
            }
            /* Dla każdego zaznaczonego wiersza wywołujemy metodę usunięcia pracownika
             * z danego wiersza z listy pracowników nie poinformowanych. Całość obejmujemy transakcją.
             * Jeżeli któregoś z pracowników nie uda się usunąć z listy, nie usuwamy żadnego.
             */
            this.BeginTransaction();
            try
            {
                foreach (DataGridViewRow row in dataGridViewNewEmployees.SelectedRows)
                {
                    this.EmployeeInformed((int)row.Cells["ID"].Value);
                }
                this.CommitTransaction();
            }
            catch 
            {
                throw new NotImplementedException();
            }//todo obsługa wszystkich wyjątków.
            RefreshDataGridViewNewEmployees();
        }

        /// <summary>
        /// Metoda obsługująca zdarzenie zmiany zakładki w zestawie zakładek ze słownikami.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void tabControlDictionaries_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Jeżeli zaznaczono zakładkę z pozycjami, to odśwież jej tabelę.
            if (tabControlDictionaries.SelectedTab.Text.Equals("Positions"))
                RefreshDataGridViewPositions();
            //Jeżeli zaznaczono zakładkę z typami urlopów, to odśwież jej tabelę.
            if (tabControlDictionaries.SelectedTab.Text.Equals("Leave types"))
                RefreshDataGridViewLeaveTypes();
        }

        /// <summary>
        /// Metoda obsługująca zdarzenie zmiany zakładki w głównym zestawie zakładek.
        /// Odświeża odpowiednie dla zaznaczonej zakładki dane.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Jeżeli zaznaczono zakładkę ze słownikami.
            if (tabControl.SelectedTab.Text.Equals("Dictionaries"))
            {
                //Jeżeli w zakładce ze słownikami zaznaczono zakładkę ze słownikiem pozycji.
                if (tabControlDictionaries.SelectedTab.Text.Equals("Positions"))
                    RefreshDataGridViewPositions();
                //Jeżeli w zakładce ze słownikami zaznaczono zakładkę ze słownikiem typów urlopów.
                if (tabControlDictionaries.SelectedTab.Text.Equals("Leave types"))
                    RefreshDataGridViewLeaveTypes();
            }
            //Jeżeli zaznaczono zakładkę z nie poinformowanymi pracownikami.
            if (tabControl.SelectedTab.Text.Equals("New Employees"))
                RefreshDataGridViewNewEmployees();

            if (tabControl.SelectedTab.Text.Equals("Public Holidays"))
                RefreshDataGridViewPublicHolidays();
        }

        private void RefreshDataGridViewPublicHolidays(object sender, FormClosedEventArgs e)
        {
            RefreshDataGridViewPublicHolidays();
        }

        private void RefreshDataGridViewPublicHolidays()
        {
            try
            {
                dataGridViewPublicHolidays.DataSource = this.GetPublicHolidays();
                foreach (DataGridViewColumn col in dataGridViewPublicHolidays.Columns)
                {
                    if (!col.HeaderText.Equals("Date"))
                    {
                        if (col.HeaderText.Equals("DayOfWeek"))
                        {
                            col.HeaderText = "Day of week";
                        }
                        else
                        {
                            if (!col.HeaderText.Equals("Day of week"))
                            {
                                col.Visible = false;
                            }
                        }
                    }
                }
            }
            catch//todo obsługa wyjątków.
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Metoda obsługująca zdarzenie wciśnięcia przycisku dodawania nowego typu
        /// pozycji do słownika pozycji.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonAddNewPosition_Click(object sender, EventArgs e)
        {
            //Stworzenie formularza dodawania nowej pozycji.
            FormAddPosition form = new FormAddPosition(connection);
            /* Dodanie metody odświeżania tabeli z pozycjami do zdarzenia zamknięcia formularza
             * dodawania nowej pozycji.
             */
            form.FormClosed += new FormClosedEventHandler(RefreshDataGridViewPositions);
            //Wyświetlenie formularza dodawania nowej pozycji.
            form.Show();
        }

        /// <summary>
        /// Metoda obsługująca zdarzenie wciśnięcia przycisku usunięcia zaznaczonego typu
        /// pozycji ze słownika możliwych pozycji pracowników.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        /// <remarks>Można usuwać tylko jedną pozycję na raz.</remarks>
        private void buttonDeletePosition_Click(object sender, EventArgs e)
        {
            //Dla każdej zaznaczonej komórki zaznaczamy cały wiersz.
            foreach (DataGridViewCell cell in dataGridViewPositions.SelectedCells)
            {
                dataGridViewPositions.Rows[cell.RowIndex].Selected = true;
            }
            //Jeżeli przynajmniej jeden wiersz jest zaznaczony.
            if (dataGridViewPositions.SelectedRows.Count != 0)
            {
                //Jeżeli zaznaczono tylko jeden wiersz.
                if (dataGridViewPositions.SelectedRows.Count == 1)
                {
                    //Stworzenie formularza kasowania pozycji.
                    FormDeletePosition form = new FormDeletePosition(connection,
                        dataGridViewPositions.SelectedRows[0].Cells[1].Value.ToString());
                    /* Dodanie metody odświeżania tabeli pozycji do obsługi zdarzenia zamknięcia
                     * formularza kasowania pozycji.
                     */
                    form.FormClosed += new FormClosedEventHandler(RefreshDataGridViewPositions);
                    //Wyświetlenie fomularza kasowania pozycji.
                    form.Show();
                }
                else
                {
                    MessageBox.Show("You may delete only one position at a time.");
                }
            }
            else
            {
                MessageBox.Show("No position selected.");
            }
        }

        /// <summary>
        /// Metoda obsługująca zdarzenie wciśnięcia przycisku dodawania nowego 
        /// typu urlopu.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonAddLeaveType_Click(object sender, EventArgs e)
        {
            //Stworzenie obiektu formularza dodawania nowego typu urlopu.
            FormAddLeaveType form = new FormAddLeaveType(connection);
            /* Dodanie metody odświeżania tabeli z typami urlopów do obsługi zdarzenia
             * zamknięcia formularza dodawania nowego typu urlopu.
             */
            form.FormClosed += new FormClosedEventHandler(RefreshDataGridViewLeaveTypes);
            //Wyświetlenie formularza dodawania nowego typu urlopu.
            form.Show();
        }

        /// <summary>
        /// Metoda obsługująca wciśnięcie przycisku kasowania zaznaczonego typu urlopu.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        /// <remarks>Można usunąć tylko jeden wpis na raz.</remarks>
        private void buttonDeleteLeaveType_Click(object sender, EventArgs e)
        {
            //Dla każdej zaznaczonej komórki zaznaczamy jej wiersz.
            foreach (DataGridViewCell cell in dataGridViewLeaveTypes.SelectedCells)
            {
                dataGridViewLeaveTypes.Rows[cell.RowIndex].Selected = true;
            }
            //Jeżeli przynajmniej jeden wiersz jest zaznaczony.
            if (dataGridViewLeaveTypes.SelectedRows.Count != 0)
            {
                //Jeżeli dokładnie jeden wiersz jest zaznaczony.
                if (dataGridViewLeaveTypes.SelectedRows.Count == 1)
                {
                    //Jeżeli zaznaczony typ to nie chorobowe (chorobowego nie można usunąć).
                    if (!dataGridViewLeaveTypes.SelectedRows[0].Cells[1].Value.ToString().Equals("Sick"))
                    {
                        //Stworzenie obiektu formularza usuwania typu urlopu.
                        FormDeleteLeaveType form = new FormDeleteLeaveType(connection,
                            dataGridViewLeaveTypes.SelectedRows[0].Cells[1].Value.ToString());
                        /* Dodanie metody odświeżania tabeli zawierającej typy urlopów do zdarzenia
                         * zamknięcia formularza usuwania typu urlopu.
                         */
                        form.FormClosed += new FormClosedEventHandler(RefreshDataGridViewLeaveTypes);
                        //Wyświetlenie formularza usuwania typu urlopu.
                        form.Show();
                    }
                    else
                    {
                        MessageBox.Show("You are not allowed to delete this leave type.");
                    }
                }
                else
                {
                    MessageBox.Show("You may delete only one position at a time.");
                }
            }
            else
            {
                MessageBox.Show("No position selected.");
            }
        }

        private void buttonAddPublicHoliday_Click(object sender, EventArgs e)
        {
            FormAddPublicHoliday form = new FormAddPublicHoliday(connection);
            form.FormClosed += new FormClosedEventHandler(RefreshDataGridViewPublicHolidays);
            form.Show();
        }

        private void buttonDeletePublicHoliday_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell cell in dataGridViewPublicHolidays.SelectedCells)
            {
                dataGridViewPublicHolidays.Rows[cell.RowIndex].Selected = true;
            }
            foreach (DataGridViewRow row in dataGridViewPublicHolidays.SelectedRows)
            {
                this.DeletePublicHoliday((DateTime)row.Cells["Date"].Value);
            }
            RefreshDataGridViewPublicHolidays();
        }
    }
}
