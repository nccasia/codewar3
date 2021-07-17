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
    builder.EntitySet<JobAssignmentListStatu>("JobAssignmentListStatus");
    builder.EntitySet<JobAssignmentList>("JobAssignmentLists"); 
    builder.EntitySet<WorkFlowStatu>("WorkFlowStatus"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class JobAssignmentListStatusController : ODataController
    {
        private MaiAnVatEntities db = new MaiAnVatEntities();

        // GET: odata/JobAssignmentListStatus
        [EnableQuery]
        public IQueryable<JobAssignmentListStatu> GetJobAssignmentListStatus()
        {
            return db.JobAssignmentListStatus;
        }

        // GET: odata/JobAssignmentListStatus(5)
        [EnableQuery]
        public SingleResult<JobAssignmentListStatu> GetJobAssignmentListStatu([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.JobAssignmentListStatus.Where(jobAssignmentListStatu => jobAssignmentListStatu.JobAssignmentListStatusK == key));
        }

        // PUT: odata/JobAssignmentListStatus(5)
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Delta<JobAssignmentListStatu> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            JobAssignmentListStatu jobAssignmentListStatu = await db.JobAssignmentListStatus.FindAsync(key);
            if (jobAssignmentListStatu == null)
            {
                return NotFound();
            }

            patch.Put(jobAssignmentListStatu);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobAssignmentListStatuExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(jobAssignmentListStatu);
        }

        // POST: odata/JobAssignmentListStatus
        public async Task<IHttpActionResult> Post(JobAssignmentListStatu jobAssignmentListStatu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JobAssignmentListStatus.Add(jobAssignmentListStatu);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (JobAssignmentListStatuExists(jobAssignmentListStatu.JobAssignmentListStatusK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(jobAssignmentListStatu);
        }

        // PATCH: odata/JobAssignmentListStatus(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<JobAssignmentListStatu> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            JobAssignmentListStatu jobAssignmentListStatu = await db.JobAssignmentListStatus.FindAsync(key);
            if (jobAssignmentListStatu == null)
            {
                return NotFound();
            }

            patch.Patch(jobAssignmentListStatu);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobAssignmentListStatuExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(jobAssignmentListStatu);
        }

        // DELETE: odata/JobAssignmentListStatus(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            JobAssignmentListStatu jobAssignmentListStatu = await db.JobAssignmentListStatus.FindAsync(key);
            if (jobAssignmentListStatu == null)
            {
                return NotFound();
            }

            db.JobAssignmentListStatus.Remove(jobAssignmentListStatu);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/JobAssignmentListStatus(5)/JobAssignmentList
        [EnableQuery]
        public SingleResult<JobAssignmentList> GetJobAssignmentList([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.JobAssignmentListStatus.Where(m => m.JobAssignmentListStatusK == key).Select(m => m.JobAssignmentList));
        }

        // GET: odata/JobAssignmentListStatus(5)/WorkFlowStatu
        [EnableQuery]
        public SingleResult<WorkFlowStatu> GetWorkFlowStatu([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.JobAssignmentListStatus.Where(m => m.JobAssignmentListStatusK == key).Select(m => m.WorkFlowStatu));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobAssignmentListStatuExists(Guid key)
        {
            return db.JobAssignmentListStatus.Count(e => e.JobAssignmentListStatusK == key) > 0;
        }
    }
}
