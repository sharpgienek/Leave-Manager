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
    /// Klasa formularza usuwania typu urlopu.
    /// </summary>
    public partial class FormDeleteLeaveType : LeaveManagerForm
    {
        /// <summary>
        /// Lista typów urlopów.
        /// </summary>
        List<LeaveType> leaveTypes;

        /// <summary>
        /// Atrybut przechowujący nazwę usuwanego typu.
        /// </summary>
        private LeaveType deletedLeaveType;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connection">Połączenie z bazą danych. Powinno być otwarte.</param>
        /// <param name="deletedLeaveType">Usuwany typ urlopu.</param>
        public FormDeleteLeaveType(SqlConnection connection, LeaveType deletedLeaveType)
        {
            InitializeComponent();
            this.connection = connection;
            this.deletedLeaveType = deletedLeaveType;
            //Zczytanie listy typów urlopów.
            leaveTypes = this.GetLeaveTypesList();
            //Usunięcie z tej listy usuwanego typu.
            leaveTypes.Remove(deletedLeaveType);
            /* Za pomocą elementu comboBoxLeaveTypes użytkownik wybiera jaki typ
             * urlopu ma zostać przypisany do tych wszystkich urlopów, których typ
             * jest usuwany. Poniżej przypisana do niego zostaje lista typów,
             * które mogą być do tego celu użyte.
             */
            comboBoxLeaveTypes.DataSource = leaveTypes;
            radioButtonReplace.Checked = true;
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
            //Jeżeli zamieniamy typ.
            if (radioButtonReplace.Checked)
            {
                //Jeżeli typy nie mają konfliktu na poziomie konsumowania dni.
                if (this.leaveTypes[comboBoxLeaveTypes.SelectedIndex].ConsumesDays == deletedLeaveType.ConsumesDays)
                {
                    try
                    {
                        this.DeleteLeaveType(comboBoxLeaveTypes.SelectedItem.ToString(), deletedLeaveType.Name);
                        this.Close();
                    }
                    catch//todo obsługa wszystkich wyjątków.
                    {
                        throw new NotImplementedException();
                    }
                }
                else
                {
                    if (deletedLeaveType.ConsumesDays)
                        MessageBox.Show("You can't replace type that consumes days with type that doesn't consume days.");
                    else
                        MessageBox.Show("You can't replace type that doesn't consume days with type that consumes days.");
                }
            }
            else
            {
                try
                {
                    this.DeleteLeaveType(deletedLeaveType);
                    this.Close();
                }
                catch//todo obsługa wszystkich wyjątków.
                {
                    throw new NotImplementedException();
                }
            }
        }

        private void radioButtonReplace_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonReplace.Checked)
            {
                comboBoxLeaveTypes.Enabled = true;
            }
            else
            {
                comboBoxLeaveTypes.Enabled = false;
            }
        }

    }
}
