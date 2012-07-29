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
    public partial class FormAddPublicHoliday : LeaveManagerForm
    {
        public FormAddPublicHoliday(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            dateTimePicker.MinDate = DateTime.Now;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                this.AddPublicHoliday(dateTimePicker.Value);
                this.Close();
            }
            catch (EntryExistsException)
            {
                MessageBox.Show("This date is already in the database.");
            }
            catch//todo obsługa wyjątków.
            {
                throw new NotImplementedException();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
