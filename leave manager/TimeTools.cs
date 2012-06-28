using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace leave_manager
{
    /// <summary>
    /// Klasa rozszerzeń dla struktury DateTime.
    /// </summary>
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
    }
}
