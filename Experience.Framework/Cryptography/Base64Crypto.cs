using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experience.Framework
{
    public class Base64Crypto
    {
        #region -- Base64 --
        /// <summary>
        /// Base64 加密
        /// </summary>
        /// <param name="encode"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string EncodeBase64(Encoding encode, string source)
        {
            byte[] bytes = encode.GetBytes(source);
            try
            {
                return Convert.ToBase64String(bytes);
            }
            catch (Exception)
            {
                return source;
            }
        }

        /// <summary>
        /// Base64 解密
        /// </summary>
        /// <param name="encode"></param>
        /// <param name="sourceString"></param>
        /// <returns></returns>
        public static string DecodeBase64(Encoding encode, string sourceString)
        {
            byte[] bytes = Convert.FromBase64String(sourceString);
            try
            {
                return encode.GetString(bytes);
            }
            catch (Exception)
            {
                return sourceString;
            }
        }
        #endregion
    }
}
