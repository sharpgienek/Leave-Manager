using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace leave_manager.Exceptions
{
    /// <summary>
    /// Klasa wyjątku rzucanego w przypadku, gdy program nie może znaleźć 
    /// wolnego loginu z puli losowanych loginów.
    /// </summary>
    class NoFreeLoginException : Exception
    {
    }
}
