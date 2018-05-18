using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experience.Framework
{
    public interface ITokenCrypto
    {
        /// <summary>
        /// 程序启动之后此值不能在变
        /// </summary>
        string DefaultKey { get; }
        string GetRandomKey();
        string Encrypt(string sourceString, string randomKey);
        string Decrypt(string encryptedString, string randomKey);
    }
}
