using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System;
using MaiAnVat.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using MaiAnVat.Common;
using System.Security.Claims;
using System.Collections.Generic;

namespace MaiVanVat.Security
{
    public class TokenProvider : ITokenProvider
    {
        private int _expirationSeconds { get { return 360000; } }

        public TokenIdentity GenerateToken(User user, string userAgent, string ip, string guid, long effectiveTime)
        {
            TokenIdentity tokenIdentity = new TokenIdentity(null, user.Id, user.UserName, userAgent, ip, effectiveTime, _expirationSeconds);
            var claims = new List<System.Security.Claims.Claim>();
            claims.Add(new System.Security.Claims.Claim(ClaimTypes.Role, "Administrator"));
            claims.Add(new System.Security.Claims.Claim(ClaimTypes.Role, "Admin"));
            claims.Add(new System.Security.Claims.Claim(MAVClaimTypes.kMAVClaimTypesUserFK, user.Id.ToString()));
            var SymmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(MAVClaimTypes.kMAVSecretKey));
            var signingCredentials = new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenCustom = new JwtSecurityToken(MAVClaimTypes.kMAVIssuer, MAVClaimTypes.kMAVAudience, claims, expires: DateTime.Now.AddSeconds(_expirationSeconds), signingCredentials: signingCredentials);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenCustom);
            tokenIdentity.Token = tokenString;
            return tokenIdentity;
        }

        public bool ValidateToken(ref TokenIdentity tokenIdentity)
        {
            bool result = false;

            try
            {
                //tokenIdentity.SetAuthenticationType("Custom");
                //// Base64 decode the string, obtaining the token:guid:username:timeStamp.
                //string key = Encoding.UTF8.GetString(Convert.FromBase64String(tokenIdentity.Token));

                //// Split the parts.
                //string[] parts = key.Split(new char[] { ':' });
                //if (parts.Length == 5)
                //{
                //    // Get the hash message, username, and timestamp.
                //    string hash = parts[0];
                //    string guid = parts[1];
                //    int userId = int.Parse(parts[2]);
                //    string username = parts[3];
                //    long ticks = long.Parse(parts[4]);
                //    tokenIdentity.EffectiveTime = ticks;
                //    DateTime timeStamp = new DateTime(ticks);

                //    // Ensure the timestamp is valid.
                //    bool expired = Math.Abs((DateTime.Now.AddHours(7) - timeStamp).TotalSeconds) > _expirationSeconds;
                //    if (!expired)
                //    {
                //        // Hash the message with the key to generate a token.
                //        string computedToken = GenerateToken(userId, username, tokenIdentity.UserAgent, tokenIdentity.IP, guid, ticks).Token;

                //        // Compare the computed token with the one supplied and ensure they match.
                //        if (tokenIdentity.Token.Equals(computedToken))
                //        {
                //            using (MaiAnVatContext db = new MaiAnVatContext())
                //            {
                //                AccessTokens accessToken = db.AccessTokens.SingleOrDefault(x => x.Token == computedToken);
                //                //connection.Open();
                //                //AccessToken accessToken = connection.QuerySingleOrDefault<AccessToken>(SchemaAuth.AccessTokens_GetByToken, new { Token = computedToken }, commandType: System.Data.CommandType.StoredProcedure);
                //                if (accessToken != null
                //                    && Math.Abs((DateTime.Now - accessToken.EffectiveTime).TotalSeconds) < _expirationSeconds
                //                    && accessToken.UserName.Equals(username))
                //                {
                //                    result = true;
                //                    tokenIdentity.SetIsAuthenticated(true);
                //                    tokenIdentity.UserName = username;
                //                    tokenIdentity.UserId = userId;
                //                }
                //            }
                //        }
                //    }
                //}
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
