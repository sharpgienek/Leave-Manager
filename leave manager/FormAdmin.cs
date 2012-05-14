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
    public partial class FormAdmin : Form
    {
        private SqlConnection connection;
        public FormAdmin(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
        }
       
        private void radioButtonDataSourceLocal_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonDataSourceLocal.Checked)
            {
                groupBoxDataSourceLocal.Visible = true;
                groupBoxDataSourceRemote.Visible = false;
            }
            else
            {
                groupBoxDataSourceRemote.Visible = true;
                groupBoxDataSourceLocal.Visible = false;
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

        private void buttonDataSourceLocalBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog browseDialog = new OpenFileDialog();
            if (browseDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxDataSourceLocalPath.Text = browseDialog.FileName;
            }
        }

        private void buttonDataSourceTestConnection_Click(object sender, EventArgs e)
        {
            if (radioButtonDataSourceLocal.Checked)
                if (!textBoxDataSourceLocalPath.Text.Equals(""))
                {
                    if (checkConnection(new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=" + "\"" + textBoxDataSourceLocalPath.Text + "\"" + ";Integrated Security=True;User Instance=True;")))
                        MessageBox.Show("Test went ok.");
                    else
                        MessageBox.Show("Connection can not be established.");
                }
                else
                    MessageBox.Show("Empty Path!");//todo ładny tekst
            else
                throw new NotImplementedException();
        }

        private void buttonDataSourceAccept_Click(object sender, EventArgs e)
        {
            if (radioButtonDataSourceLocal.Checked)
                if (!textBoxDataSourceLocalPath.Text.Equals(""))
                {
                    if (checkConnection(new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=" + "\"" + textBoxDataSourceLocalPath.Text + "\"" + ";Integrated Security=True;User Instance=True;")))
                    {
                        using (StreamWriter outfile = new StreamWriter(Path.GetDirectoryName(Application.ExecutablePath) + @"\config.ini"))
                        {
                            outfile.Write(@"Data Source=.\SQLEXPRESS;AttachDbFilename=" + "\"" + textBoxDataSourceLocalPath.Text + "\"" + ";Integrated Security=True;User Instance=True;");
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

    }
}
