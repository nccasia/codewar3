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
    builder.EntitySet<WorkFlowStatu>("WorkFlowStatus");
    builder.EntitySet<Job>("Jobs"); 
    builder.EntitySet<JobAssignmentListStatu>("JobAssignmentListStatus"); 
    builder.EntitySet<JobWorkFlowMove>("JobWorkFlowMoves"); 
    builder.EntitySet<JobWorkFlowStatu>("JobWorkFlowStatus"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class WorkFlowStatusController : ODataController
    {
        private MaiAnVatEntities db = new MaiAnVatEntities();

        // GET: odata/WorkFlowStatus
        [EnableQuery]
        public IQueryable<WorkFlowStatu> GetWorkFlowStatus()
        {
            return db.WorkFlowStatus;
        }

        // GET: odata/WorkFlowStatus(5)
        [EnableQuery]
        public SingleResult<WorkFlowStatu> GetWorkFlowStatu([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.WorkFlowStatus.Where(workFlowStatu => workFlowStatu.WorkFlowStatusK == key));
        }

        // PUT: odata/WorkFlowStatus(5)
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Delta<WorkFlowStatu> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            WorkFlowStatu workFlowStatu = await db.WorkFlowStatus.FindAsync(key);
            if (workFlowStatu == null)
            {
                return NotFound();
            }

            patch.Put(workFlowStatu);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkFlowStatuExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(workFlowStatu);
        }

        // POST: odata/WorkFlowStatus
        public async Task<IHttpActionResult> Post(WorkFlowStatu workFlowStatu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WorkFlowStatus.Add(workFlowStatu);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (WorkFlowStatuExists(workFlowStatu.WorkFlowStatusK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(workFlowStatu);
        }

        // PATCH: odata/WorkFlowStatus(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<WorkFlowStatu> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            WorkFlowStatu workFlowStatu = await db.WorkFlowStatus.FindAsync(key);
            if (workFlowStatu == null)
            {
                return NotFound();
            }

            patch.Patch(workFlowStatu);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkFlowStatuExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(workFlowStatu);
        }

        // DELETE: odata/WorkFlowStatus(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            WorkFlowStatu workFlowStatu = await db.WorkFlowStatus.FindAsync(key);
            if (workFlowStatu == null)
            {
                return NotFound();
            }

            db.WorkFlowStatus.Remove(workFlowStatu);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/WorkFlowStatus(5)/Jobs
        [EnableQuery]
        public IQueryable<Job> GetJobs([FromODataUri] Guid key)
        {
            return db.WorkFlowStatus.Where(m => m.WorkFlowStatusK == key).SelectMany(m => m.Jobs);
        }

        // GET: odata/WorkFlowStatus(5)/JobAssignmentListStatus
        [EnableQuery]
        public IQueryable<JobAssignmentListStatu> GetJobAssignmentListStatus([FromODataUri] Guid key)
        {
            return db.WorkFlowStatus.Where(m => m.WorkFlowStatusK == key).SelectMany(m => m.JobAssignmentListStatus);
        }

        // GET: odata/WorkFlowStatus(5)/JobWorkFlowMoves
        [EnableQuery]
        public IQueryable<JobWorkFlowMove> GetJobWorkFlowMoves([FromODataUri] Guid key)
        {
            return db.WorkFlowStatus.Where(m => m.WorkFlowStatusK == key).SelectMany(m => m.JobWorkFlowMoves);
        }

        // GET: odata/WorkFlowStatus(5)/JobWorkFlowMoves1
        [EnableQuery]
        public IQueryable<JobWorkFlowMove> GetJobWorkFlowMoves1([FromODataUri] Guid key)
        {
            return db.WorkFlowStatus.Where(m => m.WorkFlowStatusK == key).SelectMany(m => m.JobWorkFlowMoves1);
        }

        // GET: odata/WorkFlowStatus(5)/JobWorkFlowStatus
        [EnableQuery]
        public IQueryable<JobWorkFlowStatu> GetJobWorkFlowStatus([FromODataUri] Guid key)
        {
            return db.WorkFlowStatus.Where(m => m.WorkFlowStatusK == key).SelectMany(m => m.JobWorkFlowStatus);
        }

        // GET: odata/WorkFlowStatus(5)/JobWorkFlowStatus1
        [EnableQuery]
        public IQueryable<JobWorkFlowStatu> GetJobWorkFlowStatus1([FromODataUri] Guid key)
        {
            return db.WorkFlowStatus.Where(m => m.WorkFlowStatusK == key).SelectMany(m => m.JobWorkFlowStatus1);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorkFlowStatuExists(Guid key)
        {
            return db.WorkFlowStatus.Count(e => e.WorkFlowStatusK == key) > 0;
        }
    }
}
