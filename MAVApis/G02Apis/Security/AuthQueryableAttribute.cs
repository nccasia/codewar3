using Hvit.Security;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;

namespace KinhBacP01APIs_NetStandard.Security
{
    public class AuthQueryableAttribute : EnableQueryAttribute
    {
        public override void ValidateQuery(System.Net.Http.HttpRequestMessage request, ODataQueryOptions queryOptions)
        {
            if (queryOptions.Filter != null)
            {
                queryOptions.Filter.Validator = new RestrictiveFilterQueryValidator();
            }

            base.ValidateQuery(request, queryOptions);
        }
        public override void OnActionExecuted(HttpActionExecutedContext context)
        {
            TokenProvider tokenProvider = new TokenProvider();
            TokenIdentity tokenIdentity = new TokenIdentity();
            tokenIdentity.UserAgent = context.Request.Headers.UserAgent.ToString();

            if (context.Request.Headers.Referrer != null)
                tokenIdentity.IP = context.Request.Headers.Referrer.Authority;

            if (context.Request.Headers.Contains("access_token"))
            {
                tokenIdentity.Token = context.Request.Headers.GetValues("access_token").FirstOrDefault();
            }
            if (!tokenProvider.ValidateToken(ref tokenIdentity))
            {
                context.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
            else
            {
                // context.User = new System.Security.Claims.ClaimsPrincipal(tokenIdentity);
            }
            base.OnActionExecuted(context);
        }
    }

}