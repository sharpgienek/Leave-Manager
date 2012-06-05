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
        private DatabaseOperator databaseOperator;

        

        public FormManager(DatabaseOperator databaseOperator)
        {
            InitializeComponent();
            this.databaseOperator = databaseOperator;
            refreshDataGridViewNeedsAction();
        }

        private void refreshDataGridViewNeedsAction(object o, FormClosedEventArgs e)
        {
            refreshDataGridViewNeedsAction();
        }

        private void refreshDataGridViewNeedsAction()
        {           
            DataTable data = new DataTable();
            if (databaseOperator.getNeedsAction(this, ref data))
            {
                dataGridViewNeedsAction.DataSource = data;
            }
            else
            {
                //todo error message
            }
        }

        private void refreshDataGridViewEmployees(Object o, EventArgs e)
        {
            refreshDataGridViewEmployees();
        }

        private void refreshDataGridViewEmployees()
        {
           /* SqlCommand command = new SqlCommand("SELECT E.Name, E.Surname, E.Birth_date AS 'Birth date'," +
                "E.Address, E.PESEL, E.EMail AS 'e-mail', Pos.Description AS Position, " +
                "Perm.Description AS Permission, E.Leave_days AS 'Remaining leave days', "+
                "E.Old_leave_days AS 'Old left leave days' " +
                "FROM Employee E, Position Pos, Permission Perm " +
                "WHERE E.Permission_ID = Perm.Permission_ID " +
                "AND E.Position_ID = Pos.Position_ID", connection);

            SqlDataReader reader = command.ExecuteReader();
            DataTable employees = new DataTable();
            employees.Load(reader);
            reader.Close();*/
            DataTable employees = new DataTable();
            if (databaseOperator.getEmployees(ref employees))
            {
                dataGridViewEmployees.DataSource = employees;
            }
            else
            {
                //todo error message
            }
        }

        private void buttonEmployeesAdd_Click(object sender, EventArgs e)
        {
            FormAddEmployee form = new FormAddEmployee(databaseOperator);
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
                FormLeaveConsideration form = new FormLeaveConsideration(databaseOperator,
                   (int)row.Cells["Employee id"].Value, (DateTime)row.Cells["First day"].Value,
                   (DateTime)row.Cells["Last day"].Value, this);
                form.FormClosed += new FormClosedEventHandler(refreshDataGridViewNeedsAction);
                form.Show();
            }
        }      
    }
}
