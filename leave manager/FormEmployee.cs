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
            refreshDataGridView();
        }

        private void refreshDataGridView(object o, FormClosedEventArgs e)
        {
            refreshDataGridView();
        }

        private void refreshDataGridView()
        {
            SqlCommand command = new SqlCommand("SELECT LS.Name AS 'Status', L.First_day AS 'First day', " +
                "L.Last_day AS 'Last day', LT.Name AS 'Type', LT.Consumes_days FROM Leave L, Leave_type LT, Status_type LS " +
                "WHERE L.LT_ID = LT.LT_ID AND L.LS_ID = LS.ST_ID", connection);
            SqlDataReader reader = command.ExecuteReader();
            DataTable data = new DataTable();
            data.Load(reader);
            reader.Close();
            data.Columns.Add("No. used days");
            for (int i = 0; i < data.Rows.Count; i++ )
            {
                if ((bool)data.Rows[i]["Consumes_days"])
                {
                    data.Rows[i]["No. used days"] = TimeTools.GetNumberOfWorkDays((DateTime)data.Rows[i]["First day"],
                        (DateTime)data.Rows[i]["Last day"]);
                }
                else
                    data.Rows[i]["No. used days"] = 0;
            }
            data.Columns.Remove("Consumes_days");
            dataGridView.DataSource = data;
        }

        private void buttonChangeLoginOrPassword_Click(object sender, EventArgs e)
        {
            FormChangeLoginOrPassword form = new FormChangeLoginOrPassword(connection, employee.EmployeeID);
            form.Show();
        }

        private void refreshEmployeeDays(object o, FormClosedEventArgs e)
        {
            SqlCommand command = new SqlCommand("Select Leave_days, Old_leave_days " +
                "FROM Employee WHERE Employee_ID = @Employee_ID", connection);
            command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employee.EmployeeID;
            SqlDataReader reader = command.ExecuteReader();//todo try catch
            reader.Read();
            employee.LeaveDays = (int)reader["Leave_days"];
            employee.OldLeaveDays = (int)reader["Old_leave_days"];
            reader.Close();
            labelDaysToUseValue.Text = (employee.LeaveDays + employee.OldLeaveDays).ToString();
            
        }

        private void buttonTakeLeave_Click(object sender, EventArgs e)
        {
            FormEmployeeTakeLeave form = new FormEmployeeTakeLeave(connection, employee);
            form.FormClosed += new FormClosedEventHandler(refreshEmployeeDays);
            form.FormClosed += new FormClosedEventHandler(refreshDataGridView);
            form.Show();
        }
    }
}
