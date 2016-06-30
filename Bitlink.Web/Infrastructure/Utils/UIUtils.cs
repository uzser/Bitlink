using Bitlink.Web.Infrastructure.Resources;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

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

        public static bool TryParseUrl(string url, out string formattedUrl)
        {
            formattedUrl = null;
            Uri uri;
            if (!Uri.TryCreate(url.Replace(@"\", "/"), UriKind.Absolute, out uri))
                return false;

            var uriString = uri.ToString();

            #region Removing duplicate slashes
            const string schemeDelimeter = ":/";
            var indexOfSchemeEnd = uriString.IndexOf(schemeDelimeter, StringComparison.Ordinal) + schemeDelimeter.Length;
            var scheme = uriString.Substring(0, indexOfSchemeEnd);
            uriString = scheme + uriString.Substring(indexOfSchemeEnd).Replace("//", "/").Replace("//", "/");
            #endregion

            var regex =
                new Regex(UIRes.UrlValidationRegex);

            var isValidUrl = regex.IsMatch(uriString);
            if (isValidUrl)
                formattedUrl = uriString;

            return isValidUrl;
        }

        public static string GetShortUrl(HttpRequestMessage request, string hash)
        {
            return new Uri(request.RequestUri, "/" + hash).ToString();
        }
    }
}