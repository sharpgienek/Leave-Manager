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
    public partial class FormLeaveConsideration : Form
    {
        private SqlConnection connection;
        private int employeeId;
        private DateTime firstDay;
        private DateTime lastDay;
      //  private String aprovalStatus;
        private object parent;
      //  public FormLeaveConsideration(SqlConnection connection, int employeeId, DateTime firstDay, DateTime lastDay, String aprovalStatus)
        public FormLeaveConsideration(SqlConnection connection, int employeeId, DateTime firstDay, DateTime lastDay, object parent)
        {
            InitializeComponent();
            this.connection = connection;
            this.employeeId = employeeId;
            this.firstDay = firstDay;
            this.lastDay = lastDay;
          //  this.aprovalStatus = aprovalStatus;
            this.parent = parent;
            SqlCommand command = new SqlCommand("SELECT E.Name, E.Surname, P.Description AS 'Position' " +
                "FROM Employee E, Position P WHERE E.Employee_ID = @Employee_ID " +
                "AND E.Position_ID = P.Position_ID", connection);
            command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
            SqlDataReader reader = command.ExecuteReader();//todo try catch
            reader.Read();
            labelNameValue.Text = reader["Name"].ToString() + " " + reader["Surname"].ToString();
            labelPositionValue.Text = reader["Position"].ToString();
            reader.Close();
            labelFirstDayValue.Text = firstDay.ToString("d");
            labelLastDayValue.Text = lastDay.ToString("d");
        }

        private void buttonLeaveUnchanged_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.RepeatableRead);
          //  try
          //  {
                SqlCommand commandUpdateLeave = new SqlCommand("UPDATE Leave SET LS_ID = (SELECT ST_ID FROM " +
                    "Status_type WHERE Name = @Name) WHERE Employee_ID = @Employee_ID " +
                    "AND First_day = @First_day ", connection, transaction);
                commandUpdateLeave.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
                commandUpdateLeave.Parameters.Add("@First_day", SqlDbType.Date).Value = firstDay;
                if (parent.GetType() == new FormAssistant().GetType())
                {
                    commandUpdateLeave.Parameters.Add("@Name", SqlDbType.VarChar).Value = "Pending manager aproval";
                }
                else
                {
                    commandUpdateLeave.Parameters.Add("@Name", SqlDbType.VarChar).Value = "Approved";
                }
                
                commandUpdateLeave.ExecuteNonQuery();//todo try catch;
                int numberOfDays = TimeTools.GetNumberOfWorkDays(firstDay, lastDay);
           /*     if (aprovalStatus.Equals("Approved"))
                {
                    SqlCommand commandSelectConsumesDays = new SqlCommand("SELECT LT.Consumes_days FROM Leave L, " +
                        "Leave_type LT WHERE L.LT_ID = LT.LT_ID AND L.Employee_ID = @Employee_ID " +
                        "AND First_day = @First_day", connection, transaction);
                    commandSelectConsumesDays.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
                    commandSelectConsumesDays.Parameters.Add("@First_day", SqlDbType.Date).Value = firstDay;
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
                }*/
                transaction.Commit();
           // }
          //  catch
         //   {
         //       transaction.Rollback();
                //todo error message;
          //  }
            this.Close();
        }

        private void buttonReject_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("UPDATE Leave SET LS_ID = (SELECT ST_ID FROM " +
                "Status_type WHERE Name = @Name) WHERE Employee_ID = @Employee_ID " +
                "AND First_day = @First_day ", connection);
            command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
            command.Parameters.Add("@First_day", SqlDbType.Date).Value = firstDay;
            command.Parameters.Add("@Name", SqlDbType.VarChar).Value = "Rejected";
            command.ExecuteNonQuery();//todo try catch;
            this.Close();
        }
    }
}
