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
    /// Klasa formularza określania źródła danych.
    /// </summary>
    public partial class FormDefineDatabase : LeaveManagerForm
    {
        /// <summary>
        /// Atrybut przechowujący connection string.
        /// </summary>
        private String connectionString;

        /// <summary>
        /// Właściwość zwracająca connection string.
        /// </summary>
       public String ConnectionString
        {
            get { return connectionString; }
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        public FormDefineDatabase()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Metoda wywoływana w przypadku zmiany zaznaczenia radioButtonLocal.
        /// Odpowiada ona za wyświetlanie i chowanie elementów interfejsu użytkownika 
        /// odpowiednich dla zaznaczonych opcji.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void radioButtonLocal_CheckedChanged(object sender, EventArgs e)
        {
            //Jeżeli baza danych jest plikiem.
            if (radioButtonLocal.Checked)
            {
                //Wyświetl grupę odpowiednią dla pliku.
                groupBoxLocal.Visible = true;
                //Schowaj grupę odpowiednią dla bazy zdalnej.
                groupBoxRemote.Visible = false;
            }
            else
            {
                groupBoxRemote.Visible = true;
                groupBoxLocal.Visible = false;
            }
        }

        /// <summary>
        /// Metoda odpowiedzialna za obsługę przycisku testu połączenia z bazą danych.
        /// Wyświetla informację o wyniku próby połączenia.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonTestConnection_Click(object sender, EventArgs e)
        {
            //Jeżeli źródłem bazy danych jest plik na dysku.
            if (radioButtonLocal.Checked)
                //Jeżeli ścieżka do pliku jest niepusta.
                if (textBoxLocalPath.Text.Length != 0)
                {
                    if (new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=" + "\"" + textBoxLocalPath.Text + "\"" + ";Integrated Security=True;User Instance=True;").TestConnection())
                        MessageBox.Show("Test went ok.");
                    else
                        MessageBox.Show("Connection can not be established.");
                }
                else
                    MessageBox.Show("Empty Path!");//todo ładny tekst
            else//todo
                throw new NotImplementedException();
        }

        /// <summary>
        /// Metoda odpowiedzialna za obsługę przycisku przeglądania dysku w poszukiwaniu
        /// pliku z bazą danych. Powoduje uzupełnienie texBox'a ścieżki do pliku.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonLocalBrowse_Click(object sender, EventArgs e)
        {            
            OpenFileDialog browseDialog = new OpenFileDialog();
            if (browseDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxLocalPath.Text = browseDialog.FileName;
            }
        }

        /// <summary>
        /// Metoda odpowiedzialna za obsługę guzika akceptacji nowych ustawień połączenia z bazą danych.
        /// Sprawdza możliwość połączenia z bazą i jeżeli połączenie powiedzie się to zapisuje
        /// nowe ustawienia w pliku "config.ini" oraz zachowuje connection string.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonAccept_Click(object sender, EventArgs e)
        {
            if (radioButtonLocal.Checked)
                if (!textBoxLocalPath.Text.Equals(""))
                {
                    if (new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=" + 
                        "\"" + textBoxLocalPath.Text + "\"" + ";Integrated Security=True;User Instance=True;").TestConnection())
                    {
                        using (StreamWriter outfile = new StreamWriter(Path.GetDirectoryName(Application.ExecutablePath) + @"\config.ini"))
                        {
                            //Zapisanie nowej konfiguracji do pliku.
                            try
                            {
                                outfile.Write(@"Data Source=.\SQLEXPRESS;AttachDbFilename=" + "\"" +
                                    textBoxLocalPath.Text + "\"" + ";Integrated Security=True;User Instance=True;");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("An Error has occured. Your new configuration may not be saved. Please try again later\n" + ex.Message);
                            }
                                //Zachowanie nowego connection string.
                            connectionString += @"Data Source=.\SQLEXPRESS;AttachDbFilename=" + 
                                "\"" + textBoxLocalPath.Text + "\"" + ";Integrated Security=True;User Instance=True;";
                            this.Close();
                        }
                    }
                    else
                        MessageBox.Show("Connection can not be established.");
                }
                else
                    MessageBox.Show("Empty Path!");//todo ładny tekst
            else
                throw new NotImplementedException(); 
        }

        /// <summary>
        /// Metoda obsługi wciśnięcia guzika wyłączenia programu.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonExit_Click(object sender, EventArgs e)
        {
            connectionString = "";
            this.Close();
        }
    }
}
