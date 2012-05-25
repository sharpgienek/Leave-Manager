using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace leave_manager
{
    static class TimeTools
    {
        static public DateTime Trim(this DateTime date, long roundTicks)
        {           
             return new DateTime(date.Ticks - date.Ticks % roundTicks);                     
        }

        static public int GetNumberOfWorkDays(this DateTime date1, DateTime date2)
        {    
            TimeSpan timeSpan = date1 - date2;
            if (timeSpan.TotalDays < 0)
                timeSpan = timeSpan.Negate();
            int numberOfDays = (int)Math.Round(timeSpan.TotalDays) + 1;
            while (date1.CompareTo(date2) <= 0)
            {
                if (date1.DayOfWeek == DayOfWeek.Saturday || date1.DayOfWeek == DayOfWeek.Sunday)
                    numberOfDays--;
                date1 = date1.AddDays(1);
            }
            return numberOfDays;
        }

        static public bool IsDateFromPeriodUsed(this DateTime date1, DateTime date2, 
            SqlConnection connection, int employeeID)
        {    
            if (date1.CompareTo(date2) > 0)
            {
                DateTime tmpDate = date1;
                date1 = date2;
                date2 = tmpDate;
            }
            SqlCommand command = new SqlCommand("SELECT First_day, Last_day FROM Leave WHERE " +
                "Employee_ID = @Employee_ID", connection);
            command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeID;
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                DateTime firstDay = (DateTime)reader["First_day"];
                DateTime lastDay = (DateTime)reader["Last_day"];
                if ((date1.CompareTo(firstDay) <= 0) && (date2.CompareTo(firstDay) >= 0)//firstDay between date1 & date2
                    || (date1.CompareTo(lastDay) <= 0) && (date2.CompareTo(lastDay) >= 0)//lastDay between date1 & date2
                    || (date1.CompareTo(firstDay) >= 0 && (date2.CompareTo(lastDay) <= 0)))//date1 & date2 between firstDay & lastDay
                {
                    reader.Close();
                    return true;
                }
            }
            reader.Close();
            return false;
        }

        static public bool IsDateFromPeriodUsed(this DateTime date1, DateTime date2,
           SqlConnection connection, int employeeID, DateTime skippedEntryFirstDay)
        {
            if (date1.CompareTo(date2) > 0)
            {
                DateTime tmpDate = date1;
                date1 = date2;
                date2 = tmpDate;
            }
            SqlCommand command = new SqlCommand("SELECT First_day, Last_day FROM Leave WHERE " +
                "Employee_ID = @Employee_ID AND First_day != @Skipped_entry_first_day", connection);
            command.Parameters.Add("@Employee_ID", SqlDbType.Int).Value = employeeID;
            command.Parameters.Add("@Skipped_entry_first_day", SqlDbType.Date).Value = skippedEntryFirstDay;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                DateTime firstDay = (DateTime)reader["First_day"];
                DateTime lastDay = (DateTime)reader["Last_day"];
                if ((date1.CompareTo(firstDay) <= 0) && (date2.CompareTo(firstDay) >= 0)//firstDay between date1 & date2
                    || (date1.CompareTo(lastDay) <= 0) && (date2.CompareTo(lastDay) >= 0)//lastDay between date1 & date2
                    || (date1.CompareTo(firstDay) >= 0 && (date2.CompareTo(lastDay) <= 0)))//date1 & date2 between firstDay & lastDay
                {
                    reader.Close();
                    return true;
                }
            }
            reader.Close();
            return false;
        }
    }
}
