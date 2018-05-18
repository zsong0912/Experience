using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Experience.Framework
{
    public class RuntimeCache : ICache
    {
        private RuntimeCache() { }

        public void Insert(string category, string key, object value)
        {
            HttpRuntime.Cache.Insert(category + key, value);
        }

        public void Insert(string category, string key, object value, DateTime absoluteExpiration)
        {
            throw new NotImplementedException();
        }

        public void Insert(string category, string key, object value, TimeSpan slidingExpiration)
        {
            HttpRuntime.Cache.Insert(category + key, value, null, Cache.NoAbsoluteExpiration, slidingExpiration);
        }

        public void Remove(string category, string key)
        {
            HttpRuntime.Cache.Remove(category + key);
        }

        public object Get(string category, string key)
        {
            return HttpRuntime.Cache.Get(category + key);
        }

        public object this[string category, string key]
        {
            get
            {
                return HttpRuntime.Cache[category + key];
            }
            set
            {
                HttpRuntime.Cache[category + key] = value;
            }
        }

        public object this[string category, string key, TimeSpan slidingExpiration]
        {
            get
            {
                return HttpRuntime.Cache[category + key];
            }
            set
            {
                HttpRuntime.Cache.Insert(category + key, value, null, Cache.NoAbsoluteExpiration, slidingExpiration);
            }
        }

        int ICache.Count
        {
            get { return HttpRuntime.Cache.Count; }
        }
    }
}
