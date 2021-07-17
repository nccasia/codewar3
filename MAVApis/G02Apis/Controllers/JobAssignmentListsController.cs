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
    builder.EntitySet<JobAssignmentList>("JobAssignmentLists");
    builder.EntitySet<Group>("Groups"); 
    builder.EntitySet<JobAssignedUser>("JobAssignedUsers"); 
    builder.EntitySet<JobType>("JobTypes"); 
    builder.EntitySet<JobAssignmentListStatu>("JobAssignmentListStatus"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class JobAssignmentListsController : ODataController
    {
        private MaiAnVatEntities db = new MaiAnVatEntities();

        // GET: odata/JobAssignmentLists
        [EnableQuery]
        public IQueryable<JobAssignmentList> GetJobAssignmentLists()
        {
            return db.JobAssignmentLists;
        }

        // GET: odata/JobAssignmentLists(5)
        [EnableQuery]
        public SingleResult<JobAssignmentList> GetJobAssignmentList([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.JobAssignmentLists.Where(jobAssignmentList => jobAssignmentList.JobAssignmentListK == key));
        }

        // PUT: odata/JobAssignmentLists(5)
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Delta<JobAssignmentList> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            JobAssignmentList jobAssignmentList = await db.JobAssignmentLists.FindAsync(key);
            if (jobAssignmentList == null)
            {
                return NotFound();
            }

            patch.Put(jobAssignmentList);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobAssignmentListExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(jobAssignmentList);
        }

        // POST: odata/JobAssignmentLists
        public async Task<IHttpActionResult> Post(JobAssignmentList jobAssignmentList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JobAssignmentLists.Add(jobAssignmentList);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (JobAssignmentListExists(jobAssignmentList.JobAssignmentListK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(jobAssignmentList);
        }

        // PATCH: odata/JobAssignmentLists(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<JobAssignmentList> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            JobAssignmentList jobAssignmentList = await db.JobAssignmentLists.FindAsync(key);
            if (jobAssignmentList == null)
            {
                return NotFound();
            }

            patch.Patch(jobAssignmentList);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobAssignmentListExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(jobAssignmentList);
        }

        // DELETE: odata/JobAssignmentLists(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            JobAssignmentList jobAssignmentList = await db.JobAssignmentLists.FindAsync(key);
            if (jobAssignmentList == null)
            {
                return NotFound();
            }

            db.JobAssignmentLists.Remove(jobAssignmentList);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/JobAssignmentLists(5)/Group
        [EnableQuery]
        public SingleResult<Group> GetGroup([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.JobAssignmentLists.Where(m => m.JobAssignmentListK == key).Select(m => m.Group));
        }

        // GET: odata/JobAssignmentLists(5)/JobAssignedUsers
        [EnableQuery]
        public IQueryable<JobAssignedUser> GetJobAssignedUsers([FromODataUri] Guid key)
        {
            return db.JobAssignmentLists.Where(m => m.JobAssignmentListK == key).SelectMany(m => m.JobAssignedUsers);
        }

        // GET: odata/JobAssignmentLists(5)/JobType
        [EnableQuery]
        public SingleResult<JobType> GetJobType([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.JobAssignmentLists.Where(m => m.JobAssignmentListK == key).Select(m => m.JobType));
        }

        // GET: odata/JobAssignmentLists(5)/JobAssignmentListStatus
        [EnableQuery]
        public IQueryable<JobAssignmentListStatu> GetJobAssignmentListStatus([FromODataUri] Guid key)
        {
            return db.JobAssignmentLists.Where(m => m.JobAssignmentListK == key).SelectMany(m => m.JobAssignmentListStatus);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobAssignmentListExists(Guid key)
        {
            return db.JobAssignmentLists.Count(e => e.JobAssignmentListK == key) > 0;
        }
    }
}
