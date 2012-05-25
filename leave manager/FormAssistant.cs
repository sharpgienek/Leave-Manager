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
    public partial class FormAssistant : Form
    {
        private SqlConnection connection;

        public FormAssistant(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            refreshDataGridViewPendingAplications();
            loadAllDataGridViewEmployees();
        }

        private void refreshDataGridViewPendingAplications(object o, FormClosedEventArgs e)
        {
            refreshDataGridViewPendingAplications();
        }

        private void loadAllDataGridViewEmployees()
        {
            SqlCommand command = new SqlCommand("SELECT E.Employee_ID AS 'Employee ID', " +
                "Pos.Description AS 'Position', E.Name, E.Surname, " +
                "E.EMail AS 'e-mail', E.Birth_date AS 'Birth date', E.PESEL, E.Address, E.Leave_days, E.Old_leave_days FROM Position Pos, Employee E " +
                "WHERE E.Position_ID = Pos.Position_ID", connection);
            SqlDataReader reader = command.ExecuteReader(); //todo try catch
            DataTable data = new DataTable();
            data.Load(reader);
            dataGridViewEmployees.DataSource = data;
            dataGridViewEmployees.Columns["Leave_days"].Visible = false;
            dataGridViewEmployees.Columns["Old_leave_days"].Visible = false;
        }

        private void refreshDataGridViewPendingAplications()
        {
            SqlCommand command = new SqlCommand("SELECT E.Employee_ID AS 'Employee id', P.Description AS 'Position', " +
                "E.Name, E.Surname, E.EMail AS 'e-mail', LT.Name AS 'Type', " +
                "L.First_day AS 'First day', L.Last_day AS 'Last day' FROM Employee E, " +
                "Leave L, Leave_type LT, Position P, Status_type LS WHERE L.Employee_ID = E.Employee_ID " +
                "AND L.LT_ID = LT.LT_ID AND P.Position_ID = E.Position_ID AND LS.ST_ID = L.LS_ID AND LS.Name = @Name ORDER BY L.First_day", connection);
            command.Parameters.Add("@Name", SqlDbType.VarChar).Value = "Pending validation";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            reader.Close();
            table.Columns.Add("No. work days");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                table.Rows[i]["No. work days"] = TimeTools.GetNumberOfWorkDays((DateTime)table.Rows[i]["First day"], (DateTime)table.Rows[i]["Last day"]);
            }
            dataGridViewPendingAplications.DataSource = table;
        }

        private void buttonConsiderPendingAplication_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell cell in dataGridViewPendingAplications.SelectedCells)
            {
                dataGridViewPendingAplications.Rows[cell.RowIndex].Selected = true;
            }
            foreach (DataGridViewRow row in dataGridViewPendingAplications.SelectedRows)
            {
                FormLeaveConsideration form = new FormLeaveConsideration(connection,
                   (int)row.Cells["Employee id"].Value, (DateTime)row.Cells["First day"].Value,
                   (DateTime)row.Cells["Last day"].Value, "Pending manager aproval");
                form.FormClosed += new FormClosedEventHandler(refreshDataGridViewPendingAplications);
                form.Show();
            }
        }

        private void buttonEmployeesShowAll_Click(object sender, EventArgs e)
        {
            loadAllDataGridViewEmployees();
        }

        private void buttonEmployeesDetailedData_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell cell in dataGridViewEmployees.SelectedCells)
            {
                dataGridViewEmployees.Rows[cell.RowIndex].Selected = true;
            }
            foreach (DataGridViewRow row in dataGridViewEmployees.SelectedRows)
            {
                FormEmployeeData form = new FormEmployeeData(connection, (int)row.Cells[0].Value,
                    row.Cells["Name"].Value.ToString(), row.Cells["Surname"].Value.ToString(),
                    row.Cells["Position"].Value.ToString(), (int)row.Cells["Leave_days"].Value,
                    (int)row.Cells["Old_leave_days"].Value);
                form.FormClosed += new FormClosedEventHandler(refreshDataGridViewPendingAplications);
                form.Show();
            }           
        }      
    }
}
