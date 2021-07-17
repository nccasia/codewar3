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
    builder.EntitySet<JobType>("JobTypes");
    builder.EntitySet<Job>("Jobs"); 
    builder.EntitySet<JobAssignmentList>("JobAssignmentLists"); 
    builder.EntitySet<JobTypeWorkFlow>("JobTypeWorkFlows"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class JobTypesController : ODataController
    {
        private MaiAnVatEntities db = new MaiAnVatEntities();

        // GET: odata/JobTypes
        [EnableQuery]
        public IQueryable<JobType> GetJobTypes()
        {
            return db.JobTypes;
        }

        // GET: odata/JobTypes(5)
        [EnableQuery]
        public SingleResult<JobType> GetJobType([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.JobTypes.Where(jobType => jobType.JobTypeK == key));
        }

        // PUT: odata/JobTypes(5)
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Delta<JobType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            JobType jobType = await db.JobTypes.FindAsync(key);
            if (jobType == null)
            {
                return NotFound();
            }

            patch.Put(jobType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(jobType);
        }

        // POST: odata/JobTypes
        public async Task<IHttpActionResult> Post(JobType jobType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JobTypes.Add(jobType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (JobTypeExists(jobType.JobTypeK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(jobType);
        }

        // PATCH: odata/JobTypes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<JobType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            JobType jobType = await db.JobTypes.FindAsync(key);
            if (jobType == null)
            {
                return NotFound();
            }

            patch.Patch(jobType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(jobType);
        }

        // DELETE: odata/JobTypes(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            JobType jobType = await db.JobTypes.FindAsync(key);
            if (jobType == null)
            {
                return NotFound();
            }

            db.JobTypes.Remove(jobType);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/JobTypes(5)/Jobs
        [EnableQuery]
        public IQueryable<Job> GetJobs([FromODataUri] Guid key)
        {
            return db.JobTypes.Where(m => m.JobTypeK == key).SelectMany(m => m.Jobs);
        }

        // GET: odata/JobTypes(5)/JobAssignmentLists
        [EnableQuery]
        public IQueryable<JobAssignmentList> GetJobAssignmentLists([FromODataUri] Guid key)
        {
            return db.JobTypes.Where(m => m.JobTypeK == key).SelectMany(m => m.JobAssignmentLists);
        }

        // GET: odata/JobTypes(5)/JobTypeWorkFlows
        [EnableQuery]
        public IQueryable<JobTypeWorkFlow> GetJobTypeWorkFlows([FromODataUri] Guid key)
        {
            return db.JobTypes.Where(m => m.JobTypeK == key).SelectMany(m => m.JobTypeWorkFlows);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobTypeExists(Guid key)
        {
            return db.JobTypes.Count(e => e.JobTypeK == key) > 0;
        }
    }
}
