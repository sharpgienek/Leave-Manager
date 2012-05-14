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
    public partial class FormLogin : Form
    {
        private SqlConnection connection;
        private EmployeeLoggedIn employee;
        public EmployeeLoggedIn Employee { get { return employee; } set { employee = value; } }
        public FormLogin(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT Employee_ID, Permissions, Name, Surname," +
                                                "Role, Leave_days, Old_leave_days FROM Employee WHERE Login = '" +
                                                textBoxLogin.Text + "' AND Password = '" +
                                                textBoxPassword.Text + "'", connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                employee = new EmployeeLoggedIn((int)reader["Employee_ID"], reader["Permissions"].ToString()[0],
                   reader["Name"].ToString(), reader["Surname"].ToString(), reader["Role"].ToString(), (int)reader["Leave_days"], (int)reader["Old_leave_days"]);
                this.Close();
            }
            else
                MessageBox.Show("Wrong login or password!");
            reader.Close();
        }
    }
}
