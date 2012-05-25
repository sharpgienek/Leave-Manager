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
        private SqlConnection connection;
        private Employee employee;
        public FormEmployee(SqlConnection connection, Employee employee)
        {
            InitializeComponent();
            this.connection = connection;
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
            SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.RepeatableRead);
            try
            {
                SqlCommand commandSelectDays = new SqlCommand("SELECT Leave_days, Old_leave_days FROM Employee " +
                    "WHERE Employee_ID = @Employee_ID", connection, transaction);
                commandSelectDays.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employee.EmployeeId;
                SqlDataReader readerDays = commandSelectDays.ExecuteReader();
                readerDays.Read();//todo zabezpiecz przed błędami.
                employee.LeaveDays = (int)readerDays["Leave_days"];
                employee.OldLeaveDays = (int)readerDays["Old_leave_days"];
                readerDays.Close();
                SqlCommand commandSelectLeaves = new SqlCommand("SELECT LS.Name AS 'Status', L.First_day AS 'First day', " +
                    "L.Last_day AS 'Last day', LT.Name AS 'Type', LT.Consumes_days " +
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
                if (employee.OldLeaveDays >= unaprovedUsedDays)
                {
                    employee.OldLeaveDays -= unaprovedUsedDays;
                }
                else
                {                    
                    employee.LeaveDays -= (unaprovedUsedDays - employee.OldLeaveDays);
                    employee.OldLeaveDays = 0;
                }
                data.Columns.Remove("Consumes_days");
                dataGridView.DataSource = data;
                labelDaysToUseValue.Text = (employee.LeaveDays + employee.OldLeaveDays).ToString();
            }
            catch (SqlException)
            {
                transaction.Rollback();//todo error message
            }

        }

        private void buttonChangeLoginOrPassword_Click(object sender, EventArgs e)
        {
            FormChangeLoginOrPassword form = new FormChangeLoginOrPassword(connection, employee.EmployeeId);
            form.Show();
        }

      /*  private void refreshEmployeeDays(object o, FormClosedEventArgs e)
        {
            SqlCommand commandUpdateLeave = new SqlCommand("Select Leave_days, Old_leave_days " +
                "FROM Employee WHERE Employee_ID = @Employee_ID", connection);
            commandUpdateLeave.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employee.EmployeeId;
            SqlDataReader reader = commandUpdateLeave.ExecuteReader();//todo try catch
            reader.Read();
            employee.LeaveDays = (int)reader["Leave_days"];
            employee.OldLeaveDays = (int)reader["Old_leave_days"];
            reader.Close();
            labelDaysToUseValue.Text = (employee.LeaveDays + employee.OldLeaveDays).ToString();

        }*/

        private void buttonTakeLeave_Click(object sender, EventArgs e)
        {
            //FormEmployeeTakeLeave form = new FormEmployeeTakeLeave(connection, employee);
            FormLeaveApplication form = new FormLeaveApplication(connection, employee.LeaveDays, employee.OldLeaveDays, employee.EmployeeId);
            form.FormClosed += new FormClosedEventHandler(refreshData);
            form.Show();
        }

        private void buttonDeleteLeave_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell cell in dataGridView.SelectedCells)
            {
                dataGridView.Rows[cell.RowIndex].Selected = true;
            }
            SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                foreach (DataGridViewRow row in dataGridView.SelectedRows)
                {
                    if (!row.Cells["Status"].Value.ToString().Equals("Approved"))
                    {
                        SqlCommand command = new SqlCommand("DELETE FROM Leave WHERE " +
                            "Employee_ID = @Employee_ID AND First_day = @First_day", connection, transaction);
                        command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employee.EmployeeId;
                        command.Parameters.Add("@First_day", SqlDbType.Date).Value = row.Cells["First day"].Value;
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        transaction.Rollback();
                        MessageBox.Show("You can not delete aproved leaves.\n");
                        return;
                    }                   
                }
                transaction.Commit();
                refreshData();
            }
            catch
            {
                transaction.Rollback();
                MessageBox.Show("There was an error. Try again later.");
            }
        }
    }
}
