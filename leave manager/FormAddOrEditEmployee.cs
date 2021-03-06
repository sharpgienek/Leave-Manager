﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.Mail;
using leave_manager.Exceptions;

namespace leave_manager
{
    /// <summary>
    /// Klasa formularza dodawania nowego pracownika.
    /// </summary>
    public partial class FormAddOrEditEmployee : LeaveManagerForm
    {
        /// <summary>
        /// Atrybut przechowujący obecne dane pracownika, którego edytujemy.
        /// Jeśli atrybut ten ma wartość null informuje on dodatkowo o tym, że nie edytujemy
        /// pracownika a dodajemy nowego.
        /// </summary>
        private Employee employeeToEdit;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connection">Połączenie do bazy danych. Powinno być otwarte.</param>
        public FormAddOrEditEmployee(SqlConnection connection)
        {
            InitializeComponent();
            employeeToEdit = null;
            this.connection = connection;
            //Ustawienie ograniczeń dla daty urodzenia nowego pracownika.
            dateTimePickerBirthDate.MinDate = (DateTime.Today.AddYears(-100));
            dateTimePickerBirthDate.MaxDate = (DateTime.Today.AddYears(-16));
            dateTimePickerBirthDate.Value = (DateTime.Today.AddYears(-20));
            //Zczytanie listy możliwych do ustawienia uprawnień oraz pozycji pracownika.
            try
            {
                comboBoxPermissions.DataSource = this.GetPermissions();
                comboBoxPossition.DataSource = this.GetPositionsList();
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

            //todo usunąć dane wspomagające testy poniżej
            textBoxName.Text = "trol";
            textBoxSurname.Text = "trol";
            textBoxAddress.Text = "jaskinia";
            textBoxPesel.Text = "99999999999";
            textBoxEMail.Text = "asdf";
            textBoxNumberOfLeaveDays.Text = "22";
        }

        public FormAddOrEditEmployee(SqlConnection connection, int employeeId)
        {
            InitializeComponent();
            this.connection = connection;
            try
            {
                employeeToEdit = this.GetEmployee(employeeId);
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
            catch (EmployeeIdException)
            {
                MessageBox.Show("EmployeeID not found in database. This form will be close. Please try again later");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown exception has occured" + ex.Message);
                this.Close();
            }
            //przystosowanie odpowiednich elementów okna tak aby pasowały do funkcji związanych z edycją pracownika
            labelInfo.Text = "Here you can edit information about " + employeeToEdit.Name + " " + employeeToEdit.Surname;
            buttonAdd.Text = "Edit employee";
            //Ustawienie ograniczeń dla daty urodzenia nowego pracownika.
            dateTimePickerBirthDate.MinDate = (DateTime.Today.AddYears(-100));
            dateTimePickerBirthDate.MaxDate = (DateTime.Today.AddYears(-16));
            dateTimePickerBirthDate.Value = (DateTime.Today.AddYears(-20));
            //Zczytanie listy możliwych do ustawienia uprawnień oraz pozycji pracownika.
            comboBoxPermissions.DataSource = this.GetPermissions();
            comboBoxPossition.DataSource = this.GetPositionsList();


            comboBoxPermissions.SelectedItem = employeeToEdit.Permission;
            textBoxName.Text = employeeToEdit.Name;
            textBoxSurname.Text = employeeToEdit.Surname;
            textBoxAddress.Text = employeeToEdit.Address;
            textBoxPesel.Text = employeeToEdit.Pesel;
            textBoxEMail.Text = employeeToEdit.EMail;
            textBoxNumberOfLeaveDays.Text = employeeToEdit.LeaveDays.ToString();
            comboBoxPossition.SelectedItem = employeeToEdit.Position;
        }
        /// <summary>
        /// Metoda wywoływana w przypadku kliknięcia przycisku Cancel.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Metoda wywoływana w przypadku naciśnięcia przycisku klawiatury podczas
        /// gdy elementem zaznaczonym jest textBoxName. Odpowiada ona za ograniczenie
        /// rodzaju wprowadzanych znaków do tego pola.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Jeżeli znak jest nie odpowieni dla pola to pomiń jego obsługę.
            if (!char.IsLetter(e.KeyChar) 
                && !char.IsControl(e.KeyChar) 
                && !char.IsWhiteSpace(e.KeyChar)) 
                e.Handled = true;
        }

        /// <summary>
        /// Metoda wywoływana w przypadku naciśnięcia przycisku klawiatury podczas
        /// gdy elementem zaznaczonym jest textBoxSurname. Odpowiada ona za ograniczenie
        /// rodzaju wprowadzanych znaków do tego pola.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void textBoxSurname_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Jeżeli znak jest nie odpowieni dla pola to pomiń jego obsługę.
            if (!char.IsLetter(e.KeyChar) 
                && !char.IsControl(e.KeyChar) 
                && !char.IsWhiteSpace(e.KeyChar)) 
                e.Handled = true;
        }

        /// <summary>
        /// Metoda wywoływana w przypadku naciśnięcia przycisku klawiatury podczas
        /// gdy elementem zaznaczonym jest textBoxAddress. Odpowiada ona za ograniczenie
        /// rodzaju wprowadzanych znaków do tego pola.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void textBoxAddress_KeyPress(object sender, KeyPressEventArgs e)
        {             
            //Jeżeli znak jest nie odpowieni dla pola to pomiń jego obsługę.
            if (!e.KeyChar.Equals('-') 
                && !e.KeyChar.Equals('.') 
                && !char.IsLetterOrDigit(e.KeyChar)  
                && !char.IsWhiteSpace(e.KeyChar)
                && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        /// <summary>
        /// Metoda wywoływana w przypadku naciśnięcia przycisku klawiatury podczas
        /// gdy elementem zaznaczonym jest textBoxPesel. Odpowiada ona za ograniczenie
        /// rodzaju wprowadzanych znaków do tego pola.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void textBoxPesel_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Jeżeli znak jest nie odpowieni dla pola to pomiń jego obsługę.
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        /// <summary>
        /// Metoda wywoływana w przypadku naciśnięcia przycisku klawiatury podczas
        /// gdy elementem zaznaczonym jest textBoxEMail. Odpowiada ona za ograniczenie
        /// rodzaju wprowadzanych znaków do tego pola.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void textBoxEMail_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Jeżeli znak jest nie odpowieni dla pola to pomiń jego obsługę.
            if (!e.KeyChar.Equals('.')
               && !e.KeyChar.Equals('_')
               && !e.KeyChar.Equals('-')
               && !char.IsLetterOrDigit(e.KeyChar)
               && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        /// <summary>
        /// Metoda wywoływana w przypadku naciśnięcia przycisku klawiatury podczas
        /// gdy elementem zaznaczonym jest textBoxNumberOfLeaveDays. Odpowiada ona za ograniczenie
        /// rodzaju wprowadzanych znaków do tego pola.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void textBoxNumberOfLeaveDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Jeżeli znak jest nie odpowieni dla pola to pomiń jego obsługę.
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        /// <summary>
        /// Metoda wywoływana po przyciśnięciu guzika dodawania pracownika.
        /// Sprawdza poprawność wprowadzonych danych, a następnie (o ile dane
        /// są poprawne) dodaje nowego pracownika.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            /* Zmienna przechowująca informacje o błędnie wprowadzonych danych.
             * Jeżeli błąd zostanie wykryty odpowieni tekst dotyczący błędu zostanie 
             * dodany do tej zmiennej. Po odbyciu wszystkich testów sprawdzane jest, 
             * czy wystąpił jakiś błąd poprzez sprawdzenie długości tej zmiennej.
             * Długość == 0 oznacza, że żaden błąd nie wystąpił.
            */
            String errorString = "";
            if (textBoxName.Text.Length == 0)
                errorString += "- Name empty\n";
            if (textBoxName.Text.Length > 50)
                errorString += "- Name to long\n";

            if (textBoxSurname.Text.Length == 0)
                errorString += "- Surname empty\n";
            if (textBoxSurname.Text.Length > 50)
                errorString += "- Surname to long\n";

            if (textBoxAddress.Text.Length == 0)
                errorString += "- Address empty\n";
            if (textBoxAddress.Text.Length > 100)
                errorString += "- Address to long\n";

            if (textBoxPesel.Text.Length == 0)
                errorString += "- PESEL empty\n";
            if (textBoxPesel.Text.Length > 11)
                errorString += "- PESEL to long\n";

            if (textBoxEMail.Text.Length == 0)
                errorString += "- e-mail empty\n";
            if (textBoxEMail.Text.Length > 50)
                errorString += "- e-mail to long\n";

            /* Zmienna przechowująca informację o tym, ile dni urlopu na rok przysługuje
             * nowemu pracownikowi
             */
            int daysPerYear = 0;
            try
            {
                //Próba zrzutowania tekstu do zmiennej liczbowej.
                daysPerYear = System.Int32.Parse(textBoxNumberOfLeaveDays.Text);
            }
            catch
            {
                errorString += "- Wrong number of leave days per year";
            }

            //Sprawdzenie czy zmienna zawierająca informacje o błędach jest nie pusta.
            if (errorString.Length > 0)
                MessageBox.Show("Error! \n\n" + errorString);
            else
            {
                try
                {
                    //sprawdzenie czy pracownik ma zostać dodany czy zmieniony
                    if (employeeToEdit == null)
                    {
                        //Dodanie nowego pracownika.
                        this.AddEmployee(new Employee(-1, comboBoxPermissions.SelectedItem.ToString(), textBoxName.Text,
                            textBoxSurname.Text, dateTimePickerBirthDate.Value, textBoxAddress.Text,
                            textBoxPesel.Text, textBoxEMail.Text, comboBoxPossition.SelectedItem.ToString(),
                            daysPerYear, daysPerYear, 0, 0));
                    }
                    else
                    {
                        //Edycja danych pracownika
                        this.EditEmployee(new Employee(employeeToEdit.EmployeeId, comboBoxPermissions.SelectedItem.ToString(), textBoxName.Text,
                            textBoxSurname.Text, dateTimePickerBirthDate.Value, textBoxAddress.Text,
                            textBoxPesel.Text, textBoxEMail.Text, comboBoxPossition.SelectedItem.ToString(),
                            daysPerYear, employeeToEdit.LeaveDays, employeeToEdit.OldLeaveDays, employeeToEdit.DemandedDays));
                    }
                    this.Close();
                }
                catch (SqlException)
                {
                    MessageBox.Show("SQL error. Please try connection to database or try again later");
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Invalid operation. Please try again later.");
                }
                catch (IsolationLevelException)
                {
                    MessageBox.Show("Isolation level error. Please try again later or contact administrator");
                }
                catch (NoFreeLoginException)
                {
                    MessageBox.Show("All logins are currently occupy. Please try again later or contact administrator");
                }
                catch (PermissionException)
                {
                    MessageBox.Show("Given permission type does not exist.");
                }
                catch (PositionException)
                {
                    MessageBox.Show("Given position type does not exist.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unknown exception has occured" + ex.Message);
                }
            }
        }           
    }
}
