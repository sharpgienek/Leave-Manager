using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace leave_manager
{
    public partial class FormDefineDatabase : Form
    {
        private OpenFileDialog browseDialog;
        private String connectionString;
        public String ConnectionString
        {
            get { return connectionString; }
        }
        public FormDefineDatabase()
        {
            InitializeComponent();
        }

        private void radioButtonLocal_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonLocal.Checked)
            {
                groupBoxLocal.Visible = true;
                groupBoxRemote.Visible = false;
            }
            else
            {
                groupBoxRemote.Visible = true;
                groupBoxLocal.Visible = false;
            }
        }        

        private void buttonTestConnection_Click(object sender, EventArgs e)
        {
            if (radioButtonLocal.Checked)
                if (!textBoxLocalPath.Text.Equals(""))
                {
                    if (checkConnection(new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=" + "\"" + textBoxLocalPath.Text + "\"" + ";Integrated Security=True;User Instance=True;")))
                        MessageBox.Show("Test went ok.");
                    else
                        MessageBox.Show("Connection can not be established.");
                }
                else
                    MessageBox.Show("Empty Path!");//todo ładny tekst
                else
                    throw new NotImplementedException();
        }

        private void buttonLocalBrowse_Click(object sender, EventArgs e)
        {
            if (browseDialog == null)
            {
                browseDialog = new OpenFileDialog();
            }

            if (browseDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxLocalPath.Text = browseDialog.FileName;
            }
        }

      static public bool checkConnection(SqlConnection con)
        {
            con.Close();
             try
                {          
                    con.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Uninformed", con))
                    {
                        command.ExecuteNonQuery();
                    }
                    con.Close();
                    return true;
                }
                catch (Exception)
                {
                    con.Close();
                    return false;
                }
                
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            if (radioButtonLocal.Checked)
                if (!textBoxLocalPath.Text.Equals(""))
                {
                    if (checkConnection(new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=" + "\"" + textBoxLocalPath.Text + "\"" + ";Integrated Security=True;User Instance=True;")))
                    {
                        using (StreamWriter outfile = new StreamWriter(Path.GetDirectoryName(Application.ExecutablePath) + @"\config.ini"))
                        {
                            outfile.Write(@"Data Source=.\SQLEXPRESS;AttachDbFilename=" + "\"" + textBoxLocalPath.Text + "\"" + ";Integrated Security=True;User Instance=True;");
                            connectionString += @"Data Source=.\SQLEXPRESS;AttachDbFilename=" + "\"" + textBoxLocalPath.Text + "\"" + ";Integrated Security=True;User Instance=True;";
                            this.Close();
                        }
                    }
                    else
                        MessageBox.Show("Connection can not be established.");
                }
                else
                    MessageBox.Show("Empty Path!");//todo ładny tekst
            else
                throw new NotImplementedException(); 
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            connectionString = "";
            this.Close();
        }

       
       
    }
}
