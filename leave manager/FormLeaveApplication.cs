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
        // private SqlConnection connection;
        private DatabaseOperator databaseOperator;
        // private Employee employee;
        private List<LeaveType> leaveTypes;
      //  private int leaveDays;
       // private int oldLeaveDays;
        private int employeeId;
        private bool editMode;
        private DateTime oldFirstDay;

        public FormLeaveApplication(DatabaseOperator databaseOperator, int leaveDays, int oldLeaveDays, int employeeId,
            String leaveType, DateTime firstDay, DateTime lastDay, String remarks, String leaveStatus)
        {
            InitializeComponent();
            this.databaseOperator = databaseOperator;
            comboBoxType.DataSource = leaveTypes = databaseOperator.GetLeaveTypes();
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
            this.editMode = true;
            this.oldFirstDay = firstDay;

            List<String> statusList = databaseOperator.GetStatusTypes();
            if (!leaveStatus.Equals("Approved"))
            {
                statusList.Remove("Approved");
            }
            comboBoxStatus.DataSource = statusList;
            comboBoxStatus.SelectedIndex = comboBoxStatus.FindStringExact(leaveStatus);
        }

        public FormLeaveApplication(object parent, DatabaseOperator databaseOperator, int employeeId)
        {
            InitializeComponent();
           /* SqlCommand commandReadDays = new SqlCommand("SELECT Year_leave_days, Leave_days, Old_leave_days " +
                               "FROM Employee WHERE Employee_ID = @Employee_ID", connection);
            commandReadDays.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
            SqlDataReader readerDays = commandReadDays.ExecuteReader();
            readerDays.Read();*/
           
          //  readerDays.Close();
            List<String> statusList = databaseOperator.GetStatusTypes();
            leaveTypes = databaseOperator.GetLeaveTypes();
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
            this.databaseOperator = databaseOperator;

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
            int leaveDays = 0;
            int   oldLeaveDays = 0;
            if (databaseOperator.getDays(employeeId, ref leaveDays, ref oldLeaveDays))
            {
                labelAvailableDaysValue.Text = (leaveDays + oldLeaveDays).ToString();
                labelNormalValue.Text = leaveDays.ToString();
                labelOldValue.Text = oldLeaveDays.ToString();
                this.employeeId = employeeId;


                editMode = false;
            }
            else
            {
                //błąd.. może rzucić exception?
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)//todo aktualizacja posiadanych dni.
        {
            int numberOfDays = TimeTools.GetNumberOfWorkDays(dateTimePickerFirstDay.Value, dateTimePickerLastDay.Value);
            int leaveDays = 0;
            int oldLeaveDays = 0;
            int yearDays = 0;
            if (databaseOperator.getDays(employeeId, ref leaveDays, ref oldLeaveDays, ref yearDays))
            {
                if (numberOfDays <= oldLeaveDays + leaveDays)
                {
                    if (numberOfDays != 0)
                    {
                        if ((!editMode && !databaseOperator.IsDateFromPeriodUsed(dateTimePickerFirstDay.Value, dateTimePickerLastDay.Value, employeeId))
                            || (editMode && !databaseOperator.IsDateFromPeriodUsed(dateTimePickerFirstDay.Value, dateTimePickerLastDay.Value, employeeId, oldFirstDay))
                            || comboBoxType.SelectedItem.ToString().Equals("Sick"))
                        {
                            Leave leave = new Leave(employeeId, comboBoxType.SelectedItem.ToString(), 
                                comboBoxStatus.SelectedItem.ToString(), dateTimePickerFirstDay.Value, dateTimePickerLastDay.Value, textBoxRemarks.Text);
                            if (comboBoxType.SelectedItem.ToString().Equals("Sick"))
                            {
                              //  databaseOperator.AddSickLeave(employeeId, dateTimePickerFirstDay.Value, dateTimePickerLastDay.Value, ref leaveDays, ref oldLeaveDays, yearDays);
                              //  leave.LeaveStatus = "Approved";
                                if (databaseOperator.addSickLeave(leave,
                                    ref leaveDays, ref oldLeaveDays, yearDays))
                                {
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Error occured");//todo ładny error
                                }                                    
                                //int employeeId, String leaveType, String leaveStatus, DateTime firstDay, DateTime lastDay, String remarks)
                            }
                            else
                            {
                                if (databaseOperator.addLeave(leave))
                                {
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Error occured."); //todo ładny error
                                }
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
            else
            {
                //todo error message
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
