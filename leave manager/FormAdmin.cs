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
            refreshDataGridViewNewEmployees();
        }

        private void refreshDataGridViewNewEmployees()
        {
            SqlCommand command = new SqlCommand("SELECT E.Employee_ID AS 'ID', E.EMail AS 'e-mail', E.Login, U.Password, E.Name, " +
                "E.Surname FROM Employee E, Uninformed U WHERE E.Employee_ID = U.Employee_ID", connection);
            SqlDataReader reader = command.ExecuteReader();//todo try catch
            DataTable newEmployees = new DataTable();
            newEmployees.Load(reader);
            reader.Close();
            dataGridViewNewEmployees.DataSource = newEmployees;
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

        private void buttonNewEmployeesInformed_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell cell in dataGridViewNewEmployees.SelectedCells)
            {
                dataGridViewNewEmployees.Rows[cell.RowIndex].Selected = true;
            }
            SqlCommand command = new SqlCommand("DELETE FROM Uninformed WHERE Employee_ID = @Employee_ID", connection);
            foreach (DataGridViewRow row in dataGridViewNewEmployees.SelectedRows)
            {
                command.Parameters.Clear();
                command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = row.Cells["ID"].Value;
                command.ExecuteNonQuery();//todo try catch
            }
            refreshDataGridViewNewEmployees();
        }
    }
}
