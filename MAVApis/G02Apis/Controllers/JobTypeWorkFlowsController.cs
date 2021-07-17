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
    builder.EntitySet<JobTypeWorkFlow>("JobTypeWorkFlows");
    builder.EntitySet<JobType>("JobTypes"); 
    builder.EntitySet<JobWorkFlowStatu>("JobWorkFlowStatus"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class JobTypeWorkFlowsController : ODataController
    {
        private MaiAnVatEntities db = new MaiAnVatEntities();

        // GET: odata/JobTypeWorkFlows
        [EnableQuery]
        public IQueryable<JobTypeWorkFlow> GetJobTypeWorkFlows()
        {
            return db.JobTypeWorkFlows;
        }

        // GET: odata/JobTypeWorkFlows(5)
        [EnableQuery]
        public SingleResult<JobTypeWorkFlow> GetJobTypeWorkFlow([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.JobTypeWorkFlows.Where(jobTypeWorkFlow => jobTypeWorkFlow.JobTypeWorkFlowK == key));
        }

        // PUT: odata/JobTypeWorkFlows(5)
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Delta<JobTypeWorkFlow> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            JobTypeWorkFlow jobTypeWorkFlow = await db.JobTypeWorkFlows.FindAsync(key);
            if (jobTypeWorkFlow == null)
            {
                return NotFound();
            }

            patch.Put(jobTypeWorkFlow);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobTypeWorkFlowExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(jobTypeWorkFlow);
        }

        // POST: odata/JobTypeWorkFlows
        public async Task<IHttpActionResult> Post(JobTypeWorkFlow jobTypeWorkFlow)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JobTypeWorkFlows.Add(jobTypeWorkFlow);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (JobTypeWorkFlowExists(jobTypeWorkFlow.JobTypeWorkFlowK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(jobTypeWorkFlow);
        }

        // PATCH: odata/JobTypeWorkFlows(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<JobTypeWorkFlow> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            JobTypeWorkFlow jobTypeWorkFlow = await db.JobTypeWorkFlows.FindAsync(key);
            if (jobTypeWorkFlow == null)
            {
                return NotFound();
            }

            patch.Patch(jobTypeWorkFlow);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobTypeWorkFlowExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(jobTypeWorkFlow);
        }

        // DELETE: odata/JobTypeWorkFlows(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            JobTypeWorkFlow jobTypeWorkFlow = await db.JobTypeWorkFlows.FindAsync(key);
            if (jobTypeWorkFlow == null)
            {
                return NotFound();
            }

            db.JobTypeWorkFlows.Remove(jobTypeWorkFlow);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/JobTypeWorkFlows(5)/JobType
        [EnableQuery]
        public SingleResult<JobType> GetJobType([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.JobTypeWorkFlows.Where(m => m.JobTypeWorkFlowK == key).Select(m => m.JobType));
        }

        // GET: odata/JobTypeWorkFlows(5)/JobWorkFlowStatus
        [EnableQuery]
        public IQueryable<JobWorkFlowStatu> GetJobWorkFlowStatus([FromODataUri] Guid key)
        {
            return db.JobTypeWorkFlows.Where(m => m.JobTypeWorkFlowK == key).SelectMany(m => m.JobWorkFlowStatus);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobTypeWorkFlowExists(Guid key)
        {
            return db.JobTypeWorkFlows.Count(e => e.JobTypeWorkFlowK == key) > 0;
        }
    }
}
