using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using G02Apis.Models;

namespace G02Apis.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using G02Apis.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<UserNotification>("UserNotifications");
    builder.EntitySet<Notification>("Notifications"); 
    builder.EntitySet<User>("Users"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class UserNotificationsController : ODataController
    {
        private MaiAnVatEntities db = new MaiAnVatEntities();

        // GET: odata/UserNotifications
        [EnableQuery]
        public IQueryable<UserNotification> GetUserNotifications()
        {
            return db.UserNotifications;
        }

        // GET: odata/UserNotifications(5)
        [EnableQuery]
        public SingleResult<UserNotification> GetUserNotification([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.UserNotifications.Where(userNotification => userNotification.UserNotificationK == key));
        }

        // PUT: odata/UserNotifications(5)
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Delta<UserNotification> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserNotification userNotification = await db.UserNotifications.FindAsync(key);
            if (userNotification == null)
            {
                return NotFound();
            }

            patch.Put(userNotification);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserNotificationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(userNotification);
        }

        // POST: odata/UserNotifications
        public async Task<IHttpActionResult> Post(UserNotification userNotification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserNotifications.Add(userNotification);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserNotificationExists(userNotification.UserNotificationK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(userNotification);
        }

        // PATCH: odata/UserNotifications(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<UserNotification> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserNotification userNotification = await db.UserNotifications.FindAsync(key);
            if (userNotification == null)
            {
                return NotFound();
            }

            patch.Patch(userNotification);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserNotificationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(userNotification);
        }

        // DELETE: odata/UserNotifications(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            UserNotification userNotification = await db.UserNotifications.FindAsync(key);
            if (userNotification == null)
            {
                return NotFound();
            }

            db.UserNotifications.Remove(userNotification);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/UserNotifications(5)/Notification
        [EnableQuery]
        public SingleResult<Notification> GetNotification([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.UserNotifications.Where(m => m.UserNotificationK == key).Select(m => m.Notification));
        }

        // GET: odata/UserNotifications(5)/User
        [EnableQuery]
        public SingleResult<User> GetUser([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.UserNotifications.Where(m => m.UserNotificationK == key).Select(m => m.User));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserNotificationExists(Guid key)
        {
            return db.UserNotifications.Count(e => e.UserNotificationK == key) > 0;
        }
    }
}
