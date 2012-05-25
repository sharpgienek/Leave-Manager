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
    public partial class FormDeleteLeaveType : Form
    {            
        private SqlConnection connection;
        private String replacedLeaveType;
       // private List<LeaveType> leaveTypes;
        public FormDeleteLeaveType(SqlConnection connection, String replacedLeaveType)
        {
            InitializeComponent();
           List<LeaveType> leaveTypes = Dictionary.GetLeaveTypes(connection);
            leaveTypes.Remove(new LeaveType(replacedLeaveType));
            comboBoxPositions.DataSource = leaveTypes;
            this.connection = connection;
            this.replacedLeaveType = replacedLeaveType;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            SqlTransaction transaction = connection.BeginTransaction();//todo try catch
            SqlCommand commandUpdate = new SqlCommand("UPDATE Leave SET LT_ID = " +
                "(SELECT LT_ID FROM Leave_type WHERE Name = @Name_new) " +
                "WHERE LT_ID = " +
                "(SELECT LT_ID FROM Leave_type WHERE Name = @Name_replaced)", connection, transaction);
            commandUpdate.Parameters.Add("@Name_new", SqlDbType.VarChar).Value = comboBoxPositions.SelectedValue.ToString();
            commandUpdate.Parameters.Add("@Name_replaced", SqlDbType.VarChar).Value = replacedLeaveType;
           commandUpdate.ExecuteNonQuery();
           SqlCommand commandDelete = new SqlCommand("DELETE FROM Leave_type WHERE " +
               "Name = @Name", connection, transaction);
           commandDelete.Parameters.Add("@Name", SqlDbType.VarChar).Value = replacedLeaveType;
           commandDelete.ExecuteNonQuery();
            transaction.Commit();
            this.Close();
        }
       
    }
}
