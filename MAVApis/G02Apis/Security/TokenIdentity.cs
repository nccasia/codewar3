using System;
using System.Security.Principal;

namespace Hvit.Security
{
    public class TokenIdentity: IIdentity
    {
        private string _AuthenticationType;
        private bool _IsAuthenticated;
        public string Token { get; set; }
        public string UserName { get; set; }
        public string Name { get { return UserName; } }
        public string UserAgent { get; set; }
        public string IP { get; set; }
        public long EffectiveTime { get; set; }
        public int ExpiresTime { get; set; }
        public string AuthenticationType { get { return _AuthenticationType; } }
        public bool IsAuthenticated { get { return _IsAuthenticated; } }

        public void SetAuthenticationType(string authenticationType)
        {
            _AuthenticationType = authenticationType;
        }
        public void SetIsAuthenticated(bool isAuthenticated)
        {
            _IsAuthenticated = isAuthenticated;
        }
        public TokenIdentity() {}
        public TokenIdentity(string token, string username, string userAgent, string ip, long effectiveTime, int expiresTime)
        {
            Token = token;
            UserName = username;
            UserAgent = userAgent;
            IP = ip;
            EffectiveTime = effectiveTime;
            ExpiresTime = expiresTime;
        }

    }
}
