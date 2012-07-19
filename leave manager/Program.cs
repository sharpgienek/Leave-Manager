using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace leave_manager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {                    
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            String connectionString = "";
            //Sprawdzenie, czy nie istnieje plik konfiguracyjny.
            if (!File.Exists(Path.GetDirectoryName(Application.ExecutablePath) + @"\config.ini"))
            {
                //Stworzenie i uruchomienie formularza definiowania bazy danych.
                FormDefineDatabase form = new FormDefineDatabase();
                Application.Run(form);
                connectionString = form.ConnectionString;
                /* Jeżeli connectionString jest pusty, oznacza to, że użytkownik nie wybrał poprawnie bazy
                 * i następuje zakończenie programu.
                 */
                if (connectionString.Length == 0)
                    return;
            }
            else//Jeżeli plik konfiguracyjny istnieje, to następuje zczytanie z niego connection string'a.
            {
                connectionString = File.ReadAllText(Path.GetDirectoryName(Application.ExecutablePath) + @"\config.ini");
            }        
            //Utworzenie obiektu połączenia.
            SqlConnection connection = new SqlConnection(connectionString);
            //Sprawdzenie poprawności połączenia z bazą danych.
            if (!connection.TestConnection())
                MessageBox.Show("Could not connect to database. Try again later. If this problem will not stop, contact your administrator.");
            else
            {
                //Otworzenie połączenia.
                connection.Open();

                DatabaseOperator.UpdateLeaveDays(connection);

                //Uruchomienie formularza logowania.
                FormLogin form = new FormLogin(connection);
                Application.Run(form);
                //Sprawdzenie, czy użytkownik został poprawnie zalogowany. Jeżeli nie, to kończymy.
                if(form.LoggedIn)
                    /* Jeżeli użytkownik jest poprawnie zalogowany to nastepuje sprawdzenie jego uprawnień
                     * i w zależności od nich uruchomienie odpowiedniego formularza.
                     */
                    switch (form.Employee.Permission)
                    {
                        case "administrator":
                          Application.Run(new FormAdmin(connection));                           
                          Application.Run(new FormEmployee(connection, form.Employee));
                          Application.Run(new FormAssistant(connection));
                            Application.Run(new FormManager(connection));
                            break;
                        case "manager":
                            Application.Run(new FormManager(connection));
                            break;
                        case "assistant":
                            Application.Run(new FormAssistant(connection));
                            break;
                        case "employee":
                            Application.Run(new FormEmployee(connection, form.Employee));
                            break;
                    }               
                connection.Close();
            }
        }
    }
}
