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
    builder.EntitySet<Notification>("Notifications");
    builder.EntitySet<UserNotification>("UserNotifications"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class NotificationsController : ODataController
    {
        private MaiAnVatEntities db = new MaiAnVatEntities();

        // GET: odata/Notifications
        [EnableQuery]
        public IQueryable<Notification> GetNotifications()
        {
            return db.Notifications;
        }

        // GET: odata/Notifications(5)
        [EnableQuery]
        public SingleResult<Notification> GetNotification([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.Notifications.Where(notification => notification.NotificationK == key));
        }

        // PUT: odata/Notifications(5)
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Delta<Notification> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Notification notification = await db.Notifications.FindAsync(key);
            if (notification == null)
            {
                return NotFound();
            }

            patch.Put(notification);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(notification);
        }

        // POST: odata/Notifications
        public async Task<IHttpActionResult> Post(Notification notification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Notifications.Add(notification);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (NotificationExists(notification.NotificationK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(notification);
        }

        // PATCH: odata/Notifications(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<Notification> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Notification notification = await db.Notifications.FindAsync(key);
            if (notification == null)
            {
                return NotFound();
            }

            patch.Patch(notification);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(notification);
        }

        // DELETE: odata/Notifications(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            Notification notification = await db.Notifications.FindAsync(key);
            if (notification == null)
            {
                return NotFound();
            }

            db.Notifications.Remove(notification);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Notifications(5)/UserNotifications
        [EnableQuery]
        public IQueryable<UserNotification> GetUserNotifications([FromODataUri] Guid key)
        {
            return db.Notifications.Where(m => m.NotificationK == key).SelectMany(m => m.UserNotifications);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NotificationExists(Guid key)
        {
            return db.Notifications.Count(e => e.NotificationK == key) > 0;
        }
    }
}
