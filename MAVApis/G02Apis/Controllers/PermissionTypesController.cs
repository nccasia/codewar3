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
    builder.EntitySet<PermissionType>("PermissionTypes");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class PermissionTypesController : ODataController
    {
        private MaiAnVatEntities db = new MaiAnVatEntities();

        // GET: odata/PermissionTypes
        [EnableQuery]
        public IQueryable<PermissionType> GetPermissionTypes()
        {
            return db.PermissionTypes;
        }

        // GET: odata/PermissionTypes(5)
        [EnableQuery]
        public SingleResult<PermissionType> GetPermissionType([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.PermissionTypes.Where(permissionType => permissionType.PermissionTypeK == key));
        }

        // PUT: odata/PermissionTypes(5)
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Delta<PermissionType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PermissionType permissionType = await db.PermissionTypes.FindAsync(key);
            if (permissionType == null)
            {
                return NotFound();
            }

            patch.Put(permissionType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermissionTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(permissionType);
        }

        // POST: odata/PermissionTypes
        public async Task<IHttpActionResult> Post(PermissionType permissionType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PermissionTypes.Add(permissionType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PermissionTypeExists(permissionType.PermissionTypeK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(permissionType);
        }

        // PATCH: odata/PermissionTypes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<PermissionType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PermissionType permissionType = await db.PermissionTypes.FindAsync(key);
            if (permissionType == null)
            {
                return NotFound();
            }

            patch.Patch(permissionType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermissionTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(permissionType);
        }

        // DELETE: odata/PermissionTypes(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            PermissionType permissionType = await db.PermissionTypes.FindAsync(key);
            if (permissionType == null)
            {
                return NotFound();
            }

            db.PermissionTypes.Remove(permissionType);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PermissionTypeExists(Guid key)
        {
            return db.PermissionTypes.Count(e => e.PermissionTypeK == key) > 0;
        }
    }
}
