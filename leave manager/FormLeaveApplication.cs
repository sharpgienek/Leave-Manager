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

        public FormLeaveApplication(object parent, SqlConnection connection, int employeeId)
        {
            InitializeComponent();
            SqlCommand commandReadDays = new SqlCommand("SELECT Year_leave_days, Leave_days, Old_leave_days " +
                               "FROM Employee WHERE Employee_ID = @Employee_ID", connection);
            commandReadDays.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
            SqlDataReader readerDays = commandReadDays.ExecuteReader();
            readerDays.Read();
            leaveDays = (int)readerDays["Leave_days"];
            oldLeaveDays = (int)readerDays["Old_leave_days"];
            readerDays.Close();
            List<String> statusList = Dictionary.GetStatusTypes(connection);
            leaveTypes = Dictionary.GetLeaveTypes(connection);
            if (parent.GetType() != new FormEmployee().GetType())
            {
                statusList.Remove("Rejected");
                if (parent.GetType() == new FormAssistant().GetType())
                {
                    statusList.Remove("Approved");
                }
            }
            else
            {
                leaveTypes.Remove(new LeaveType("Sick"));
                labelStatus.Visible = false;
                comboBoxStatus.Visible = false;
            }
            comboBoxStatus.DataSource = statusList;
            comboBoxType.DataSource = leaveTypes;
            this.connection = connection;

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
           
            editMode = false;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)//todo aktualizacja posiadanych dni.
        {
            int numberOfDays = TimeTools.GetNumberOfWorkDays(dateTimePickerFirstDay.Value, dateTimePickerLastDay.Value);
            if (numberOfDays <= oldLeaveDays + leaveDays)
            {
                if (numberOfDays != 0)
                {
                    if ((!editMode && !TimeTools.IsDateFromPeriodUsed(dateTimePickerFirstDay.Value, dateTimePickerLastDay.Value, connection, employeeId))
                        || (editMode && !TimeTools.IsDateFromPeriodUsed(dateTimePickerFirstDay.Value, dateTimePickerLastDay.Value, connection, employeeId, oldFirstDay))
                        || comboBoxType.SelectedItem.ToString().Equals("Sick"))
                    {
                        SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.RepeatableRead);
                        // try
                        //    {

                        if (comboBoxType.SelectedItem.ToString().Equals("Sick"))
                        {
                            SqlCommand commandReadLeaves = new SqlCommand("SELECT First_day, Last_day FROM Leave WHERE " +
                                "Employee_ID = @Employee_ID", connection, transaction);
                            commandReadLeaves.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
                            SqlDataReader readerLeaves = commandReadLeaves.ExecuteReader();
                            DataTable dataLeaves = new DataTable();
                            dataLeaves.Load(readerLeaves);
                            readerLeaves.Close();
                            int returnedLeaveDays = 0;
                            foreach (DataRow row in dataLeaves.Rows)
                            {
                                //pierwszy dzień sprawdzanego urlopu jest później lub ten sam, co pierwszy dzień chorobowego
                                if ((((DateTime)row.ItemArray.GetValue(0)).CompareTo(dateTimePickerFirstDay.Value) >= 0)
                                    //i jest wcześniej lub taki sam jak ostatni dzień chorobowego.
                                && (((DateTime)row.ItemArray.GetValue(0)).CompareTo(dateTimePickerLastDay.Value) <= 0))
                                {
                                    SqlCommand commandUpdateLeave = new SqlCommand("UPDATE Leave SET " +
                                       "LS_ID = (SELECT ST_ID FROM Status_type WHERE Name = @Status_name) " +
                                       "WHERE Employee_ID = @Employee_ID AND First_day = @First_day", connection, transaction);
                                    commandUpdateLeave.Parameters.Add("@Status_name", SqlDbType.VarChar).Value = "Canceled";
                                    commandUpdateLeave.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
                                    commandUpdateLeave.Parameters.Add("@First_day", SqlDbType.Date).Value = row.ItemArray.GetValue(0);
                                    commandUpdateLeave.ExecuteNonQuery();
                                    returnedLeaveDays += ((DateTime)row.ItemArray.GetValue(0)).GetNumberOfWorkDays((DateTime)row.ItemArray.GetValue(1));                                  
                                    continue;
                                }

                                if ((dateTimePickerFirstDay.Value.CompareTo((DateTime)row.ItemArray.GetValue(0)) >= 0)//Sick first day later than leave first day 
                                && (dateTimePickerFirstDay.Value.CompareTo((DateTime)row.ItemArray.GetValue(1)) <= 0))//and earlier than leave last day.
                                {
                                    SqlCommand commandUpdateLeave = new SqlCommand("UPDATE Leave SET " +
                                        "Last_day = @Last_day WHERE Employee_ID = @Employee_ID AND First_day = @First_day", connection, transaction);
                                    commandUpdateLeave.Parameters.Add("@Last_day", SqlDbType.Date).Value = dateTimePickerFirstDay.Value.AddDays(-1);
                                    commandUpdateLeave.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
                                    commandUpdateLeave.Parameters.Add("@First_day", SqlDbType.Date).Value = row.ItemArray.GetValue(0);
                                    commandUpdateLeave.ExecuteNonQuery();
                                    returnedLeaveDays += ((DateTime)row.ItemArray.GetValue(0)).GetNumberOfWorkDays(dateTimePickerFirstDay.Value.AddDays(-1));
                                    continue;
                                }
                            }
                            SqlCommand commandReadDays = new SqlCommand("SELECT Year_leave_days, Leave_days, Old_leave_days " +
                                "FROM Employee WHERE Employee_ID = @Employee_ID", connection, transaction);
                            commandReadDays.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
                            SqlDataReader readerDays = commandReadDays.ExecuteReader();
                            readerDays.Read();//todo try catch?
                            SqlCommand commandUpdateEmployee = new SqlCommand("UPDATE Employee SET " +
                                "Leave_days = @Leave_days, Old_leave_days = @Old_leave_days " +
                                "WHERE Employee_ID = @Employee_ID", connection, transaction);
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
                            commandUpdateEmployee.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
                            commandUpdateEmployee.ExecuteNonQuery();
                        }



                        SqlCommand commandInsertLeave;
                        if (editMode)
                        {
                            commandInsertLeave = new SqlCommand("UPDATE Leave SET LT_ID = (SELECT LT_ID FROM Leave_type WHERE Name = @Name), LS_ID = " +
                                "(SELECT ST_ID FROM Status_type WHERE Name = @Type_name), " +
                                "First_day = @First_day, Last_day = @Last_day, Remarks = @Remarks " +
                                "WHERE Employee_ID = @Employee_ID AND First_day = @Old_first_day", connection, transaction);
                            commandInsertLeave.Parameters.Add("@Old_first_day", SqlDbType.Date).Value = oldFirstDay;
                            //   commandInsertLeave.Parameters.Add("@Type_name", SqlDbType.VarChar).Value = comboBoxStatus.SelectedItem.ToString();
                        }
                        else
                        {                           
                            commandInsertLeave = new SqlCommand("INSERT INTO Leave VALUES (@Employee_ID, " +
                                "(SELECT LT_ID FROM Leave_type WHERE Name = @Name), (SELECT ST_ID FROM Status_type WHERE Name = @Status_name), " +
                                "@First_day, @Last_day, @Remarks)", connection, transaction);
                        }
                        if (comboBoxType.SelectedItem.ToString().Equals("Sick"))
                        {
                            commandInsertLeave.Parameters.Add("@Status_name", SqlDbType.VarChar).Value = "Approved";
                        }
                        else
                        {
                            commandInsertLeave.Parameters.Add("@Status_name", SqlDbType.VarChar).Value = comboBoxStatus.SelectedItem.ToString();
                        }
                        commandInsertLeave.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
                        commandInsertLeave.Parameters.Add("@Name", SqlDbType.VarChar).Value = comboBoxType.SelectedItem.ToString();
                        commandInsertLeave.Parameters.Add("@First_day", SqlDbType.Date).Value = dateTimePickerFirstDay.Value;
                        commandInsertLeave.Parameters.Add("@Last_day", SqlDbType.Date).Value = dateTimePickerLastDay.Value;
                        commandInsertLeave.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = textBoxRemarks.Text;
                        commandInsertLeave.ExecuteNonQuery();

                        SqlCommand commandSelectConsumesDays = new SqlCommand("SELECT Consumes_days FROM  " +
                        "Leave_type WHERE Name = @Name", connection, transaction);
                        commandSelectConsumesDays.Parameters.Add("@Name", SqlDbType.VarChar).Value = comboBoxType.SelectedItem.ToString();//todo czy potrzebne tostring
                        SqlDataReader readerConsumesDays = commandSelectConsumesDays.ExecuteReader();
                        readerConsumesDays.Read();//todo try catch
                        if ((bool)readerConsumesDays["Consumes_days"])
                        {
                            readerConsumesDays.Close();

                            SqlCommand commandSelectDays = new SqlCommand("SELECT Leave_days, Old_leave_days FROM Employee WHERE " +
                                                "Employee_ID = @Employee_ID", connection, transaction);

                            commandSelectDays.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
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
                        }
                        readerConsumesDays.Close();
/*
                        SqlCommand commandSelectConsumesDays = new SqlCommand("SELECT LT.Consumes_days FROM Leave L, " +
                        "Leave_type LT WHERE L.LT_ID = LT.LT_ID AND L.Employee_ID = @Employee_ID " +
                        "AND First_day = @First_day", connection, transaction);
                        commandSelectConsumesDays.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
                        commandSelectConsumesDays.Parameters.Add("@First_day", SqlDbType.Date).Value = dateTimePickerFirstDay.Value;
                        SqlDataReader readerConsumesDays = commandSelectConsumesDays.ExecuteReader();
                        readerConsumesDays.Read();//todo try catch
                        if ((bool)readerConsumesDays["Consumes_days"])
                        {
                            readerConsumesDays.Close();

                            SqlCommand commandSelectDays = new SqlCommand("SELECT Leave_days, Old_leave_days FROM Employee WHERE " +
                                                "Employee_ID = @Employee_ID", connection, transaction);

                            commandSelectDays.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
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
                        }
                        readerConsumesDays.Close();
                        
                        */
                        
                        
                        
                        
                        
                        
                        
                        
                        
                        
                        
                        
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
            if (comboBoxType.SelectedItem.ToString().Equals("Sick"))
            {
                //  comboBoxStatus.Items.Add("Approved");
                //  comboBoxStatus.SelectedIndex = comboBoxStatus.FindStringExact("Approved");
                comboBoxStatus.Enabled = false;
            }
            else
            {
                comboBoxStatus.Enabled = true;
                //  comboBoxStatus.Items.Remove("Approved");
                //  comboBoxStatus.SelectedIndex = 0;
            }
        }
    }
}
