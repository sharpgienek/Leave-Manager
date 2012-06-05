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
    public partial class FormEmployee : Form
    {
        private DatabaseOperator databaseOperator;
        private Employee employee;

        public FormEmployee()
        { }

        public FormEmployee(DatabaseOperator databaseOperator, Employee employee)
        {
            InitializeComponent();
            this.databaseOperator = databaseOperator;
            this.employee = employee;
            labelDaysToUseValue.Text = (employee.LeaveDays + employee.OldLeaveDays).ToString();
            labelNameValue.Text = employee.Name + " " + employee.Surname;
            labelPositionValue.Text = employee.Position;
            refreshData();
        }

        private void refreshData(object o, FormClosedEventArgs e)
        {
            refreshData();
        }

        private void refreshData()
        {           
            DataTable data = new DataTable();
            if (databaseOperator.getLeaves(ref data, employee.EmployeeId))
            {
                dataGridView.DataSource = data;
                int leaveDays = 0;
                int oldLeaveDays = 0;
                databaseOperator.getDays(employee.EmployeeId, ref leaveDays, ref oldLeaveDays);
                employee.LeaveDays = leaveDays;
                employee.OldLeaveDays = oldLeaveDays;
                labelDaysToUseValue.Text = (leaveDays + oldLeaveDays).ToString();
            }
            else
            {
                //todo error message
            }
        }

        private void buttonChangeLoginOrPassword_Click(object sender, EventArgs e)
        {
            FormChangeLoginOrPassword form = new FormChangeLoginOrPassword(databaseOperator, employee.EmployeeId);
            form.Show();
        }

        private void buttonTakeLeave_Click(object sender, EventArgs e)
        {
            //FormEmployeeTakeLeave form = new FormEmployeeTakeLeave(connection, employee);
            FormLeaveApplication form = new FormLeaveApplication(this, databaseOperator, employee.EmployeeId);
            form.FormClosed += new FormClosedEventHandler(refreshData);
            form.Show();
        }

        private void buttonDeleteLeave_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell cell in dataGridView.SelectedCells)
            {
                dataGridView.Rows[cell.RowIndex].Selected = true;
            }
          //  SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                foreach (DataGridViewRow row in dataGridView.SelectedRows)
                {
                    if (!row.Cells["Status"].Value.ToString().Equals("Approved"))
                    {
                        if (!databaseOperator.DeleteLeave(employee.EmployeeId, (DateTime)row.Cells["First day"].Value, (DateTime)row.Cells["Last day"].Value))
                        {
                            MessageBox.Show("Error occured.");//todo ładny tekst
                        }
                        else
                        {
                            refreshData();
                        }
                    }
                    else
                    {
                      //  transaction.Rollback();
                        MessageBox.Show("You can not delete aproved leaves.\n");
                     //   return;
                    }                   
                }
           //     transaction.Commit();
                refreshData();
            }
            catch
            {
              //  transaction.Rollback();
                MessageBox.Show("There was an error. Try again later.");
            }
        }
    }
}
