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
    /// Klasa formularza kasowania typu pozycji ze słownika pozycji.
    /// </summary>
    public partial class FormDeletePosition : LeaveManagerForm
    {

        /// <summary>
        /// Atrybut przechowujący nazwę usuwanego typu.
        /// </summary>
        private String deletedPosition;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connection">Połączenie z bazą danych. Powinno być otwarte.</param>
        /// <param name="deletedLeaveType">Nazwa usuwanego typu.</param>
        public FormDeletePosition(SqlConnection connection, String replacedPosition)
        {
            this.connection = connection;
            this.deletedPosition = replacedPosition;
            InitializeComponent();
            //Zczytanie listy typów pozycji.
            List<String> positions = this.GetPositionsList();
            //Usunięcie z tej listy usuwanego typu.
            positions.Remove(replacedPosition);
            /* Za pomocą elementu comboBoxLeaveTypes użytkownik wybiera jaki typ
             * pozycji ma zostać przypisany do tych wszystkich pracowników, których typ
             * pozycji jest usuwany. Poniżej przypisana do niego zostaje lista typów,
             * które mogą być do tego celu użyte.
             */
            comboBoxPositions.DataSource = positions;
        }

        /// <summary>
        /// Metoda obsługująca kliknięcie w przycisk anulujący
        /// usuwanie typu pozycji. Zamyka formularz.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Metoda obsługująca kliknięcie w przycisk OK. Kasuje dany typ.
        /// Typ wszystkich pracowników, którzy zajmowali kasowaną pozycję 
        /// zostaje przypisana pozycja wybrana za pomocą combo box'a.
        /// </summary>
        /// <param name="sender">Obiekt wysyłąjący.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                this.DeletePosition(comboBoxPositions.SelectedItem.ToString(), deletedPosition);
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
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Wrong argument\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown exception has occured" + ex.Message);
            }
        }       
    }
}
