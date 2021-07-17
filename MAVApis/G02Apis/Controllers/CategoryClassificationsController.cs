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
    builder.EntitySet<CategoryClassification>("CategoryClassifications");
    builder.EntitySet<Category>("Categories"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class CategoryClassificationsController : ODataController
    {
        private MaiAnVatEntities db = new MaiAnVatEntities();

        // GET: odata/CategoryClassifications
        [EnableQuery]
        public IQueryable<CategoryClassification> GetCategoryClassifications()
        {
            return db.CategoryClassifications;
        }

        // GET: odata/CategoryClassifications(5)
        [EnableQuery]
        public SingleResult<CategoryClassification> GetCategoryClassification([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.CategoryClassifications.Where(categoryClassification => categoryClassification.CategoryClassificationK == key));
        }

        // PUT: odata/CategoryClassifications(5)
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Delta<CategoryClassification> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CategoryClassification categoryClassification = await db.CategoryClassifications.FindAsync(key);
            if (categoryClassification == null)
            {
                return NotFound();
            }

            patch.Put(categoryClassification);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryClassificationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(categoryClassification);
        }

        // POST: odata/CategoryClassifications
        public async Task<IHttpActionResult> Post(CategoryClassification categoryClassification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CategoryClassifications.Add(categoryClassification);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CategoryClassificationExists(categoryClassification.CategoryClassificationK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(categoryClassification);
        }

        // PATCH: odata/CategoryClassifications(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<CategoryClassification> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CategoryClassification categoryClassification = await db.CategoryClassifications.FindAsync(key);
            if (categoryClassification == null)
            {
                return NotFound();
            }

            patch.Patch(categoryClassification);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryClassificationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(categoryClassification);
        }

        // DELETE: odata/CategoryClassifications(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            CategoryClassification categoryClassification = await db.CategoryClassifications.FindAsync(key);
            if (categoryClassification == null)
            {
                return NotFound();
            }

            db.CategoryClassifications.Remove(categoryClassification);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/CategoryClassifications(5)/Category
        [EnableQuery]
        public SingleResult<Category> GetCategory([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.CategoryClassifications.Where(m => m.CategoryClassificationK == key).Select(m => m.Category));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryClassificationExists(Guid key)
        {
            return db.CategoryClassifications.Count(e => e.CategoryClassificationK == key) > 0;
        }
    }
}
