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
    builder.EntitySet<UserGroup>("UserGroups");
    builder.EntitySet<Group>("Groups"); 
    builder.EntitySet<User>("Users"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class UserGroupsController : ODataController
    {
        private MaiAnVatEntities db = new MaiAnVatEntities();

        // GET: odata/UserGroups
        [EnableQuery]
        public IQueryable<UserGroup> GetUserGroups()
        {
            return db.UserGroups;
        }

        // GET: odata/UserGroups(5)
        [EnableQuery]
        public SingleResult<UserGroup> GetUserGroup([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.UserGroups.Where(userGroup => userGroup.UserGroupK == key));
        }

        // PUT: odata/UserGroups(5)
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Delta<UserGroup> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserGroup userGroup = await db.UserGroups.FindAsync(key);
            if (userGroup == null)
            {
                return NotFound();
            }

            patch.Put(userGroup);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserGroupExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(userGroup);
        }

        // POST: odata/UserGroups
        public async Task<IHttpActionResult> Post(UserGroup userGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserGroups.Add(userGroup);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserGroupExists(userGroup.UserGroupK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(userGroup);
        }

        // PATCH: odata/UserGroups(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<UserGroup> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserGroup userGroup = await db.UserGroups.FindAsync(key);
            if (userGroup == null)
            {
                return NotFound();
            }

            patch.Patch(userGroup);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserGroupExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(userGroup);
        }

        // DELETE: odata/UserGroups(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            UserGroup userGroup = await db.UserGroups.FindAsync(key);
            if (userGroup == null)
            {
                return NotFound();
            }

            db.UserGroups.Remove(userGroup);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/UserGroups(5)/Group
        [EnableQuery]
        public SingleResult<Group> GetGroup([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.UserGroups.Where(m => m.UserGroupK == key).Select(m => m.Group));
        }

        // GET: odata/UserGroups(5)/User
        [EnableQuery]
        public SingleResult<User> GetUser([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.UserGroups.Where(m => m.UserGroupK == key).Select(m => m.User));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserGroupExists(Guid key)
        {
            return db.UserGroups.Count(e => e.UserGroupK == key) > 0;
        }
    }
}
