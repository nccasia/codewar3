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
    builder.EntitySet<JobWorkFlowStatu>("JobWorkFlowStatus");
    builder.EntitySet<JobTypeWorkFlow>("JobTypeWorkFlows"); 
    builder.EntitySet<WorkFlowStatu>("WorkFlowStatus"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class JobWorkFlowStatusController : ODataController
    {
        private MaiAnVatEntities db = new MaiAnVatEntities();

        // GET: odata/JobWorkFlowStatus
        [EnableQuery]
        public IQueryable<JobWorkFlowStatu> GetJobWorkFlowStatus()
        {
            return db.JobWorkFlowStatus;
        }

        // GET: odata/JobWorkFlowStatus(5)
        [EnableQuery]
        public SingleResult<JobWorkFlowStatu> GetJobWorkFlowStatu([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.JobWorkFlowStatus.Where(jobWorkFlowStatu => jobWorkFlowStatu.JobWorkFlowStatusK == key));
        }

        // PUT: odata/JobWorkFlowStatus(5)
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Delta<JobWorkFlowStatu> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            JobWorkFlowStatu jobWorkFlowStatu = await db.JobWorkFlowStatus.FindAsync(key);
            if (jobWorkFlowStatu == null)
            {
                return NotFound();
            }

            patch.Put(jobWorkFlowStatu);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobWorkFlowStatuExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(jobWorkFlowStatu);
        }

        // POST: odata/JobWorkFlowStatus
        public async Task<IHttpActionResult> Post(JobWorkFlowStatu jobWorkFlowStatu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JobWorkFlowStatus.Add(jobWorkFlowStatu);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (JobWorkFlowStatuExists(jobWorkFlowStatu.JobWorkFlowStatusK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(jobWorkFlowStatu);
        }

        // PATCH: odata/JobWorkFlowStatus(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<JobWorkFlowStatu> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            JobWorkFlowStatu jobWorkFlowStatu = await db.JobWorkFlowStatus.FindAsync(key);
            if (jobWorkFlowStatu == null)
            {
                return NotFound();
            }

            patch.Patch(jobWorkFlowStatu);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobWorkFlowStatuExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(jobWorkFlowStatu);
        }

        // DELETE: odata/JobWorkFlowStatus(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            JobWorkFlowStatu jobWorkFlowStatu = await db.JobWorkFlowStatus.FindAsync(key);
            if (jobWorkFlowStatu == null)
            {
                return NotFound();
            }

            db.JobWorkFlowStatus.Remove(jobWorkFlowStatu);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/JobWorkFlowStatus(5)/JobTypeWorkFlow
        [EnableQuery]
        public SingleResult<JobTypeWorkFlow> GetJobTypeWorkFlow([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.JobWorkFlowStatus.Where(m => m.JobWorkFlowStatusK == key).Select(m => m.JobTypeWorkFlow));
        }

        // GET: odata/JobWorkFlowStatus(5)/WorkFlowStatu
        [EnableQuery]
        public SingleResult<WorkFlowStatu> GetWorkFlowStatu([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.JobWorkFlowStatus.Where(m => m.JobWorkFlowStatusK == key).Select(m => m.WorkFlowStatu));
        }

        // GET: odata/JobWorkFlowStatus(5)/WorkFlowStatu1
        [EnableQuery]
        public SingleResult<WorkFlowStatu> GetWorkFlowStatu1([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.JobWorkFlowStatus.Where(m => m.JobWorkFlowStatusK == key).Select(m => m.WorkFlowStatu1));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobWorkFlowStatuExists(Guid key)
        {
            return db.JobWorkFlowStatus.Count(e => e.JobWorkFlowStatusK == key) > 0;
        }
    }
}
