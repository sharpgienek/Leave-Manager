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

        public FormEmployee()
        { }

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
               // int sumOfUsedDays = 0;
                int usedDays;
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    if ((bool)data.Rows[i]["Consumes_days"])
                    {
                        data.Rows[i]["No. used days"] = usedDays = TimeTools.GetNumberOfWorkDays((DateTime)data.Rows[i]["First day"],
                                (DateTime)data.Rows[i]["Last day"]);
                       /* if (!data.Rows[i]["Status"].ToString().Equals("Approved") && !data.Rows[i]["Status"].ToString().Equals("Rejected"))
                        {
                            sumOfUsedDays += usedDays;
                        }*/
                    }
                    else
                        data.Rows[i]["No. used days"] = 0;
                }
                /*if (employee.OldLeaveDays >= sumOfUsedDays)
                {
                    employee.OldLeaveDays -= sumOfUsedDays;
                }
                else
                {                    
                    employee.LeaveDays -= (sumOfUsedDays - employee.OldLeaveDays);
                    employee.OldLeaveDays = 0;
                }*/
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
            SqlDataReader readerLeaves = commandUpdateLeave.ExecuteReader();//todo try catch
            readerLeaves.Read();
            employee.LeaveDays = (int)readerLeaves["Leave_days"];
            employee.OldLeaveDays = (int)readerLeaves["Old_leave_days"];
            readerLeaves.Close();
            labelDaysToUseValue.Text = (employee.LeaveDays + employee.OldLeaveDays).ToString();

        }*/

        private void buttonTakeLeave_Click(object sender, EventArgs e)
        {
            //FormEmployeeTakeLeave form = new FormEmployeeTakeLeave(connection, employee);
            FormLeaveApplication form = new FormLeaveApplication(this, connection, employee.EmployeeId);
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
                        SqlCommand commandDeleteLeave = new SqlCommand("DELETE FROM Leave WHERE " +
                            "Employee_ID = @Employee_ID AND First_day = @First_day", connection, transaction);
                        commandDeleteLeave.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employee.EmployeeId;
                        commandDeleteLeave.Parameters.Add("@First_day", SqlDbType.Date).Value = row.Cells["First day"].Value;
                        commandDeleteLeave.ExecuteNonQuery();
                       
                        
                        SqlCommand commandReadDays = new SqlCommand("SELECT Year_leave_days, Leave_days, Old_leave_days " +
                                "FROM Employee WHERE Employee_ID = @Employee_ID", connection, transaction);
                        commandReadDays.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employee.EmployeeId;
                        SqlDataReader readerDays = commandReadDays.ExecuteReader();
                        readerDays.Read();//todo try catch?
                        SqlCommand commandUpdateEmployee = new SqlCommand("UPDATE Employee SET " +
                            "Leave_days = @Leave_days, Old_leave_days = @Old_leave_days " +
                            "WHERE Employee_ID = @Employee_ID", connection, transaction);
                        int returnedLeaveDays = ((DateTime)row.Cells["First day"].Value).GetNumberOfWorkDays((DateTime)row.Cells["Last day"].Value);
                        if ((int)readerDays["Leave_days"] + returnedLeaveDays > (int)readerDays["Year_leave_days"])
                        {
                            commandUpdateEmployee.Parameters.Add("@Leave_days", SqlDbType.Int).Value = (int)readerDays["Year_leave_days"];
                            commandUpdateEmployee.Parameters.Add("@Old_leave_days", SqlDbType.Int).Value =
                                (int)readerDays["Old_leave_days"] + returnedLeaveDays - ((int)readerDays["Year_leave_days"] - (int)readerDays["Leave_days"]);
                        }
                        else
                        {
                            commandUpdateEmployee.Parameters.Add("@Leave_days", SqlDbType.Int).Value = (int)readerDays["Leave_days"] + returnedLeaveDays;
                            commandUpdateEmployee.Parameters.Add("@Old_leave_days", SqlDbType.Int).Value = 0;
                        }
                        readerDays.Close();
                        commandUpdateEmployee.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employee.EmployeeId;
                        commandUpdateEmployee.ExecuteNonQuery();
                       // SqlCommand commandUpdateDays = new SqlCommand("UPDATE Employee SET @

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
