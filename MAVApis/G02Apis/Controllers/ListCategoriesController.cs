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
    builder.EntitySet<ListCategory>("ListCategories");
    builder.EntitySet<Category>("Categories"); 
    builder.EntitySet<Job>("Jobs"); 
    builder.EntitySet<Schedule>("Schedules"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ListCategoriesController : ODataController
    {
        private MaiAnVatEntities db = new MaiAnVatEntities();

        // GET: odata/ListCategories
        [EnableQuery]
        public IQueryable<ListCategory> GetListCategories()
        {
            return db.ListCategories;
        }

        // GET: odata/ListCategories(5)
        [EnableQuery]
        public SingleResult<ListCategory> GetListCategory([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.ListCategories.Where(listCategory => listCategory.ListCategoryK == key));
        }

        // PUT: odata/ListCategories(5)
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Delta<ListCategory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ListCategory listCategory = await db.ListCategories.FindAsync(key);
            if (listCategory == null)
            {
                return NotFound();
            }

            patch.Put(listCategory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListCategoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(listCategory);
        }

        // POST: odata/ListCategories
        public async Task<IHttpActionResult> Post(ListCategory listCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ListCategories.Add(listCategory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ListCategoryExists(listCategory.ListCategoryK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(listCategory);
        }

        // PATCH: odata/ListCategories(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<ListCategory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ListCategory listCategory = await db.ListCategories.FindAsync(key);
            if (listCategory == null)
            {
                return NotFound();
            }

            patch.Patch(listCategory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListCategoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(listCategory);
        }

        // DELETE: odata/ListCategories(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            ListCategory listCategory = await db.ListCategories.FindAsync(key);
            if (listCategory == null)
            {
                return NotFound();
            }

            db.ListCategories.Remove(listCategory);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ListCategories(5)/Category
        [EnableQuery]
        public SingleResult<Category> GetCategory([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.ListCategories.Where(m => m.ListCategoryK == key).Select(m => m.Category));
        }

        // GET: odata/ListCategories(5)/Jobs
        [EnableQuery]
        public IQueryable<Job> GetJobs([FromODataUri] Guid key)
        {
            return db.ListCategories.Where(m => m.ListCategoryK == key).SelectMany(m => m.Jobs);
        }

        // GET: odata/ListCategories(5)/Schedules
        [EnableQuery]
        public IQueryable<Schedule> GetSchedules([FromODataUri] Guid key)
        {
            return db.ListCategories.Where(m => m.ListCategoryK == key).SelectMany(m => m.Schedules);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ListCategoryExists(Guid key)
        {
            return db.ListCategories.Count(e => e.ListCategoryK == key) > 0;
        }
    }
}
