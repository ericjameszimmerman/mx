using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace mx.core
{
    /// <summary>
    ///     Make this a singleton
    /// </summary>
    public class HashGen
    {
        private static HashGen instance = null;

        private UnicodeEncoding encoding;
        private SHA1Managed hash;

        private HashGen()
        {
            //Create a new instance of the UnicodeEncoding class to 
            //convert the string into an array of Unicode bytes.
            this.encoding = new UnicodeEncoding();
            this.hash = new SHA1Managed();
        }

        public static HashGen Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HashGen();
                }
                return instance;
            }
        }

        public string GenerateHash(string content)
        {
            byte[] hashValue;
            
            //Convert the string into an array of bytes.
            byte[] contentBytes = encoding.GetBytes(content);
            
            hashValue = hash.ComputeHash(contentBytes);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashValue)
            {
                sb.AppendFormat("{0:x2}", b);
            }

            return sb.ToString();
        }
    }
}
