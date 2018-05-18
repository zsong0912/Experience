using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Experience.Framework
{
    internal class CookieAuthorizeHelper : AuthorizeBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="crypto"></param>
        /// <param name="cookieSetting"></param>
        /// <param name="randomKey"></param>
        ICookieSetting CookieSetting { get { return ServiceConfiguration.Configuration.CookieSetting; } }
        public CookieAuthorizeHelper(string randomKey)
            : base(randomKey)
        {
        }
        public T GetIdentity<T>(HttpContext context) where T : IIdentity
        {
            return GetIdentity<T>(new HttpContextWrapper(context));
        }
        public T GetIdentity<T>(HttpContextBase context) where T : IIdentity
        {
            var cookie = context.Request.Cookies[CookieSetting.CookieName];
            if (cookie != null)
            {
                return GetToken<T>(cookie.Value);
            }
            return default(T);
        }
        public T GetIdentity<T>(string token) where T : IIdentity
        {
            if (string.IsNullOrEmpty(token))
            {
                return default(T);
            }
            return GetToken<T>(token);
        }
        public T GetIdentity<T>(HttpRequestMessage requestMessage) where T : IIdentity
        {
            var cookie = requestMessage.Headers.GetCookies(CookieSetting.CookieName).FirstOrDefault();
            if (cookie != null)
            {
                return GetToken<T>(cookie[CookieSetting.CookieName].Value);
            }
            return default(T);
        }
        public void WriteIdentitytoCookie<T>(HttpContextBase context, T identity, bool requireSsl, bool isClear = false) where T : IIdentity
        {
            if (identity != null)
            {
                HttpCookie cookie = new HttpCookie(CookieSetting.CookieName);
                cookie.Secure = requireSsl;
                cookie.HttpOnly = CookieSetting.HttpOnly;
                cookie.Expires = DateTime.UtcNow.AddMinutes(CookieSetting.Expired);
                cookie.Value = CreateToken(identity);
                if (isClear)
                {
                    cookie.Expires = DateTime.UtcNow.AddYears(-1);
                }
                context.Response.AppendCookie(cookie);
            }
        }
    }
}
