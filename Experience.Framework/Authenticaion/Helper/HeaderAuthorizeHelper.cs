using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Experience.Framework
{
    internal class HeaderAuthorizeHelper : AuthorizeBase
    {
        public HeaderAuthorizeHelper()
        {
        }
        const string HeaderKey = "Authorization";
        public T GetIdentity<T>(HttpContextBase context) where T : IIdentity
        {
            if (context.Request.Headers.AllKeys.Contains(HeaderKey))
            {
                return GetIdentityFromToken<T>(context.Request.Headers[HeaderKey]);
            }
            return default(T);
        }
        public T GetIdentity<T>(HttpRequestMessage requestMessage) where T : IIdentity
        {
            if (requestMessage.Headers.Contains(HeaderKey))
            {
                var authHeader = requestMessage.Headers.GetValues(HeaderKey).FirstOrDefault();
                if (authHeader != null && authHeader.Count() > 0)
                {
                    return GetIdentityFromToken<T>(authHeader);
                }
            }
            return default(T);
        }
        public void WriteIdentitytoHeader<T>(HttpContextBase context, T identity) where T : IIdentity
        {
            if (identity != null)
            {
                context.Response.Headers.Add(HeaderKey, "Basic " + CreateToken(identity));
            }
        }
        private T GetIdentityFromToken<T>(string authHeader) where T : IIdentity
        {
            if (string.IsNullOrEmpty(authHeader))
            {
                return default(T);
            }
            AuthenticationHeaderValue authHeaderValues;
            if (!AuthenticationHeaderValue.TryParse(authHeader, out authHeaderValues))
            {
                return default(T);
            }
            return GetToken<T>(authHeaderValues.Parameter);
        }
    }
}
