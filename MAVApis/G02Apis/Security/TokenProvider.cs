using G02Apis.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Hvit.Security
{
    public class TokenProvider : ITokenProvider
    {
        private string _secretKey { get { return "Aff^*Hn43pmf@7hTU%#R01Nnmsk_lK9"; } }
        private int _expirationSeconds { get { return 360000; } }

        public TokenIdentity GenerateToken(string username, string userAgent, string ip, string guid, long effectiveTime)
        {
            TokenIdentity tokenIdentity = new TokenIdentity(null, username, userAgent, ip, effectiveTime, _expirationSeconds);
            string hashLeft = "";
            string hash = string.Join(":", new string[] {
                username,
                userAgent,
                // ip,
                guid,
                tokenIdentity.EffectiveTime.ToString()
            });

            using (HMAC hmac = new HMACSHA256())
            {
                hmac.Key = Encoding.UTF8.GetBytes(_secretKey);
                hashLeft = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(hash)));
                tokenIdentity.Token = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(":", hashLeft, guid, username, tokenIdentity.EffectiveTime)));
            }

            return tokenIdentity;
        }

        public bool ValidateToken(ref TokenIdentity tokenIdentity)
        {
            bool result = false;

            try
            {
                tokenIdentity.SetAuthenticationType("Custom");
                // Base64 decode the string, obtaining the token:guid:username:timeStamp.
                string key = Encoding.UTF8.GetString(Convert.FromBase64String(tokenIdentity.Token));

                // Split the parts.
                string[] parts = key.Split(new char[] { ':' });
                if (parts.Length == 4)
                {
                    // Get the hash message, username, and timestamp.
                    string hash = parts[0];
                    string guid = parts[1];
                    string username = parts[2];
                    long ticks = long.Parse(parts[3]);
                    tokenIdentity.EffectiveTime = ticks;
                    DateTime timeStamp = new DateTime(ticks);

                    // Ensure the timestamp is valid.
                    bool expired = Math.Abs((DateTime.Now.AddHours(7) - timeStamp).TotalSeconds) > _expirationSeconds;
                    if (!expired)
                    {
                        // Hash the message with the key to generate a token.
                        string computedToken = GenerateToken(username, tokenIdentity.UserAgent, tokenIdentity.IP, guid, ticks).Token;

                        // Compare the computed token with the one supplied and ensure they match.
                        if (tokenIdentity.Token.Equals(computedToken))
                        {
                            using (MaiAnVatEntities db = new MaiAnVatEntities())
                            {
                                AccessToken accessToken = db.AccessTokens.SingleOrDefault(x => x.Token == computedToken);
                                //connection.Open();
                                //AccessToken accessToken = connection.QuerySingleOrDefault<AccessToken>(SchemaAuth.AccessTokens_GetByToken, new { Token = computedToken }, commandType: System.Data.CommandType.StoredProcedure);
                                if (accessToken != null
                                    && Math.Abs((DateTime.Now - accessToken.EffectiveTime).TotalSeconds) < _expirationSeconds
                                    && accessToken.UserName.Equals(username))
                                {
                                    result = true;
                                    tokenIdentity.SetIsAuthenticated(true);
                                    tokenIdentity.UserName = username;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }

            return result;
        }
    }
}
