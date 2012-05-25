using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace leave_manager
{
    class Dictionary
    {
     static public List<LeaveType> GetLeaveTypes(SqlConnection connection)
        {
            List<LeaveType> result = new List<LeaveType>();
            SqlCommand command = new SqlCommand("SELECT LT_ID, Name, Consumes_days FROM Leave_type ORDER BY LT_ID", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new LeaveType((int)reader["LT_ID"], reader["Name"].ToString(), (bool)reader["Consumes_days"]));
            }
            reader.Close();
            return result;
        }

        static public List<String> GetPermissions(SqlConnection connection)
        {
            List<String> result = new List<String>();
            SqlCommand command = new SqlCommand("SELECT Description FROM Permission ORDER BY Permission_ID", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(reader["Description"].ToString());
            }
            reader.Close();
            return result;
        }

        static public List<String> GetPositions(SqlConnection connection)
        {
            List<String> result = new List<string>();
            SqlCommand command = new SqlCommand("SELECT Description FROM Position ORDER BY Position_ID", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(reader["Description"].ToString());
            }
            reader.Close();
            return result;
        }

        static public List<String> GetStatusTypes(SqlConnection connection)
        {
            List<String> result = new List<string>();
            SqlCommand command = new SqlCommand("SELECT Name FROM Status_type ORDER BY ST_ID", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(reader["Name"].ToString());
            }
            reader.Close();
            return result;
        }

    }
}
