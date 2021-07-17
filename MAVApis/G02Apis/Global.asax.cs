using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace G02Apis
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            HttpConfiguration config = GlobalConfiguration.Configuration;

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
                = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Context.Request.HttpMethod == "OPTIONS")
            {
                Context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                Context.Response.AddHeader("Access-Control-Allow-Headers", "*");
                Context.Response.AddHeader("Access-Control-Allow-Methods", "*");
                // Context.Response.AddHeader("Access-Control-Allow-Credentials", "true");
                Context.Response.End();
            }
        }
    }
    
}