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
    builder.EntitySet<GroupPermission>("GroupPermissions");
    builder.EntitySet<Group>("Groups"); 
    builder.EntitySet<Permission>("Permissions"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class GroupPermissionsController : ODataController
    {
        private MaiAnVatEntities db = new MaiAnVatEntities();

        // GET: odata/GroupPermissions
        [EnableQuery]
        public IQueryable<GroupPermission> GetGroupPermissions()
        {
            return db.GroupPermissions;
        }

        // GET: odata/GroupPermissions(5)
        [EnableQuery]
        public SingleResult<GroupPermission> GetGroupPermission([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.GroupPermissions.Where(groupPermission => groupPermission.GroupPermissionK == key));
        }

        // PUT: odata/GroupPermissions(5)
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Delta<GroupPermission> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GroupPermission groupPermission = await db.GroupPermissions.FindAsync(key);
            if (groupPermission == null)
            {
                return NotFound();
            }

            patch.Put(groupPermission);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupPermissionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(groupPermission);
        }

        // POST: odata/GroupPermissions
        public async Task<IHttpActionResult> Post(GroupPermission groupPermission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GroupPermissions.Add(groupPermission);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GroupPermissionExists(groupPermission.GroupPermissionK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(groupPermission);
        }

        // PATCH: odata/GroupPermissions(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<GroupPermission> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GroupPermission groupPermission = await db.GroupPermissions.FindAsync(key);
            if (groupPermission == null)
            {
                return NotFound();
            }

            patch.Patch(groupPermission);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupPermissionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(groupPermission);
        }

        // DELETE: odata/GroupPermissions(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            GroupPermission groupPermission = await db.GroupPermissions.FindAsync(key);
            if (groupPermission == null)
            {
                return NotFound();
            }

            db.GroupPermissions.Remove(groupPermission);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/GroupPermissions(5)/Group
        [EnableQuery]
        public SingleResult<Group> GetGroup([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.GroupPermissions.Where(m => m.GroupPermissionK == key).Select(m => m.Group));
        }

        // GET: odata/GroupPermissions(5)/Permission
        [EnableQuery]
        public SingleResult<Permission> GetPermission([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.GroupPermissions.Where(m => m.GroupPermissionK == key).Select(m => m.Permission));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GroupPermissionExists(Guid key)
        {
            return db.GroupPermissions.Count(e => e.GroupPermissionK == key) > 0;
        }
    }
}
