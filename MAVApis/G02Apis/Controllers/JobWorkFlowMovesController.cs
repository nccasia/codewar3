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
    builder.EntitySet<JobWorkFlowMove>("JobWorkFlowMoves");
    builder.EntitySet<Job>("Jobs"); 
    builder.EntitySet<WorkFlowStatu>("WorkFlowStatus"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class JobWorkFlowMovesController : ODataController
    {
        private MaiAnVatEntities db = new MaiAnVatEntities();

        // GET: odata/JobWorkFlowMoves
        [EnableQuery]
        public IQueryable<JobWorkFlowMove> GetJobWorkFlowMoves()
        {
            return db.JobWorkFlowMoves;
        }

        // GET: odata/JobWorkFlowMoves(5)
        [EnableQuery]
        public SingleResult<JobWorkFlowMove> GetJobWorkFlowMove([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.JobWorkFlowMoves.Where(jobWorkFlowMove => jobWorkFlowMove.JobWorkFlowMoveK == key));
        }

        // PUT: odata/JobWorkFlowMoves(5)
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Delta<JobWorkFlowMove> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            JobWorkFlowMove jobWorkFlowMove = await db.JobWorkFlowMoves.FindAsync(key);
            if (jobWorkFlowMove == null)
            {
                return NotFound();
            }

            patch.Put(jobWorkFlowMove);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobWorkFlowMoveExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(jobWorkFlowMove);
        }

        // POST: odata/JobWorkFlowMoves
        public async Task<IHttpActionResult> Post(JobWorkFlowMove jobWorkFlowMove)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JobWorkFlowMoves.Add(jobWorkFlowMove);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (JobWorkFlowMoveExists(jobWorkFlowMove.JobWorkFlowMoveK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(jobWorkFlowMove);
        }

        // PATCH: odata/JobWorkFlowMoves(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<JobWorkFlowMove> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            JobWorkFlowMove jobWorkFlowMove = await db.JobWorkFlowMoves.FindAsync(key);
            if (jobWorkFlowMove == null)
            {
                return NotFound();
            }

            patch.Patch(jobWorkFlowMove);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobWorkFlowMoveExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(jobWorkFlowMove);
        }

        // DELETE: odata/JobWorkFlowMoves(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            JobWorkFlowMove jobWorkFlowMove = await db.JobWorkFlowMoves.FindAsync(key);
            if (jobWorkFlowMove == null)
            {
                return NotFound();
            }

            db.JobWorkFlowMoves.Remove(jobWorkFlowMove);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/JobWorkFlowMoves(5)/Job
        [EnableQuery]
        public SingleResult<Job> GetJob([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.JobWorkFlowMoves.Where(m => m.JobWorkFlowMoveK == key).Select(m => m.Job));
        }

        // GET: odata/JobWorkFlowMoves(5)/WorkFlowStatu
        [EnableQuery]
        public SingleResult<WorkFlowStatu> GetWorkFlowStatu([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.JobWorkFlowMoves.Where(m => m.JobWorkFlowMoveK == key).Select(m => m.WorkFlowStatu));
        }

        // GET: odata/JobWorkFlowMoves(5)/WorkFlowStatu1
        [EnableQuery]
        public SingleResult<WorkFlowStatu> GetWorkFlowStatu1([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.JobWorkFlowMoves.Where(m => m.JobWorkFlowMoveK == key).Select(m => m.WorkFlowStatu1));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobWorkFlowMoveExists(Guid key)
        {
            return db.JobWorkFlowMoves.Count(e => e.JobWorkFlowMoveK == key) > 0;
        }
    }
}
