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
    public partial class FormAddPosition : Form
    {
        private SqlConnection connection;
        public FormAddPosition(SqlConnection connection)
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
                SqlCommand commandCheckIfExists = new SqlCommand("SELECT Position_ID " +
                    "FROM Position WHERE Description = @Description", connection, transaction);
                commandCheckIfExists.Parameters.Add("@Description", SqlDbType.VarChar).Value = textBoxName.Text;
                SqlDataReader reader = commandCheckIfExists.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();
                    transaction.Rollback();                    
                    MessageBox.Show("This position already exists.");
                    return;
                }
                reader.Close();
                SqlCommand command = new SqlCommand("INSERT INTO Position " +
                    "VALUES((SELECT MAX(Position_ID) + 1 FROM Position), @Name)", connection, transaction);
                command.Parameters.Add("@Name", SqlDbType.VarChar).Value = textBoxName.Text;
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
