using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace leave_manager.Exceptions
{
    /// <summary>
    /// Wyjątek wyrzucany w przypadku gdy w bazie danych nie ma podanego rodzaju uprawnień
    /// </summary>
    class PermissionException : Exception
    {
    }
}
