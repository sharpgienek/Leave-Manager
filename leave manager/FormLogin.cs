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
        private DatabaseOperator databaseOperator;
        private Employee employee;
        public Employee Employee { get { return employee; } set { employee = value; } }
        private bool loggedIn;
        public bool LoggedIn { get { return loggedIn; } }

        public FormLogin(DatabaseOperator databaseOperator)
        {
            InitializeComponent();
            this.databaseOperator = databaseOperator;
            this.loggedIn = false;
          //  textBoxLogin.Text = StringSha.GetSha256Managed("employee");
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {            
            loggedIn = databaseOperator.login(textBoxLogin.Text, textBoxPassword.Text, ref employee);
            if (loggedIn)
                this.Close();
            else
                MessageBox.Show("Wrong login or password!");
        }
    }
}
