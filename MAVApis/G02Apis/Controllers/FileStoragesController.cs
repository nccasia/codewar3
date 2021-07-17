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
    builder.EntitySet<FileStorage>("FileStorages");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class FileStoragesController : ODataController
    {
        private MaiAnVatEntities db = new MaiAnVatEntities();

        // GET: odata/FileStorages
        [EnableQuery]
        public IQueryable<FileStorage> GetFileStorages()
        {
            return db.FileStorages;
        }

        // GET: odata/FileStorages(5)
        [EnableQuery]
        public SingleResult<FileStorage> GetFileStorage([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.FileStorages.Where(fileStorage => fileStorage.FileStorageK == key));
        }

        // PUT: odata/FileStorages(5)
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Delta<FileStorage> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FileStorage fileStorage = await db.FileStorages.FindAsync(key);
            if (fileStorage == null)
            {
                return NotFound();
            }

            patch.Put(fileStorage);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FileStorageExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(fileStorage);
        }

        // POST: odata/FileStorages
        public async Task<IHttpActionResult> Post(FileStorage fileStorage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FileStorages.Add(fileStorage);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FileStorageExists(fileStorage.FileStorageK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(fileStorage);
        }

        // PATCH: odata/FileStorages(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<FileStorage> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FileStorage fileStorage = await db.FileStorages.FindAsync(key);
            if (fileStorage == null)
            {
                return NotFound();
            }

            patch.Patch(fileStorage);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FileStorageExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(fileStorage);
        }

        // DELETE: odata/FileStorages(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            FileStorage fileStorage = await db.FileStorages.FindAsync(key);
            if (fileStorage == null)
            {
                return NotFound();
            }

            db.FileStorages.Remove(fileStorage);
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

        private bool FileStorageExists(Guid key)
        {
            return db.FileStorages.Count(e => e.FileStorageK == key) > 0;
        }
    }
}
