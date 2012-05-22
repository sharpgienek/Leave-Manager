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
        private SqlConnection connection;
        private int employeeId;
        public FormChangeLoginOrPassword(SqlConnection connection, int employeeId)
        {
            InitializeComponent();
            this.connection = connection;
            this.employeeId = employeeId;
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT Employee_ID FROM Employee WHERE " +
                "Employee_ID = @Employee_ID AND Password = @Password", connection);
            command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
            command.Parameters.Add("@Password", SqlDbType.VarChar).Value = StringSha.GetSha256Managed(textBoxOldPassword.Text);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                if (textBoxNewLogin.Text.Length != 0 && textBoxNewPassword.Text.Length != 0)
                {
                    if (textBoxNewPassword.Text.Equals(textBoxRepeatNewPassword.Text))
                    {
                        command.CommandText = "UPDATE Employee SET Login = @Login, " +
                            "Password = @Password WHERE Employee_ID = @Employee_ID";
                        command.Parameters.Clear();
                        command.Parameters.Add("@Login", SqlDbType.VarChar).Value = textBoxNewLogin.Text;
                        command.Parameters.Add("@Password", SqlDbType.VarChar).Value = StringSha.GetSha256Managed(textBoxNewPassword.Text);
                        command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
                        command.ExecuteNonQuery();//todo try catch
                        textBoxNewLogin.Text = "";
                        textBoxNewPassword.Text = "";
                        textBoxRepeatNewPassword.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Repeated password is not equal to new password. No changes will be made.");
                    }
                }
                else
                {
                    if (textBoxNewLogin.Text.Length != 0)
                    {
                        command.CommandText = "UPDATE Employee SET Login = @Login WHERE Employee_ID = @Employee_ID";
                        command.Parameters.Clear();
                        command.Parameters.Add("@Login", SqlDbType.VarChar).Value = textBoxNewLogin.Text;
                        command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
                        command.ExecuteNonQuery(); //todo try catch
                        textBoxNewLogin.Text = "";
                    }

                    if (textBoxNewPassword.Text.Length != 0)
                    {
                        if (textBoxNewPassword.Text.Equals(textBoxRepeatNewPassword.Text))
                        {
                            command.CommandText = "UPDATE Employee SET Password = @Password WHERE Employee_ID = @Employee_ID";
                            command.Parameters.Clear();
                            command.Parameters.Add("@Password", SqlDbType.VarChar).Value = StringSha.GetSha256Managed(textBoxNewPassword.Text);
                            command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
                            command.ExecuteNonQuery(); //todo try catch
                            textBoxNewPassword.Text = "";
                            textBoxRepeatNewPassword.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Repeated password is not equal to new password. No changes will be made.");
                        }
                    }
                }
            }
            else
            {
                reader.Close();
                MessageBox.Show("Old password is incorrect.");
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }




    }
}
