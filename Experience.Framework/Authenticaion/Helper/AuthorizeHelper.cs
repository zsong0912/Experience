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
    public class AuthorizeHelper
    {
        static CookieAuthorizeHelper CookieAuthorize = new CookieAuthorizeHelper();
        static HeaderAuthorizeHelper HeaderAuthorize = new HeaderAuthorizeHelper();
        public static bool CanAccess<T>(T identity) where T : IIdentity
        {
            if (identity != null && identity.IsAuthenticated)
            {
                return true;
            }
            return false;
        }
        public static T GetIdentity<T>(HttpContext context) where T : IIdentity
        {
            return GetIdentity<T>(new HttpContextWrapper(context));
        }
        public static T GetIdentity<T>(HttpContextBase context) where T : IIdentity
        {
            try
            {
                var identity = CookieAuthorize.GetIdentity<T>(context);
                if (identity == null)
                {
                    return HeaderAuthorize.GetIdentity<T>(context);
                }
                return identity;
            }
            catch (Exception)
            {
            }
            return default(T);
        }
        public static T GetIdentity<T>(HttpRequestMessage requestMessage) where T : IIdentity
        {
            try
            {
                var identity = CookieAuthorize.GetIdentity<T>(requestMessage);
                if (identity == null)
                {
                    return HeaderAuthorize.GetIdentity<T>(requestMessage);
                }
                return identity;
            }
            catch (Exception)
            {
            }
            return default(T);
        }
        public static void WriteIdentity<T>(HttpContextBase context, T identity, bool requireSsl, bool isClear = false) where T : IIdentity
        {
            if (identity != null)
            {
                CookieAuthorize.WriteIdentitytoCookie(context, identity, requireSsl, isClear);
                if (!isClear)
                {
                    HeaderAuthorize.WriteIdentitytoHeader(context, identity);
                }
            }
        }
    }
}
