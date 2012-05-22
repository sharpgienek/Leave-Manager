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
    public partial class FormManager : Form
    {
        private SqlConnection connection;
        public FormManager(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
        }

        private void refreshDataGridViewEmployees(Object o, EventArgs e)
        {
            refreshDataGridViewEmployees();
        }

        private void refreshDataGridViewEmployees()
        {
            SqlCommand command = new SqlCommand("SELECT E.Name, E.Surname, E.Birth_date AS 'Birth date'," +
                "E.Address, E.PESEL, E.EMail AS 'e-mail', Pos.Description AS Position, " +
                "Perm.Description AS Permission, E.Leave_days AS 'Remaining leave days', "+
                "E.Old_leave_days AS 'Old left leave days' " +
                "FROM Employee E, Position Pos, Permission Perm " +
                "WHERE E.Permission_ID = Perm.Permission_ID " +
                "AND E.Position_ID = Pos.Position_ID", connection);

            SqlDataReader reader = command.ExecuteReader();
            DataTable employees = new DataTable();
            employees.Load(reader);
            reader.Close();
            dataGridViewEmployees.DataSource = employees;
        }

        private void buttonEmployeesAdd_Click(object sender, EventArgs e)
        {
            FormAddEmployee form = new FormAddEmployee(connection);
            form.Show();
            form.FormClosed += new FormClosedEventHandler(this.refreshDataGridViewEmployees);
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab.Text.Equals("Employees"))
                refreshDataGridViewEmployees();
        }      
    }
}
