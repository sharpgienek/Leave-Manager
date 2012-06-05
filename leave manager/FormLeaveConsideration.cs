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
        //private SqlConnection connection;
        private DatabaseOperator databaseOperator;
        private int employeeId;
        private DateTime firstDay;
        private DateTime lastDay;
      //  private String aprovalStatus;
        private object parent;
      //  public FormLeaveConsideration(SqlConnection connection, int employeeId, DateTime firstDay, DateTime lastDay, String aprovalStatus)
        public FormLeaveConsideration(DatabaseOperator databaseOperator, int employeeId, DateTime firstDay, DateTime lastDay, object parent)
        {
            InitializeComponent();
            this.databaseOperator = databaseOperator;
            this.employeeId = employeeId;
            this.firstDay = firstDay;
            this.lastDay = lastDay;
          //  this.aprovalStatus = aprovalStatus;
            this.parent = parent;
          /*  SqlCommand command = new SqlCommand("SELECT E.Name, E.Surname, P.Description AS 'Position' " +
                "FROM Employee E, Position P WHERE E.Employee_ID = @Employee_ID " +
                "AND E.Position_ID = P.Position_ID", connection);
            command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
            SqlDataReader reader = command.ExecuteReader();//todo try catch
            reader.Read();*/
            Employee employee = new Employee();
            if (databaseOperator.getEmployee(employeeId, ref employee))
            {
                labelNameValue.Text = employee.Name;
                labelPositionValue.Text = employee.Position;
            }
            else
            {
                //todo error message
            }
            labelFirstDayValue.Text = firstDay.ToString("d");
            labelLastDayValue.Text = lastDay.ToString("d");
        }

        private void buttonLeaveUnchanged_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            if (databaseOperator.acceptLeave(parent, employeeId, firstDay))
            {
                this.Close();
            }
            else
            {
                //todo error message;
            }
            
        }

        private void buttonReject_Click(object sender, EventArgs e)
        {
           /* SqlCommand command = new SqlCommand("UPDATE Leave SET LS_ID = (SELECT ST_ID FROM " +
                "Status_type WHERE Name = @Name) WHERE Employee_ID = @Employee_ID " +
                "AND First_day = @First_day ", connection);
            command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeId;
            command.Parameters.Add("@First_day", SqlDbType.Date).Value = firstDay;
            command.Parameters.Add("@Name", SqlDbType.VarChar).Value = "Rejected";
            command.ExecuteNonQuery();*///todo try catch;
            if (databaseOperator.rejectLeave(employeeId, firstDay))
            {
                this.Close();
            }
            else
            {
                //todo error message
            }
        }

    }
}
