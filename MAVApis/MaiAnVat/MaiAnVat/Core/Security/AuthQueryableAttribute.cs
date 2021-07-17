//using HVIT.Security;
//using System.Linq;
//using System.Web;
//using System.Web.Http;
//using System.Security.Claims;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;

//namespace MaiVanVat.Security
//{
//    public class AuthorizeUserAttribute : AuthorizeAttribute
//    {
//        // Custom property
//        public string AccessLevel { get; set; }

//        public AuthorizeUserAttribute()
//        {
//            AccessLevel = "";
//        }

//        public override void OnAuthorization(HttpActionContext context)
//        {
//            try
//            {
//                TokenProvider tokenProvider = new TokenProvider();
//                TokenIdentity tokenIdentity = new TokenIdentity();
//                tokenIdentity.UserAgent = context.Request.Headers.UserAgent.ToString();

//                if (context.Request.RequestUri.AbsoluteUri != null)
//                    tokenIdentity.UriApi = context.Request.RequestUri.AbsolutePath;
//                if (context.Request.Method.Method != null)
//                    tokenIdentity.Method = context.Request.Method.Method;
//                if (context.Request.Headers.Referrer.AbsolutePath != null)
//                    tokenIdentity.UriClient = context.Request.Headers.Referrer.AbsolutePath;
//                if (context.Request.Headers.Referrer != null)
//                    tokenIdentity.IP = context.Request.Headers.Referrer.Authority;
//                if (context.Request.Headers.Contains("access_token"))
//                    tokenIdentity.Token = context.Request.Headers.GetValues("access_token").FirstOrDefault();
//                if (!tokenProvider.ValidateToken(ref tokenIdentity))
//                    context.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
//                //else if (tokenIdentity.UriApi != "/api/auth/validate-token/" && !tokenProvider.ValidateAction(tokenIdentity))
//                //    context.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
//                else
//                {
//                    HttpContext.Current.User = new System.Security.Claims.ClaimsPrincipal(tokenIdentity);
//                }
//                base.OnAuthorization(context);
//            }
//            catch (System.Exception ex)
//            {
//                throw ex;
//            }
//        }

//        protected override bool IsAuthorized(HttpActionContext actionContext)
//        {
//            var isAuthorized = base.IsAuthorized(actionContext);
//            if (!isAuthorized)
//            {
//                return false;
//            }

//            string privilegeLevels = string.Join("", "abc");

//            return privilegeLevels.Contains(this.AccessLevel);
//        }
//    }
//}