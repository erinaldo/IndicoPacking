using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IndicoPacking.Common
{
    public static class Security
    {
        public static string EncryptPassword(string password, string salt)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] src = Convert.FromBase64String(salt);

            return Convert.ToBase64String(new SHA1Managed().ComputeHash(src.Concat(bytes).ToArray()));
        }
    }
}
