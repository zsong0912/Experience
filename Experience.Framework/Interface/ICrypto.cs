﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experience.Framework
{
    public interface ICrypto
    {
        string Encrypt(string sourceString);
        string Decrypt(string encryptedString);
    }
}
