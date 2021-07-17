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
    builder.EntitySet<JobMessage>("JobMessages");
    builder.EntitySet<Job>("Jobs"); 
    builder.EntitySet<User>("Users"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class JobMessagesController : ODataController
    {
        private MaiAnVatEntities db = new MaiAnVatEntities();

        // GET: odata/JobMessages
        [EnableQuery]
        public IQueryable<JobMessage> GetJobMessages()
        {
            return db.JobMessages;
        }

        // GET: odata/JobMessages(5)
        [EnableQuery]
        public SingleResult<JobMessage> GetJobMessage([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.JobMessages.Where(jobMessage => jobMessage.MessageK == key));
        }

        // PUT: odata/JobMessages(5)
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Delta<JobMessage> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            JobMessage jobMessage = await db.JobMessages.FindAsync(key);
            if (jobMessage == null)
            {
                return NotFound();
            }

            patch.Put(jobMessage);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobMessageExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(jobMessage);
        }

        // POST: odata/JobMessages
        public async Task<IHttpActionResult> Post(JobMessage jobMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JobMessages.Add(jobMessage);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (JobMessageExists(jobMessage.MessageK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(jobMessage);
        }

        // PATCH: odata/JobMessages(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<JobMessage> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            JobMessage jobMessage = await db.JobMessages.FindAsync(key);
            if (jobMessage == null)
            {
                return NotFound();
            }

            patch.Patch(jobMessage);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobMessageExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(jobMessage);
        }

        // DELETE: odata/JobMessages(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            JobMessage jobMessage = await db.JobMessages.FindAsync(key);
            if (jobMessage == null)
            {
                return NotFound();
            }

            db.JobMessages.Remove(jobMessage);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/JobMessages(5)/Job
        [EnableQuery]
        public SingleResult<Job> GetJob([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.JobMessages.Where(m => m.MessageK == key).Select(m => m.Job));
        }

        // GET: odata/JobMessages(5)/JobMessage1
        [EnableQuery]
        public IQueryable<JobMessage> GetJobMessage1([FromODataUri] Guid key)
        {
            return db.JobMessages.Where(m => m.MessageK == key).SelectMany(m => m.JobMessage1);
        }

        // GET: odata/JobMessages(5)/JobMessage2
        [EnableQuery]
        public SingleResult<JobMessage> GetJobMessage2([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.JobMessages.Where(m => m.MessageK == key).Select(m => m.JobMessage2));
        }

        // GET: odata/JobMessages(5)/User
        [EnableQuery]
        public SingleResult<User> GetUser([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.JobMessages.Where(m => m.MessageK == key).Select(m => m.User));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobMessageExists(Guid key)
        {
            return db.JobMessages.Count(e => e.MessageK == key) > 0;
        }
    }
}
