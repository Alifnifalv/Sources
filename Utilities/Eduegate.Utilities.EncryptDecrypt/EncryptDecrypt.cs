using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Framework.Security;

namespace Eduegate.Utilities.EncryptDecrypt
{
    public static class EncryptDecrypt
    {
        public static string[,] Encrypt(string[] keys, string passPhrase)
        {
            var stringArray = new string[keys.Length, 2];
            var i = 0;
            foreach (var key in keys)
            {
                stringArray[i, 0] = key;
                stringArray[i, 1] = StringCipher.Encrypt(key, passPhrase);
                i++;
            }
            return stringArray;
        }

        public static string[,] Decrypt(string[] keys, string passPhrase)
        {
            var stringArray = new string[keys.Length,2];
            var i = 0;
            foreach (var key in keys)
            {
                var enkey = EncodeBase64(key);
                stringArray[i, 0] = key;
                stringArray[i, 1] = StringCipher.Decrypt(enkey, passPhrase);
                i++;
            }
            return stringArray;
        }

        public static string EncodeBase64(string data)
        {
            string s = data.Replace(" ", "+");
            if (s.Length % 4 > 0)
                s = s.PadRight(s.Length + 4 - s.Length % 4, '=');
            return s;
        }
    }
}
