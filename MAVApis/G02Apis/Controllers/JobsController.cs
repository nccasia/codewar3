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
    builder.EntitySet<Job>("Jobs");
    builder.EntitySet<JobAssignedUser>("JobAssignedUsers"); 
    builder.EntitySet<JobWorkFlowMove>("JobWorkFlowMoves"); 
    builder.EntitySet<ListCategory>("ListCategories"); 
    builder.EntitySet<JobType>("JobTypes"); 
    builder.EntitySet<WorkFlowStatu>("WorkFlowStatus"); 
    builder.EntitySet<JobMessage>("JobMessages"); 
    builder.EntitySet<RegistrationJob>("RegistrationJobs"); 
    builder.EntitySet<Schedule>("Schedules"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class JobsController : ODataController
    {
        private MaiAnVatEntities db = new MaiAnVatEntities();

        // GET: odata/Jobs
        [EnableQuery]
        public IQueryable<Job> GetJobs()
        {
            return db.Jobs;
        }

        // GET: odata/Jobs(5)
        [EnableQuery]
        public SingleResult<Job> GetJob([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.Jobs.Where(job => job.JobK == key));
        }

        // PUT: odata/Jobs(5)
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Delta<Job> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Job job = await db.Jobs.FindAsync(key);
            if (job == null)
            {
                return NotFound();
            }

            patch.Put(job);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(job);
        }

        // POST: odata/Jobs
        public async Task<IHttpActionResult> Post(Job job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Jobs.Add(job);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (JobExists(job.JobK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(job);
        }

        // PATCH: odata/Jobs(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<Job> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Job job = await db.Jobs.FindAsync(key);
            if (job == null)
            {
                return NotFound();
            }

            patch.Patch(job);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(job);
        }

        // DELETE: odata/Jobs(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            Job job = await db.Jobs.FindAsync(key);
            if (job == null)
            {
                return NotFound();
            }

            db.Jobs.Remove(job);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Jobs(5)/JobAssignedUsers
        [EnableQuery]
        public IQueryable<JobAssignedUser> GetJobAssignedUsers([FromODataUri] Guid key)
        {
            return db.Jobs.Where(m => m.JobK == key).SelectMany(m => m.JobAssignedUsers);
        }

        // GET: odata/Jobs(5)/JobWorkFlowMoves
        [EnableQuery]
        public IQueryable<JobWorkFlowMove> GetJobWorkFlowMoves([FromODataUri] Guid key)
        {
            return db.Jobs.Where(m => m.JobK == key).SelectMany(m => m.JobWorkFlowMoves);
        }

        // GET: odata/Jobs(5)/ListCategory
        [EnableQuery]
        public SingleResult<ListCategory> GetListCategory([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.Jobs.Where(m => m.JobK == key).Select(m => m.ListCategory));
        }

        // GET: odata/Jobs(5)/JobType
        [EnableQuery]
        public SingleResult<JobType> GetJobType([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.Jobs.Where(m => m.JobK == key).Select(m => m.JobType));
        }

        // GET: odata/Jobs(5)/WorkFlowStatu
        [EnableQuery]
        public SingleResult<WorkFlowStatu> GetWorkFlowStatu([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.Jobs.Where(m => m.JobK == key).Select(m => m.WorkFlowStatu));
        }

        // GET: odata/Jobs(5)/JobMessages
        [EnableQuery]
        public IQueryable<JobMessage> GetJobMessages([FromODataUri] Guid key)
        {
            return db.Jobs.Where(m => m.JobK == key).SelectMany(m => m.JobMessages);
        }

        // GET: odata/Jobs(5)/RegistrationJobs
        [EnableQuery]
        public IQueryable<RegistrationJob> GetRegistrationJobs([FromODataUri] Guid key)
        {
            return db.Jobs.Where(m => m.JobK == key).SelectMany(m => m.RegistrationJobs);
        }

        // GET: odata/Jobs(5)/Schedules
        [EnableQuery]
        public IQueryable<Schedule> GetSchedules([FromODataUri] Guid key)
        {
            return db.Jobs.Where(m => m.JobK == key).SelectMany(m => m.Schedules);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobExists(Guid key)
        {
            return db.Jobs.Count(e => e.JobK == key) > 0;
        }
    }
}
