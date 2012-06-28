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
    /// Klasa formularza logowania.
    /// </summary>
    public partial class FormLogin : LeaveManagerForm
    { 
        /// <summary>
        /// Atrybut zawierający dane zalogowanego użytkownika. Przed zalogowaniem jest pusty.
        /// </summary>
        private Employee employee;

        /// <summary>
        /// Właściwość zwracająca dane zalogowanego użytkownika.
        /// </summary>
        public Employee Employee { get { return employee; } }

        /// <summary>
        /// Atrybut określający czy poprawnie zalogowano użytkownika.
        /// </summary>
        private bool loggedIn;

        /// <summary>
        /// Właściwość zwracająca informację o tym, czy poprawnie zalogowano użytkownika.
        /// </summary>
        public bool LoggedIn { get { return loggedIn; } }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connection">Połączenie do bazy danych.</param>
        public FormLogin(SqlConnection connection)
        {
            InitializeComponent();
            //Zachowanie połączenia do bazy danych.
            this.connection = connection;
            this.loggedIn = false;
        }

        /// <summary>
        /// Metoda wywoływana w przypadku naciśnięcia guzika "Exit".
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Metoda wywoływana w przypadku naciśnięcia guzika logowania.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                //Próba zalogowania
                employee = this.login(textBoxLogin.Text, textBoxPassword.Text);
                //Jeżeli próba zalogowania powiodła się to ustawiamy flagę zalogowania.
                loggedIn = true;
                this.Close();
            }
                //todo obsłuż wszystkie rodzaje wyjątków
            catch
            {
            }           
        }
    }
}
