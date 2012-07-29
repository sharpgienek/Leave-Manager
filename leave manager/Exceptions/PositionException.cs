using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace leave_manager.Exceptions
{
    /// <summary>
    /// Wyjątek wyrzucany gdy w bazie danych nie znaleziono podanej pozycji
    /// </summary>
    class PositionException : Exception
    {
         string msg;
        /// <summary>
        /// Konstruktor, ustawia domyślna informacje
        /// </summary>
        public PositionException()
        {
            msg = "Position not found";
        }

        /// <summary>
        /// Metoda dostępu do pola klasy.
        /// </summary>
        public override string Message
        {
            get
            {
                return msg + "Exception";
            }
        }
    }
}
