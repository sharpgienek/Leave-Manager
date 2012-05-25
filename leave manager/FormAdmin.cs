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
            refreshDataGridViewPositions();
        }

        private void refreshDataGridViewLeaveTypes(object o, FormClosedEventArgs e)
        {
            refreshDataGridViewLeaveTypes();
        }

        private void refreshDataGridViewLeaveTypes()
        {
            dataGridViewLeaveTypes.Rows.Clear();
            dataGridViewLeaveTypes.Columns.Clear();
            List<LeaveType> leaveTypes = Dictionary.GetLeaveTypes(connection);
            dataGridViewLeaveTypes.Columns.Add("columnLeaveTypeId", "Type id");
            dataGridViewLeaveTypes.Columns.Add("columnLeaveTypeName", "Name");
            dataGridViewLeaveTypes.Columns.Add("columnLeaveTypeConsumesDays", "Consumes days");
            dataGridViewLeaveTypes.Rows.Add(leaveTypes.Count);
            for (int i = 0; i < leaveTypes.Count; i++)
            {
                dataGridViewLeaveTypes.Rows[i].Cells[0].Value = i;
                dataGridViewLeaveTypes.Rows[i].Cells[1].Value = leaveTypes[i].Name;
                dataGridViewLeaveTypes.Rows[i].Cells[2].Value = leaveTypes[i].ConsumesDays;
            }
        }

        private void refreshDataGridViewPositions(object o, FormClosedEventArgs e)
        {
            refreshDataGridViewPositions();
        }

        private void refreshDataGridViewPositions()
        {
            dataGridViewPositions.Rows.Clear();
            dataGridViewPositions.Columns.Clear();
            List<String> positions = Dictionary.GetPositions(connection);
            dataGridViewPositions.Columns.Add("columnPositionId", "Position id");
            dataGridViewPositions.Columns.Add("columnDescription", "Name");
            dataGridViewPositions.Rows.Add(positions.Count);
            for (int i = 0; i < positions.Count; i++)
            {
                dataGridViewPositions.Rows[i].Cells[0].Value = i;
                dataGridViewPositions.Rows[i].Cells[1].Value = positions[i];
            }
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
                command.ExecuteNonQuery();//todo transaction
            }
            refreshDataGridViewNewEmployees();
        }

        private void tabControlDictionaries_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControlDictionaries.SelectedTab.Text.Equals("Positions"))
                refreshDataGridViewPositions();
            if (tabControlDictionaries.SelectedTab.Text.Equals("Leave types"))
                refreshDataGridViewLeaveTypes();
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab.Text.Equals("Dictionaries"))
            {
                if (tabControlDictionaries.SelectedTab.Text.Equals("Positions"))
                    refreshDataGridViewPositions();
                if (tabControlDictionaries.SelectedTab.Text.Equals("Leave types"))
                    refreshDataGridViewLeaveTypes();
            }
            if (tabControl.SelectedTab.Text.Equals("New Employees"))
                refreshDataGridViewNewEmployees();
        }

        private void buttonAddNewPosition_Click(object sender, EventArgs e)
        {
            FormAddPosition form = new FormAddPosition(connection);
            form.FormClosed += new FormClosedEventHandler(refreshDataGridViewPositions);
            form.Show();
        }

        private void buttonDeleteEntry_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell cell in dataGridViewPositions.SelectedCells)
            {
                dataGridViewPositions.Rows[cell.RowIndex].Selected = true;
            }
            if (dataGridViewPositions.SelectedRows.Count != 0)
            {
                if (dataGridViewPositions.SelectedRows.Count == 1)
                {
                    FormDeletePosition form = new FormDeletePosition(connection, 
                        dataGridViewPositions.SelectedRows[0].Cells[1].Value.ToString());
                    form.FormClosed += new FormClosedEventHandler(refreshDataGridViewPositions);
                    form.Show();
                }
                else
                {
                    MessageBox.Show("You may delete only one position at a time.");
                }
            }
            else
            {
                MessageBox.Show("No position selected.");
            }

        }

        private void buttonAddLeaveType_Click(object sender, EventArgs e)
        {
            FormAddLeaveType form = new FormAddLeaveType(connection);
            form.FormClosed += new FormClosedEventHandler(refreshDataGridViewLeaveTypes);
            form.Show();
        }

        private void buttonDeleteLeaveType_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell cell in dataGridViewLeaveTypes.SelectedCells)
            {
                dataGridViewLeaveTypes.Rows[cell.RowIndex].Selected = true;
            }
            if (dataGridViewLeaveTypes.SelectedRows.Count != 0)
            {
                if (dataGridViewLeaveTypes.SelectedRows.Count == 1)
                {
                    FormDeleteLeaveType form = new FormDeleteLeaveType(connection,
                        dataGridViewLeaveTypes.SelectedRows[0].Cells[1].Value.ToString());
                    form.FormClosed += new FormClosedEventHandler(refreshDataGridViewLeaveTypes);
                    form.Show();
                }
                else
                {
                    MessageBox.Show("You may delete only one position at a time.");
                }
            }
            else
            {
                MessageBox.Show("No position selected.");
            }
        }
    }
}
