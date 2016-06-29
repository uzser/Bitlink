using Bitlink.Web.Infrastructure.Resources;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Bitlink.Web.Infrastructure.Utils
{
    public static class UIUtils
    {
        public static Guid GetUserUid(HttpRequestMessage request, out bool isNew)
        {
            var userUidCookie = request.Headers.GetCookies(UIRes.UserUidCookieName).FirstOrDefault();
            Guid userUid;
            if (userUidCookie == null || !Guid.TryParse(userUidCookie[UIRes.UserUidCookieName].Value, out userUid))
            {
                isNew = true;
                return Guid.NewGuid();
            }
            isNew = false;
            return userUid;
        }

        public static void SetUserUid(HttpRequestMessage request, HttpResponseMessage response)
        {
            var cookie = new CookieHeaderValue(UIRes.UserUidCookieName, Guid.NewGuid().ToString())
            {
                Expires = DateTimeOffset.Now.AddYears(100),
                Domain = request.RequestUri.Host,
                Path = "/"
            };

            response.Headers.AddCookies(new[] { cookie });
        }
    }
}