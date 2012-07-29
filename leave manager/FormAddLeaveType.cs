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
    //todo ograniczenia w tekście nazwy.
    /// <summary>
    /// Klasa formularza dodawania nowego typu urlopu.
    /// </summary>
    public partial class FormAddLeaveType : LeaveManagerForm
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connection">Połączenie z bazą danych. Powinno być otwarte.</param>
        public FormAddLeaveType(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
        }

        /// <summary>
        /// Metoda wywoływana w przypadku naciśnięcia guzika anulowania.
        /// Zamyka formularz.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Metoda wywoływana w przypadku naciśnięcia guzika dodawania nowego typu urlopu.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //Sprawdzenie, czy nazwa nowego typu nie jest pusta.
            if (textBoxName.Text.Length != 0)
            {
                try
                {
                    //Dodanie nowego typu.
                    this.AddLeaveType(textBoxName.Text, checkBoxConsumesDays.Checked);
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
                catch (EntryExistsException)
                {
                    MessageBox.Show("Entry of this type already exist in database");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unknown exception has occured" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Name field empty.");
            }
        }
    }
}
