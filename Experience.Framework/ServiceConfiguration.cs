using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experience.Framework
{
    public class ServiceConfiguration
    {
        private static ServiceConfiguration _configuration;
        static ServiceConfiguration()
        {
            _configuration = new ServiceConfiguration();
        }

        private ServiceConfiguration()
        {
        }

        public static ServiceConfiguration Configuration { get { return _configuration; } }

        public ITokenCrypto TokenCrypto { get; set; }
        public ICookieSetting CookieSetting { get; set; }
    }
}
