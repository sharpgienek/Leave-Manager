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
    public partial class FormChangeLoginOrPassword : Form
    {
        private DatabaseOperator databaseOperator;
        private int employeeId;
        public FormChangeLoginOrPassword(DatabaseOperator databaseOperator, int employeeId)
        {
            InitializeComponent();
            this.databaseOperator = databaseOperator;
            this.employeeId = employeeId;
        }
        //todo więcej i ładniejsze komunikaty błędów.
        private void buttonAccept_Click(object sender, EventArgs e)
        {
            if (textBoxNewPassword.Text.Equals(textBoxRepeatNewPassword.Text))
            {
                if (databaseOperator.ChangeLoginOrPassword(employeeId, textBoxOldPassword.Text, textBoxNewPassword.Text, textBoxNewLogin.Text))
                {
                    MessageBox.Show("Operation compleated successfuly.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error occured. Plese retype your old password and try again.");
                }
            }
            else
            {
                MessageBox.Show("Repeated password is not equal to new password. No changes will be made.");
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }




    }
}
