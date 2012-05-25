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
        private SqlConnection connection;
        private int employeeId;
        private int leaveDays;
        private int oldLeaveDays;

        public FormEmployeeData(SqlConnection connection, int employeeId, String name, String surname, String position,
            int leaveDays, int oldLeaveDays)
        {
            InitializeComponent();
            this.connection = connection;
            this.employeeId = employeeId;
            this.leaveDays = leaveDays;
            this.oldLeaveDays = oldLeaveDays;
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
            SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.RepeatableRead);
            try
            {
                SqlCommand commandSelectDays = new SqlCommand("SELECT Leave_days, Old_leave_days FROM Employee " +
                    "WHERE Employee_ID = @Employee_ID", connection, transaction);
                commandSelectDays.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
                SqlDataReader readerDays = commandSelectDays.ExecuteReader();
                readerDays.Read();//todo zabezpiecz przed błędami.
                int leaveDays = (int)readerDays["Leave_days"];
                int oldLeaveDays = (int)readerDays["Old_leave_days"];
                readerDays.Close();
                SqlCommand commandSelectLeaves = new SqlCommand("SELECT LS.Name AS 'Status', L.First_day AS 'First day', " +
                    "L.Last_day AS 'Last day', LT.Name AS 'Type', LT.Consumes_days, L.Remarks " +
                    "FROM Employee E, Leave L, Leave_type LT, Status_type LS " +
                    "WHERE L.LT_ID = LT.LT_ID AND L.LS_ID = LS.ST_ID AND E.Employee_ID = L.Employee_ID ORDER BY L.First_day", connection, transaction);
                SqlDataReader readerLeaves = commandSelectLeaves.ExecuteReader();
                DataTable data = new DataTable();
                data.Load(readerLeaves);
                readerLeaves.Close();
                transaction.Commit();
                data.Columns.Add("No. used days");
                int unaprovedUsedDays = 0;
                int usedDays;
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    if ((bool)data.Rows[i]["Consumes_days"])
                    {
                        data.Rows[i]["No. used days"] = usedDays = TimeTools.GetNumberOfWorkDays((DateTime)data.Rows[i]["First day"],
                                (DateTime)data.Rows[i]["Last day"]);
                        if (!data.Rows[i]["Status"].ToString().Equals("Approved") && !data.Rows[i]["Status"].ToString().Equals("Rejected"))
                        {
                            unaprovedUsedDays += usedDays;
                        }
                    }
                    else
                        data.Rows[i]["No. used days"] = 0;
                }
                if (oldLeaveDays >= unaprovedUsedDays)
                {
                    oldLeaveDays -= unaprovedUsedDays;
                }
                else
                {
                    leaveDays -= (unaprovedUsedDays - oldLeaveDays);
                    oldLeaveDays = 0;
                }
                data.Columns.Remove("Consumes_days");
                dataGridView.DataSource = data;
                labelDaysToUseValue.Text = (leaveDays + oldLeaveDays).ToString();
            }
            catch (SqlException)
            {
                transaction.Rollback();//todo error message
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
                FormLeaveApplication form = new FormLeaveApplication(connection, leaveDays,
                    oldLeaveDays, employeeId, row.Cells["Type"].Value.ToString(), (DateTime)row.Cells["First day"].Value,
                    (DateTime)row.Cells["Last day"].Value, row.Cells["Remarks"].Value.ToString(), row.Cells["Status"].Value.ToString());
                form.FormClosed += new FormClosedEventHandler(refreshData);
                form.Show();
            }           
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            FormLeaveApplication form = new FormLeaveApplication(connection, leaveDays, oldLeaveDays, employeeId);
            form.FormClosed += new FormClosedEventHandler(refreshData);
            form.Show();
        }
        
    }
}
