using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experience.Framework
{
    public interface ITokenCrypto
    {
        string GetRandomKey();
        string Encrypt(string sourceString, string randomKey);
        string Decrypt(string encryptedString, string randomKey);
    }
}
