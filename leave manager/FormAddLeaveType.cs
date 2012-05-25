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
    //todo ograniczenia w tekście nazwy.
    public partial class FormAddLeaveType : Form
    {
        private SqlConnection connection;
        public FormAddLeaveType(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (!textBoxName.Text.Equals(""))
            {
                SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.RepeatableRead);//todo try catch'e
                SqlCommand commandCheckIfExists = new SqlCommand("SELECT LT_ID " +
                    "FROM Leave_type WHERE Name = @Name", connection, transaction);
                commandCheckIfExists.Parameters.Add("@Name", SqlDbType.VarChar).Value = textBoxName.Text;
                SqlDataReader reader = commandCheckIfExists.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();
                    transaction.Rollback();                    
                    MessageBox.Show("This type already exists.");
                    return;
                }
                reader.Close();
                SqlCommand command = new SqlCommand("INSERT INTO Leave_type " +
                    "VALUES((SELECT MAX(LT_ID) + 1 FROM Leave_type), @Name, @Consumes_days)", connection, transaction);
                command.Parameters.Add("@Name", SqlDbType.VarChar).Value = textBoxName.Text;
                if (checkBoxConsumesDays.Checked)
                {
                    command.Parameters.Add("@Consumes_days", SqlDbType.Bit).Value = true;
                }
                else
                {
                    command.Parameters.Add("@Consumes_days", SqlDbType.Bit).Value = false;
                }
                command.ExecuteNonQuery();//todo try catch;
                transaction.Commit();
                this.Close();
            }
            else
            {
                MessageBox.Show("Name field empty.");
            }
        }
    }
}
