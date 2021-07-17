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
    builder.EntitySet<User>("Users");
    builder.EntitySet<JobAssignedUser>("JobAssignedUsers"); 
    builder.EntitySet<JobMessage>("JobMessages"); 
    builder.EntitySet<Schedule>("Schedules"); 
    builder.EntitySet<ScheduleHistory>("ScheduleHistories"); 
    builder.EntitySet<UserGroup>("UserGroups"); 
    builder.EntitySet<UserNotification>("UserNotifications"); 
    builder.EntitySet<Claim>("Claims"); 
    builder.EntitySet<UserLogin>("UserLogins"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class UsersController : ODataController
    {
        private MaiAnVatEntities db = new MaiAnVatEntities();

        // GET: odata/Users
        [EnableQuery]
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: odata/Users(5)
        [EnableQuery]
        public SingleResult<User> GetUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.Users.Where(user => user.Id == key));
        }

        // PUT: odata/Users(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<User> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = await db.Users.FindAsync(key);
            if (user == null)
            {
                return NotFound();
            }

            patch.Put(user);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(user);
        }

        // POST: odata/Users
        public async Task<IHttpActionResult> Post(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            await db.SaveChangesAsync();

            return Created(user);
        }

        // PATCH: odata/Users(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<User> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = await db.Users.FindAsync(key);
            if (user == null)
            {
                return NotFound();
            }

            patch.Patch(user);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(user);
        }

        // DELETE: odata/Users(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            User user = await db.Users.FindAsync(key);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Users(5)/JobAssignedUsers
        [EnableQuery]
        public IQueryable<JobAssignedUser> GetJobAssignedUsers([FromODataUri] int key)
        {
            return db.Users.Where(m => m.Id == key).SelectMany(m => m.JobAssignedUsers);
        }

        // GET: odata/Users(5)/JobMessages
        [EnableQuery]
        public IQueryable<JobMessage> GetJobMessages([FromODataUri] int key)
        {
            return db.Users.Where(m => m.Id == key).SelectMany(m => m.JobMessages);
        }

        // GET: odata/Users(5)/Schedules
        [EnableQuery]
        public IQueryable<Schedule> GetSchedules([FromODataUri] int key)
        {
            return db.Users.Where(m => m.Id == key).SelectMany(m => m.Schedules);
        }

        // GET: odata/Users(5)/ScheduleHistories
        [EnableQuery]
        public IQueryable<ScheduleHistory> GetScheduleHistories([FromODataUri] int key)
        {
            return db.Users.Where(m => m.Id == key).SelectMany(m => m.ScheduleHistories);
        }

        // GET: odata/Users(5)/UserGroups
        [EnableQuery]
        public IQueryable<UserGroup> GetUserGroups([FromODataUri] int key)
        {
            return db.Users.Where(m => m.Id == key).SelectMany(m => m.UserGroups);
        }

        // GET: odata/Users(5)/UserNotifications
        [EnableQuery]
        public IQueryable<UserNotification> GetUserNotifications([FromODataUri] int key)
        {
            return db.Users.Where(m => m.Id == key).SelectMany(m => m.UserNotifications);
        }

        // GET: odata/Users(5)/Claims
        [EnableQuery]
        public IQueryable<Claim> GetClaims([FromODataUri] int key)
        {
            return db.Users.Where(m => m.Id == key).SelectMany(m => m.Claims);
        }

        // GET: odata/Users(5)/UserLogins
        [EnableQuery]
        public IQueryable<UserLogin> GetUserLogins([FromODataUri] int key)
        {
            return db.Users.Where(m => m.Id == key).SelectMany(m => m.UserLogins);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int key)
        {
            return db.Users.Count(e => e.Id == key) > 0;
        }
    }
}
