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
    public partial class FormManager : Form
    {
        private SqlConnection connection;

      //  public FormManager()
       // { }

        public FormManager(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            refreshDataGridViewNeedsAction();
        }

        private void refreshDataGridViewNeedsAction(object o, FormClosedEventArgs e)
        {
            refreshDataGridViewNeedsAction();
        }

        private void refreshDataGridViewNeedsAction()
        {
           /* SqlCommand commandUpdateLeave = new SqlCommand("SELECT E.Employee_ID AS 'Employee id', " +
                "E.Name, E.Surname, Pos.Description AS 'Position', LT.Name AS 'Type', " +
                "L.First_day AS 'First day', L.Last_day AS 'Last day' FROM Employee E," +
                "Position Pos, Leave_type LT, Leave L WHERE E.Employee_ID = L.Employee_ID AND " +
                "L.LT_ID = LT.LT_ID AND E.Position_ID = Pos.Position_ID ", connection);*/
            SqlCommand command = new SqlCommand("SELECT E.Employee_ID AS 'Employee id', P.Description AS 'Position', " +
                "E.Name, E.Surname, E.EMail AS 'e-mail', LT.Name AS 'Type', " +
                "L.First_day AS 'First day', L.Last_day AS 'Last day' FROM Employee E, " +
                "Leave L, Leave_type LT, Position P, Status_type LS WHERE L.Employee_ID = E.Employee_ID " +
                "AND L.LT_ID = LT.LT_ID AND P.Position_ID = E.Position_ID AND LS.ST_ID = L.LS_ID AND LS.Name = @Name ORDER BY L.First_day", connection);
           // commandUpdateLeave.Parameters.Add("@Name", SqlDbType.VarChar).Value = "Pending validation";
            command.Parameters.Add("@Name", SqlDbType.VarChar).Value = "Pending manager aproval";
            
            SqlDataReader reader = command.ExecuteReader();
            DataTable data = new DataTable();
            data.Load(reader);
            reader.Close();
            data.Columns.Add("No. used work days");
            for (int i = 0; i < data.Rows.Count; i++)
            {
                data.Rows[i]["No. used work days"] = TimeTools.GetNumberOfWorkDays(
                    (DateTime)data.Rows[i]["First day"], (DateTime)data.Rows[i]["Last day"]);
            }
            dataGridViewNeedsAction.DataSource = data;
        }

        private void refreshDataGridViewEmployees(Object o, EventArgs e)
        {
            refreshDataGridViewEmployees();
        }

        private void refreshDataGridViewEmployees()
        {
            SqlCommand command = new SqlCommand("SELECT E.Name, E.Surname, E.Birth_date AS 'Birth date'," +
                "E.Address, E.PESEL, E.EMail AS 'e-mail', Pos.Description AS Position, " +
                "Perm.Description AS Permission, E.Leave_days AS 'Remaining leave days', "+
                "E.Old_leave_days AS 'Old left leave days' " +
                "FROM Employee E, Position Pos, Permission Perm " +
                "WHERE E.Permission_ID = Perm.Permission_ID " +
                "AND E.Position_ID = Pos.Position_ID", connection);

            SqlDataReader reader = command.ExecuteReader();
            DataTable employees = new DataTable();
            employees.Load(reader);
            reader.Close();
            dataGridViewEmployees.DataSource = employees;
        }

        private void buttonEmployeesAdd_Click(object sender, EventArgs e)
        {
            FormAddEmployee form = new FormAddEmployee(connection);
            form.Show();
            form.FormClosed += new FormClosedEventHandler(this.refreshDataGridViewEmployees);
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab.Text.Equals("Employees"))
                refreshDataGridViewEmployees();
            if (tabControl.SelectedTab.Text.Equals("Needs your action"))
                refreshDataGridViewNeedsAction();
        }

        private void buttonConsider_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell cell in dataGridViewNeedsAction.SelectedCells)
            {
                dataGridViewNeedsAction.Rows[cell.RowIndex].Selected = true;
            }
            foreach (DataGridViewRow row in dataGridViewNeedsAction.SelectedRows)
            {
                FormLeaveConsideration form = new FormLeaveConsideration(connection,
                   (int)row.Cells["Employee id"].Value, (DateTime)row.Cells["First day"].Value,
                   (DateTime)row.Cells["Last day"].Value, this);
                form.FormClosed += new FormClosedEventHandler(refreshDataGridViewNeedsAction);
                form.Show();
            }
        }      
    }
}
