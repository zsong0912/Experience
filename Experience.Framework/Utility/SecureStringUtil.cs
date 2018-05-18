using Experience.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experience.Framework
{
    public class SecureStringUtil
    {
        public static string Encrypt(ICrypto cryptoProxy, string secureStr)
        {
            if (secureStr.StartsWith("@Sec{") && secureStr.EndsWith("}@"))
            {
                return secureStr;
            }
            else
            {
                string secure = cryptoProxy.Encrypt(secureStr);
                return "@Sec{" + secure + "}@";
            }
        }
        public static string Decrypt(ICrypto cryptoProxy, string secureStr)
        {
            if (secureStr.StartsWith("@Sec{") && secureStr.EndsWith("}@"))
            {
                string str = secureStr.Replace("@Sec{", "").Replace("}@", "");
                return cryptoProxy.Decrypt(str);
            }
            else
            {
                return secureStr;
            }
        }
    }
}
