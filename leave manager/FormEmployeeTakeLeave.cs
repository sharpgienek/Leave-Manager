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
    public partial class FormEmployeeTakeLeave : Form
    {
        private SqlConnection connection;
        private Employee employee;
        private List<LeaveType> leaveTypes;
        public FormEmployeeTakeLeave(SqlConnection connection, Employee employee)
        {
            InitializeComponent();
            this.connection = connection;
            comboBoxType.DataSource = leaveTypes = Dictionary.getLeaveTypes(connection);
            dateTimePickerFirstDay.MinDate = DateTime.Now.Trim(TimeSpan.TicksPerDay).AddDays(1);
            dateTimePickerFirstDay.MaxDate = DateTime.Now.Trim(TimeSpan.TicksPerDay).AddYears(1);
            dateTimePickerLastDay.MinDate = DateTime.Now.Trim(TimeSpan.TicksPerDay).AddDays(1);
            dateTimePickerLastDay.MaxDate = DateTime.Now.Trim(TimeSpan.TicksPerDay).AddYears(1);
            if (leaveTypes[comboBoxType.SelectedIndex].ConsumesDays)
            {
                labelUsedDaysValue.Text = TimeTools.GetNumberOfWorkDays(dateTimePickerFirstDay.Value,
                    dateTimePickerLastDay.Value).ToString();
            }
            else
            {
                labelUsedDaysValue.Text = "0";
            }
            labelAvailableDaysValue.Text = (employee.LeaveDays + employee.OldLeaveDays).ToString();
            labelNormalValue.Text = employee.LeaveDays.ToString();
            labelOldValue.Text = employee.OldLeaveDays.ToString();
            this.employee = employee;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            int numberOfDays = TimeTools.GetNumberOfWorkDays(dateTimePickerFirstDay.Value, dateTimePickerLastDay.Value);
            if (numberOfDays <= employee.OldLeaveDays + employee.LeaveDays)
            {
                if (numberOfDays != 0)
                {
                    if (!TimeTools.IsDateFromPeriodUsed(dateTimePickerFirstDay.Value, dateTimePickerLastDay.Value, connection, employee.EmployeeID))
                    {
                        SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.RepeatableRead);
                        try
                        {
                            SqlCommand commandInsertLeave = new SqlCommand("INSERT INTO Leave VALUES (@Employee_ID, @LT_ID, @LS_ID, " +
                                "@First_day, @Last_day, @Remarks)", connection, transaction);
                            commandInsertLeave.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employee.EmployeeID;
                            commandInsertLeave.Parameters.Add("@LT_ID", SqlDbType.Int).Value = comboBoxType.SelectedIndex;
                            commandInsertLeave.Parameters.Add("@LS_ID", SqlDbType.Int).Value = 0;
                            commandInsertLeave.Parameters.Add("@First_day", SqlDbType.Date).Value = dateTimePickerFirstDay.Value;
                            commandInsertLeave.Parameters.Add("@Last_day", SqlDbType.Date).Value = dateTimePickerLastDay.Value;
                            commandInsertLeave.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = textBoxRemarks.Text;
                            commandInsertLeave.ExecuteNonQuery();

                            SqlCommand commandSelectDays = new SqlCommand("SELECT Leave_days, Old_leave_days FROM Employee WHERE " +
                                "Employee_ID = @Employee_ID", connection, transaction);
                            commandSelectDays.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employee.EmployeeID;
                            SqlDataReader readerDays = commandSelectDays.ExecuteReader();
                            readerDays.Read();
                            SqlCommand commandUpdateEmployee = new SqlCommand("UPDATE Employee SET Old_leave_days = @Old_leave_days, " +
                                "Leave_days = @Leave_days", connection, transaction);
                            if ((int)readerDays["Old_leave_days"] - numberOfDays >= 0)
                            {
                                commandUpdateEmployee.Parameters.Add("@Old_leave_days", SqlDbType.Int).Value =
                                    (int)readerDays["Old_leave_days"] - numberOfDays;
                                commandUpdateEmployee.Parameters.Add("@Leave_days", SqlDbType.Int).Value = (int)readerDays["Leave_days"];
                            }
                            else
                            {
                                commandUpdateEmployee.Parameters.Add("@Old_leave_days", SqlDbType.Int).Value = 0;
                                commandUpdateEmployee.Parameters.Add("@Leave_days", SqlDbType.Int).Value =
                                    (int)readerDays["Leave_days"] - (numberOfDays - (int)readerDays["Old_leave_days"]);
                            }
                            readerDays.Close();
                            commandUpdateEmployee.ExecuteNonQuery();
                            transaction.Commit();
                            this.Close();
                        }
                        catch
                        {
                            transaction.Rollback();
                            MessageBox.Show("Error occured. Please try again.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("At least one day from selected period is already used for leave.");
                    }
                }
                else
                {
                    MessageBox.Show("No work days selected.");
                }
            }
            else
            {
                MessageBox.Show("You don't have enough leave days left.");
            }
        }

        private void dateTimePickerFirstDay_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerLastDay.MinDate = dateTimePickerFirstDay.Value;
            updateLabelUsedDaysValue();
        }

        private void updateLabelUsedDaysValue()
        {
            if (leaveTypes[comboBoxType.SelectedIndex].ConsumesDays)
            {
                labelUsedDaysValue.Text = TimeTools.GetNumberOfWorkDays(dateTimePickerFirstDay.Value,
                    dateTimePickerLastDay.Value).ToString();
            }
            else
            {
                labelUsedDaysValue.Text = "0";
            }
        }

        private void dateTimePickerLastDay_ValueChanged(object sender, EventArgs e)
        {
            updateLabelUsedDaysValue();
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateLabelUsedDaysValue();
        }
    }
}
