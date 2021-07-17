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
    builder.EntitySet<RegistrationJob>("RegistrationJobs");
    builder.EntitySet<Job>("Jobs"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class RegistrationJobsController : ODataController
    {
        private MaiAnVatEntities db = new MaiAnVatEntities();

        // GET: odata/RegistrationJobs
        [EnableQuery]
        public IQueryable<RegistrationJob> GetRegistrationJobs()
        {
            return db.RegistrationJobs;
        }

        // GET: odata/RegistrationJobs(5)
        [EnableQuery]
        public SingleResult<RegistrationJob> GetRegistrationJob([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.RegistrationJobs.Where(registrationJob => registrationJob.RegistrationJobK == key));
        }

        // PUT: odata/RegistrationJobs(5)
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Delta<RegistrationJob> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RegistrationJob registrationJob = await db.RegistrationJobs.FindAsync(key);
            if (registrationJob == null)
            {
                return NotFound();
            }

            patch.Put(registrationJob);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistrationJobExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(registrationJob);
        }

        // POST: odata/RegistrationJobs
        public async Task<IHttpActionResult> Post(RegistrationJob registrationJob)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RegistrationJobs.Add(registrationJob);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RegistrationJobExists(registrationJob.RegistrationJobK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(registrationJob);
        }

        // PATCH: odata/RegistrationJobs(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<RegistrationJob> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RegistrationJob registrationJob = await db.RegistrationJobs.FindAsync(key);
            if (registrationJob == null)
            {
                return NotFound();
            }

            patch.Patch(registrationJob);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistrationJobExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(registrationJob);
        }

        // DELETE: odata/RegistrationJobs(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            RegistrationJob registrationJob = await db.RegistrationJobs.FindAsync(key);
            if (registrationJob == null)
            {
                return NotFound();
            }

            db.RegistrationJobs.Remove(registrationJob);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/RegistrationJobs(5)/Job
        [EnableQuery]
        public SingleResult<Job> GetJob([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.RegistrationJobs.Where(m => m.RegistrationJobK == key).Select(m => m.Job));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RegistrationJobExists(Guid key)
        {
            return db.RegistrationJobs.Count(e => e.RegistrationJobK == key) > 0;
        }
    }
}
