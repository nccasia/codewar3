using MaiAnVat.Common;
using MaiAnVat.Models;
using MaiAnVat.Models.CustomModels;
using MaiVanVat.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace MaiAnVat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        public class LoginForm
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string RePassword { get; set; }
            public string ChangePasswordKey { get; set; }
            public string OldPassword { get; set; }
        }

        public class UserModel
        {
            public User User { get; set; }
            public List<int> Groups { get; set; }

            public UserModel()
            {
                Groups = new List<int>();
            }
        }
        [HttpPost]
        public IActionResult Login([FromBody] LoginForm login)
        {
            using (MaiAnVatContext db = new MaiAnVatContext())
            {
                User user = db.User.SingleOrDefault(x => x.UserName == login.Username);
                if (user != null)
                {
                    string passwordSalt = user.PasswordSalt;
                    string passwordInput = AuthenticationHelper.GetMd5Hash(passwordSalt + login.Password);
                    string passwordUser = user.PasswordHash;

                    if (passwordInput.Equals(passwordUser))
                    {
                        TokenProvider tokenProvider = new TokenProvider();
                        TokenIdentity token = tokenProvider.GenerateToken(user,
                            Request.Headers["User-Agent"].ToString(),
                            "", Guid.NewGuid().ToString(),
                            DateTime.Now.Ticks);
                        token.SetAuthenticationType("Custom");
                        token.SetIsAuthenticated(true);
                        db.AccessTokens.Add(new AccessTokens()
                        {
                            Token = token.Token,
                            EffectiveTime = new DateTime(token.EffectiveTime),
                            ExpiresIn = token.ExpiresTime,
                            Ip = token.IP,
                            UserAgent = token.UserAgent,
                            UserName = token.Name
                        });
                        user.LastLoginDateUtc = DateTime.Now;
                        db.SaveChanges();
                        return Ok(
                            new
                            {
                                AccessToken = token,
                                Profile = new
                                {
                                    UserId = user.Id,
                                    Username = user.UserName,
                                    Email = user.Email,
                                }
                            });
                    }
                }
                return Ok("Login failed!");
            }
        }
        [Authorize]
        [HttpGet("validate-token")]
        public IActionResult ValidateToken()
        {
            var access_token = HttpContext.GetTokenAsync("access_token");
            using (MaiAnVatContext db = new MaiAnVatContext())
            {
                var userName = db.User.FirstOrDefault(x => x.Id.Equals(UserK))?.UserName;
                var accessToKen = db.AccessTokens.FirstOrDefault(x => x.Token.Equals(access_token.Result) && x.UserName.Equals(userName));
                if (accessToKen != null)
                {
                    var expriedTime =  accessToKen.EffectiveTime.AddSeconds(accessToKen.ExpiresIn);
                    if (expriedTime < DateTime.Now)
                    {
                        return BadRequest("Access token is expried");
                    }
                    return Ok();
                }
                else
                {
                    return BadRequest("Access token is expried");
                }
            }
        }
    }
}
