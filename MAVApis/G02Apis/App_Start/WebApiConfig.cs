using G02Apis.Models;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;

namespace G02Apis
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            // Web API routes
            config.MapHttpAttributeRoutes();
            var cors = new EnableCorsAttribute(
                "*",
                "*",
                "*"
            );
            config.EnableCors(cors);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.JsonFormatter.SupportedMediaTypes
            .Add(new MediaTypeHeaderValue("text/html"));

            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Category>("Categories");
            builder.EntitySet<CategoryClassification>("CategoryClassifications");
            builder.EntitySet<ListCategory>("ListCategories");
            builder.EntitySet<Group>("Groups");
            builder.EntitySet<RefreshToken>("RefreshTokens");
            builder.EntitySet<AccessToken>("AccessTokens");
            builder.EntitySet<Claim>("Claims");
            builder.EntitySet<User>("Users");
            builder.EntitySet<FileStorage>("FileStorages");
            builder.EntitySet<GroupPermission>("GroupPermissions");
            builder.EntitySet<Permission>("Permissions");
            builder.EntitySet<JobAssignmentList>("JobAssignmentLists");
            builder.EntitySet<UserGroup>("UserGroups");
            builder.EntitySet<JobAssignedUser>("JobAssignedUsers");
            builder.EntitySet<Job>("Jobs");
            builder.EntitySet<JobType>("JobTypes");
            builder.EntitySet<JobAssignmentListStatu>("JobAssignmentListStatus");
            builder.EntitySet<WorkFlowStatu>("WorkFlowStatus");
            builder.EntitySet<JobMessage>("JobMessages");
            builder.EntitySet<JobWorkFlowMove>("JobWorkFlowMoves");
            builder.EntitySet<RegistrationJob>("RegistrationJobs");
            builder.EntitySet<Schedule>("Schedules");
            builder.EntitySet<JobTypeWorkFlow>("JobTypeWorkFlows");
            builder.EntitySet<JobWorkFlowStatu>("JobWorkFlowStatus");
            builder.EntitySet<MavMenu>("MavMenus");
            builder.EntitySet<Notification>("Notifications");
            builder.EntitySet<UserNotification>("UserNotifications");
            builder.EntitySet<PermissionType>("PermissionTypes");
            builder.EntitySet<ScheduleHistory>("ScheduleHistories");
            builder.EntitySet<UserLoginHistory>("UserLoginHistories");
            builder.EntitySet<UserLogin>("UserLogins");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());


            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
                = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }
    }
}

