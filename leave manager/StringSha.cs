using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace leave_manager
{
    /// <summary>
    /// Klasa rozszerzająca klasę String o metody kryptograficzne.
    /// </summary>
   static class StringSha
    {
       /// <summary>
       /// Metoda zwracająca skrót Sha256Managed w postaci ciągu znaków.
       /// </summary>
       /// <param name="value">Ciąg znaków, dla którego obliczany jest skrót.</param>
       /// <returns>Ciąg znaków reprezentujący skrót ciągu znaków, na rzecz którego została wywołana metoda.</returns>
        public static string GetSha256Managed(this string value)
        {
            Byte[] sha256 = new SHA256Managed().ComputeHash(Encoding.ASCII.GetBytes(value));
            String result = "";
            foreach (Byte b in sha256)
                result += b.ToString("X2");
            return result;
        }
    }
}
