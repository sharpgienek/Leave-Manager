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
        private SqlConnection con;
        public FormLogin()
        {
            InitializeComponent();
         //   con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=" + "\"" + @"C:\Users\sharpy\documents\visual studio 2010\Projects\leave manager\leave manager\Database.mdf" + "\"" + ";Integrated Security=True;User Instance=True;");
            con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=" + "\"" + @"C:\Users\sharpy\documents\visual studio 2010\Projects\leave manager\leave manager\Database.mdf" + "\"" + ";Integrated Security=True;User Instance=True;");
            //
            con.Open();
        }
        
        private void buttonLoginLogin_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlCommand command = new SqlCommand(
                "CREATE TABLE Dogs1 (Weight INT, Name TEXT, Breed TEXT)", con))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch
            {
                MessageBox.Show("Table couldn't be created.");
            }
            con.Close();
        }
    }
}
