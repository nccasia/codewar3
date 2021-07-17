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
    builder.EntitySet<Permission>("Permissions");
    builder.EntitySet<GroupPermission>("GroupPermissions"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class PermissionsController : ODataController
    {
        private MaiAnVatEntities db = new MaiAnVatEntities();

        // GET: odata/Permissions
        [EnableQuery]
        public IQueryable<Permission> GetPermissions()
        {
            return db.Permissions;
        }

        // GET: odata/Permissions(5)
        [EnableQuery]
        public SingleResult<Permission> GetPermission([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.Permissions.Where(permission => permission.PermissionK == key));
        }

        // PUT: odata/Permissions(5)
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Delta<Permission> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Permission permission = await db.Permissions.FindAsync(key);
            if (permission == null)
            {
                return NotFound();
            }

            patch.Put(permission);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermissionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(permission);
        }

        // POST: odata/Permissions
        public async Task<IHttpActionResult> Post(Permission permission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Permissions.Add(permission);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PermissionExists(permission.PermissionK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(permission);
        }

        // PATCH: odata/Permissions(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<Permission> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Permission permission = await db.Permissions.FindAsync(key);
            if (permission == null)
            {
                return NotFound();
            }

            patch.Patch(permission);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermissionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(permission);
        }

        // DELETE: odata/Permissions(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            Permission permission = await db.Permissions.FindAsync(key);
            if (permission == null)
            {
                return NotFound();
            }

            db.Permissions.Remove(permission);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Permissions(5)/GroupPermissions
        [EnableQuery]
        public IQueryable<GroupPermission> GetGroupPermissions([FromODataUri] Guid key)
        {
            return db.Permissions.Where(m => m.PermissionK == key).SelectMany(m => m.GroupPermissions);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PermissionExists(Guid key)
        {
            return db.Permissions.Count(e => e.PermissionK == key) > 0;
        }
    }
}
