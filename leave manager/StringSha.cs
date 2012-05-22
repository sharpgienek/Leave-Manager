using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace leave_manager
{
    class StringSha
    {
        public static string GetSha256Managed(string value)
        {
            Byte[] sha256 = new SHA256Managed().ComputeHash(Encoding.ASCII.GetBytes(value));
            String result = "";
            foreach (Byte b in sha256)
                result += b.ToString("X2");
            return result;
        }
    }
}
