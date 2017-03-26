using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ProjectX.Shared
{
    public class HashPassword
    {
        public static string hashUserPassword(string password)
        {
            //sha256 zbog nedavnog collision attacka na sha1
            SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
            sha256.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));
            byte[] re = sha256.Hash;

            StringBuilder sb = new StringBuilder();

            foreach (byte b in re)
                sb.Append(b.ToString("x2"));

            string retHash = sb.ToString();

            return retHash;
        }
    }
}
