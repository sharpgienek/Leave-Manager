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
    /// Klasa formularza usuwania typu urlopu.
    /// </summary>
    public partial class FormDeleteLeaveType : LeaveManagerForm
    {    
        /// <summary>
        /// Atrybut przechowujący nazwę usuwanego typu.
        /// </summary>
        private String deletedLeaveType;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connection">Połączenie z bazą danych. Powinno być otwarte.</param>
        /// <param name="deletedLeaveType">Nazwa usuwanego typu.</param>
        public FormDeleteLeaveType(SqlConnection connection, String deletedLeaveType)
        {
            InitializeComponent();
            this.connection = connection;
            this.deletedLeaveType = deletedLeaveType;
            //Zczytanie listy typów urlopów.
            List<LeaveType> leaveTypes = this.GetLeaveTypesList();
            //Usunięcie z tej listy usuwanego typu.
            leaveTypes.Remove(new LeaveType(deletedLeaveType));
            /* Za pomocą elementu comboBoxLeaveTypes użytkownik wybiera jaki typ
             * urlopu ma zostać przypisany do tych wszystkich urlopów, których typ
             * jest usuwany. Poniżej przypisana do niego zostaje lista typów,
             * które mogą być do tego celu użyte.
             */
            comboBoxLeaveTypes.DataSource = leaveTypes;  
        }      

        /// <summary>
        /// Metoda obsługująca kliknięcie w przycisk anulujący
        /// usuwanie typu urlopu. Zamyka formularz.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Metoda obsługująca kliknięcie w przycisk OK. Kasuje dany typ.
        /// Typ wszystkich urlopów, które były typu kasowanego są zamieniane
        /// na typ wybrany w combo box'ie.
        /// </summary>
        /// <param name="sender">Obiekt wysyłąjący.</param>
        /// <param name="e">Argumenty.</param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                this.DeleteLeaveType(comboBoxLeaveTypes.SelectedItem.ToString(), deletedLeaveType);
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
