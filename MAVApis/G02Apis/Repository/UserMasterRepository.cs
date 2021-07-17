using G02Apis.Models;
using Hvit.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace G02Apis.Repository
{
    public class UserMasterRepository : IDisposable
    {
        // SECURITY_DBEntities it is your context class
        MaiAnVatEntities context = new MaiAnVatEntities();
        //This method is used to check and validate the user credentials
        public User ValidateUser(string username, string password)
        {
            var user = context.Users.FirstOrDefault(x => x.UserName == username);
            string passwordSalt = user.PasswordSalt;
            string passwordInput = AuthenticationHelper.GetMd5Hash(passwordSalt + password);
            string passwordUser = user.PasswordHash;
            if (!passwordInput.Equals(passwordUser))
                return null;
            return user;
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }

    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using (UserMasterRepository _repo = new UserMasterRepository())
            {
                var user = _repo.ValidateUser(context.UserName, context.Password);
                if (user == null)
                {
                    context.SetError("invalid_grant", "Provided username and password is incorrect");
                    return;
                }
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new System.Security.Claims.Claim(ClaimTypes.Role, user.UserName));
                identity.AddClaim(new System.Security.Claims.Claim(ClaimTypes.Name, user.UserName));
                identity.AddClaim(new System.Security.Claims.Claim("Email", user.Email));

                context.Validated(identity);
            }
        }
    }
}
