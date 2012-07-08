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
        /// <summary>
        /// Metoda przyninająca wartość daty.
        /// </summary>
        /// <param name="date">Data do przycięcia.</param>
        /// <param name="roundTicks">Liczba tyknięć do której ma być przycięta (np. liczba tyknięć na dzień).</param>
        /// <returns>Przyciętą datę.</returns>
       static public DateTime Trim(this DateTime date, long roundTicks)
        {           
             return new DateTime(date.Ticks - date.Ticks % roundTicks);                     
        }
    }
}
