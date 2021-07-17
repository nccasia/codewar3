using MaiAnVat.Models;
using MaiVanVat.Security;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

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
                        TokenIdentity token = tokenProvider.GenerateToken(user.Id, login.Username,
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
        //[AuthorizeUser]
        //[HttpGet]
        //[Route("validate-token")]
        //public IActionResult ValidateToken()
        //{
        //    TokenIdentity tokenIdentity = ClaimsPrincipal.Current.Identity as TokenIdentity;
        //    return Ok();
        //}
    }
}
