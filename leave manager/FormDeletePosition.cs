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
    public partial class FormDeletePosition : Form
    {        
       // private String newPosition;
       // public String NewPosition { get { return newPosition; } }
        private SqlConnection connection;
        private String replacedPosition;
        public FormDeletePosition(SqlConnection connection, String replacedPosition)
        {
            InitializeComponent();
            List<String> positions = Dictionary.GetPositions(connection);
            positions.Remove(replacedPosition);
            comboBoxPositions.DataSource = positions;
            this.connection = connection;
            this.replacedPosition = replacedPosition;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            SqlTransaction transaction = connection.BeginTransaction();//todo try catch
            SqlCommand commandUpdate = new SqlCommand("UPDATE Employee SET Position_ID = " +
                "(SELECT Position_ID FROM Position WHERE Description = @Description_new) " +
                "WHERE Position_ID = " +
                "(SELECT Position_ID FROM Position WHERE Description = @Description_replaced)", connection, transaction);
            commandUpdate.Parameters.Add("@Description_new", SqlDbType.VarChar).Value = comboBoxPositions.SelectedValue.ToString();
            commandUpdate.Parameters.Add("@Description_replaced", SqlDbType.VarChar).Value = replacedPosition;
           commandUpdate.ExecuteNonQuery();
           SqlCommand commandDelete = new SqlCommand("DELETE FROM Position WHERE " +
               "Description = @Description", connection, transaction);
           commandDelete.Parameters.Add("@Description", SqlDbType.VarChar).Value = replacedPosition;
           commandDelete.ExecuteNonQuery();
            transaction.Commit();
            this.Close();
        }
       
    }
}
