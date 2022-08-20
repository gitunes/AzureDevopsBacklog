using AzureDevopsBacklog.Contants;
using System.Collections.Specialized;
using System.Text;

namespace AzureDevopsBacklog.Application.Methods
{
    public static class HelperMethods
    {
        public static string GetBasicAuthenticationToken(string username, string password)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
        }

        public static NameValueCollection GetHeaderCollection(string key, string token, string prefix)
        {
            NameValueCollection nameValueCollection = new()
            {
                { key, $"{(!string.IsNullOrEmpty(prefix) ? $"{prefix} " : string.Empty)}{token}" }
            };
            return nameValueCollection;
        }

        public static NameValueCollection GetAuthorizationHeaderCollection(string username, string password)
        {
            var token = GetBasicAuthenticationToken(username, password);
            return GetHeaderCollection(HeaderKeys.Authorization, token, Prefixes.Basic);
        }
    }
}
