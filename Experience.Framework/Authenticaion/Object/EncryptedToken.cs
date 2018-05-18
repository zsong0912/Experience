using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Experience.Framework
{
    [DataContract]
    [Serializable]
    public class EncryptedToken
    {
        [DataMember]
        public byte[] Token { get; set; }
        [DataMember]
        public byte[] RandomKey { get; set; }
    }
}
