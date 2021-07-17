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
    builder.EntitySet<JobAssignedUser>("JobAssignedUsers");
    builder.EntitySet<Job>("Jobs"); 
    builder.EntitySet<JobAssignmentList>("JobAssignmentLists"); 
    builder.EntitySet<User>("Users"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class JobAssignedUsersController : ODataController
    {
        private MaiAnVatEntities db = new MaiAnVatEntities();

        // GET: odata/JobAssignedUsers
        [EnableQuery]
        public IQueryable<JobAssignedUser> GetJobAssignedUsers()
        {
            return db.JobAssignedUsers;
        }

        // GET: odata/JobAssignedUsers(5)
        [EnableQuery]
        public SingleResult<JobAssignedUser> GetJobAssignedUser([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.JobAssignedUsers.Where(jobAssignedUser => jobAssignedUser.JobAssignedUserK == key));
        }

        // PUT: odata/JobAssignedUsers(5)
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Delta<JobAssignedUser> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            JobAssignedUser jobAssignedUser = await db.JobAssignedUsers.FindAsync(key);
            if (jobAssignedUser == null)
            {
                return NotFound();
            }

            patch.Put(jobAssignedUser);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobAssignedUserExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(jobAssignedUser);
        }

        // POST: odata/JobAssignedUsers
        public async Task<IHttpActionResult> Post(JobAssignedUser jobAssignedUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JobAssignedUsers.Add(jobAssignedUser);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (JobAssignedUserExists(jobAssignedUser.JobAssignedUserK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(jobAssignedUser);
        }

        // PATCH: odata/JobAssignedUsers(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<JobAssignedUser> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            JobAssignedUser jobAssignedUser = await db.JobAssignedUsers.FindAsync(key);
            if (jobAssignedUser == null)
            {
                return NotFound();
            }

            patch.Patch(jobAssignedUser);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobAssignedUserExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(jobAssignedUser);
        }

        // DELETE: odata/JobAssignedUsers(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            JobAssignedUser jobAssignedUser = await db.JobAssignedUsers.FindAsync(key);
            if (jobAssignedUser == null)
            {
                return NotFound();
            }

            db.JobAssignedUsers.Remove(jobAssignedUser);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/JobAssignedUsers(5)/Job
        [EnableQuery]
        public SingleResult<Job> GetJob([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.JobAssignedUsers.Where(m => m.JobAssignedUserK == key).Select(m => m.Job));
        }

        // GET: odata/JobAssignedUsers(5)/JobAssignmentList
        [EnableQuery]
        public SingleResult<JobAssignmentList> GetJobAssignmentList([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.JobAssignedUsers.Where(m => m.JobAssignedUserK == key).Select(m => m.JobAssignmentList));
        }

        // GET: odata/JobAssignedUsers(5)/User
        [EnableQuery]
        public SingleResult<User> GetUser([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.JobAssignedUsers.Where(m => m.JobAssignedUserK == key).Select(m => m.User));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobAssignedUserExists(Guid key)
        {
            return db.JobAssignedUsers.Count(e => e.JobAssignedUserK == key) > 0;
        }
    }
}
