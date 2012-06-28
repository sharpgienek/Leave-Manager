using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace leave_manager.Exceptions
{
    /// <summary>
    /// Klasa wyjątku występującego w przypadku, gdy wpis w bazie danych już istnieje.
    /// </summary>
    class EntryExistsException : Exception
    {
    }
}
