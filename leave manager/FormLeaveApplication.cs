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
    public partial class FormLeaveApplication : Form
    {
        private SqlConnection connection;
       // private Employee employee;
        private List<LeaveType> leaveTypes;
        private int leaveDays;
        private int oldLeaveDays;
        private int employeeId;
        private bool editMode;
        private DateTime oldFirstDay;
     
        public FormLeaveApplication(SqlConnection connection, int leaveDays, int oldLeaveDays, int employeeId,
            String leaveType, DateTime firstDay, DateTime lastDay, String remarks, String leaveStatus)
        {
            InitializeComponent();
            this.connection = connection;
            comboBoxType.DataSource = leaveTypes = Dictionary.GetLeaveTypes(connection);
            comboBoxType.SelectedIndex = comboBoxType.FindStringExact(leaveType);
            if (firstDay.CompareTo(DateTime.Now) <= 0)
            {
                dateTimePickerFirstDay.MinDate = firstDay.Trim(TimeSpan.TicksPerDay);
            }
            else
            {
                dateTimePickerFirstDay.MinDate = DateTime.Now.Trim(TimeSpan.TicksPerDay);
            }
            dateTimePickerFirstDay.MaxDate = DateTime.Now.Trim(TimeSpan.TicksPerDay).AddYears(1);
            dateTimePickerFirstDay.Value = firstDay;
            dateTimePickerLastDay.MinDate = DateTime.Now.Trim(TimeSpan.TicksPerDay);
            dateTimePickerLastDay.MaxDate = DateTime.Now.Trim(TimeSpan.TicksPerDay).AddYears(1);
            dateTimePickerLastDay.Value = lastDay;            
            if (leaveTypes[comboBoxType.SelectedIndex].ConsumesDays)
            {
                labelUsedDaysValue.Text = TimeTools.GetNumberOfWorkDays(dateTimePickerFirstDay.Value,
                    dateTimePickerLastDay.Value).ToString();
            }
            else
            {
                labelUsedDaysValue.Text = "0";
            }
            labelAvailableDaysValue.Text = (leaveDays + oldLeaveDays).ToString();
            labelNormalValue.Text = leaveDays.ToString();
            labelOldValue.Text = oldLeaveDays.ToString();
            textBoxRemarks.Text = remarks;
            this.employeeId = employeeId;
            this.leaveDays = leaveDays;
            this.oldLeaveDays = oldLeaveDays;
            this.editMode = true;
            this.oldFirstDay = firstDay;

            List<String> statusList = Dictionary.GetStatusTypes(connection);
            if (!leaveStatus.Equals("Approved"))
            {
                statusList.Remove("Approved");
            }
            comboBoxStatus.DataSource = statusList;
            comboBoxStatus.SelectedIndex = comboBoxStatus.FindStringExact(leaveStatus);
        }

        public FormLeaveApplication(SqlConnection connection, int leaveDays, int oldLeaveDays, int employeeId)
        {
            InitializeComponent();
            this.connection = connection;
            comboBoxType.DataSource = leaveTypes = Dictionary.GetLeaveTypes(connection);
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
            labelAvailableDaysValue.Text = (leaveDays + oldLeaveDays).ToString();
            labelNormalValue.Text = leaveDays.ToString();
            labelOldValue.Text = oldLeaveDays.ToString();
            this.employeeId = employeeId;
            this.leaveDays = leaveDays;
            this.oldLeaveDays = oldLeaveDays;
            editMode = false;
            labelStatus.Visible = false;
            comboBoxStatus.Visible = false;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            int numberOfDays = TimeTools.GetNumberOfWorkDays(dateTimePickerFirstDay.Value, dateTimePickerLastDay.Value);
            if (numberOfDays <= oldLeaveDays + leaveDays)
            {
                if (numberOfDays != 0)
                {
                    if ((!editMode && !TimeTools.IsDateFromPeriodUsed(dateTimePickerFirstDay.Value, dateTimePickerLastDay.Value, connection, employeeId))
                        || (editMode && !TimeTools.IsDateFromPeriodUsed(dateTimePickerFirstDay.Value, dateTimePickerLastDay.Value, connection, employeeId, oldFirstDay)))
                    {           
                            SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.RepeatableRead);
                           // try
                        //    {
                                SqlCommand commandInsertLeave;
                                if (editMode)
                                {
                                    commandInsertLeave = new SqlCommand("UPDATE Leave SET LT_ID = (SELECT LT_ID FROM Leave_type WHERE Name = @Name), LS_ID = " +
                                        "(SELECT ST_ID FROM Status_type WHERE Name = @Type_name), " +
                                        "First_day = @First_day, Last_day = @Last_day, Remarks = @Remarks " +
                                        "WHERE Employee_ID = @Employee_ID AND First_day = @Old_first_day", connection, transaction);
                                    commandInsertLeave.Parameters.Add("@Old_first_day", SqlDbType.Date).Value = oldFirstDay;
                                    commandInsertLeave.Parameters.Add("@Type_name", SqlDbType.VarChar).Value = comboBoxStatus.SelectedItem.ToString();
                                }
                                else
                                {
                                    commandInsertLeave = new SqlCommand("INSERT INTO Leave VALUES (@Employee_ID, " +
                                        "(SELECT LT_ID FROM Leave_type WHERE Name = @Name), @LS_ID, " +
                                        "@First_day, @Last_day, @Remarks)", connection, transaction);
                                    commandInsertLeave.Parameters.Add("@LS_ID", SqlDbType.Int).Value = 0;
                                }
                                commandInsertLeave.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
                                commandInsertLeave.Parameters.Add("@Name", SqlDbType.VarChar).Value = comboBoxType.SelectedItem.ToString();                               
                                commandInsertLeave.Parameters.Add("@First_day", SqlDbType.Date).Value = dateTimePickerFirstDay.Value;
                                commandInsertLeave.Parameters.Add("@Last_day", SqlDbType.Date).Value = dateTimePickerLastDay.Value;
                                commandInsertLeave.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = textBoxRemarks.Text;
                                commandInsertLeave.ExecuteNonQuery();
                                /*
                                                            SqlCommand commandSelectDays = new SqlCommand("SELECT Leave_days, Old_leave_days FROM Employee WHERE " +
                                                                "Employee_ID = @Employee_ID", connection, transaction);
                                                            commandSelectDays.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employee.EmployeeId;
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
                                                            commandUpdateEmployee.ExecuteNonQuery();*/
                                transaction.Commit();//todo on final version check if transaction needed
                                this.Close();
                           /* }
                            catch
                            {
                                transaction.Rollback();
                                MessageBox.Show("Error occured. Please try again.");
                            }*/
                        
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
