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
    public partial class FormEmployeeData : Form
    {
        private DatabaseOperator databaseOperator;
        private int employeeId;
      //  private int leaveDays;
       // private int oldLeaveDays;
        private object parent;

        public FormEmployeeData(object parent, DatabaseOperator databaseOperator, int employeeId, String name, String surname, String position,
            int leaveDays, int oldLeaveDays)
        {
            InitializeComponent();
            this.databaseOperator = databaseOperator;
            this.employeeId = employeeId;
        //    this.leaveDays = leaveDays;
          //  this.oldLeaveDays = oldLeaveDays;
            this.parent = parent;
            labelNameValue.Text = name + " " + surname;
            labelPositionValue.Text = position;
            refreshData();
        } 

        private void refreshData(object o, FormClosedEventArgs e)
        {
            refreshData();
        }

        private void refreshData()
        {
            //SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.RepeatableRead);
            
                int leaveDays = 0;// (int)readerDays["Leave_days"];
                int oldLeaveDays = 0;// (int)readerDays["Old_leave_days"];
                if (databaseOperator.getDays(employeeId,ref leaveDays,ref oldLeaveDays))
                {                       
                    DataTable data = new DataTable();
                    if (databaseOperator.getLeaves(ref data, employeeId))
                    {
                        dataGridView.DataSource = data;
                        labelDaysToUseValue.Text = (leaveDays + oldLeaveDays).ToString();
                    }
                    else
                    {
                        //todo error message
                    }
                }
                else
                {
                    //todo error message
                }
          
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell cell in dataGridView.SelectedCells)
            {
                dataGridView.Rows[cell.RowIndex].Selected = true;
            }
            foreach (DataGridViewRow row in dataGridView.SelectedRows)
            {
                FormLeaveApplication form = new FormLeaveApplication(this, databaseOperator, employeeId);/*new FormLeaveApplication(databaseOperator,
                    employeeId, row.Cells["Type"].Value.ToString(), (DateTime)row.Cells["First day"].Value,
                    (DateTime)row.Cells["Last day"].Value, row.Cells["Remarks"].Value.ToString(), row.Cells["Status"].Value.ToString());*/
                form.FormClosed += new FormClosedEventHandler(refreshData);
                form.Show();
            }           
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            FormLeaveApplication form = new FormLeaveApplication(parent, databaseOperator, employeeId);
            form.FormClosed += new FormClosedEventHandler(refreshData);
            form.Show();
        }
        
    }
}
