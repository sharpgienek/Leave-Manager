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
        private Employee employee;
        public Employee Employee { get { return employee; } set { employee = value; } }
        private bool loggedIn;
        public bool LoggedIn { get { return loggedIn; } }

        public FormLogin(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            this.loggedIn = false;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT E.Employee_ID, Perm.Description AS Permission, E.Name, E.Surname, E.Birth_date," +
                                                "E.Address, E.PESEL, E.EMail, Pos.Description AS Position, E.Year_leave_days, "+
                                                "E.Leave_days, E.Old_leave_days " +
                                                "FROM Employee E, Permission Perm, Position Pos WHERE Login = @Login AND " +
                                                "Password = @Password AND E.Permission_ID = Perm.Permission_ID AND " +
                                                "E.Position_ID = Pos.Position_ID", connection);
            command.Parameters.Add("@Login", SqlDbType.VarChar).Value = textBoxLogin.Text;
            command.Parameters.Add("@Password", SqlDbType.VarChar).Value = StringSha.GetSha256Managed(textBoxPassword.Text);
        
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                employee = new Employee((int)reader["Employee_ID"], reader["Permission"].ToString(),
                   reader["Name"].ToString(), reader["Surname"].ToString(), (DateTime)reader["Birth_date"],
                   reader["Address"].ToString(), reader["PESEL"].ToString(), reader["EMail"].ToString(), 
                   reader["Position"].ToString(),(int)reader["Year_leave_days"], (int)reader["Leave_days"], 
                   (int)reader["Old_leave_days"]);
                loggedIn = true;
                this.Close();                
            }
            else
                MessageBox.Show("Wrong login or password!");
            reader.Close();
        }
    }
}
