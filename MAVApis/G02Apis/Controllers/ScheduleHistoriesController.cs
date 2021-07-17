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
    builder.EntitySet<ScheduleHistory>("ScheduleHistories");
    builder.EntitySet<Schedule>("Schedules"); 
    builder.EntitySet<User>("Users"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ScheduleHistoriesController : ODataController
    {
        private MaiAnVatEntities db = new MaiAnVatEntities();

        // GET: odata/ScheduleHistories
        [EnableQuery]
        public IQueryable<ScheduleHistory> GetScheduleHistories()
        {
            return db.ScheduleHistories;
        }

        // GET: odata/ScheduleHistories(5)
        [EnableQuery]
        public SingleResult<ScheduleHistory> GetScheduleHistory([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.ScheduleHistories.Where(scheduleHistory => scheduleHistory.ScheduleHistoryK == key));
        }

        // PUT: odata/ScheduleHistories(5)
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Delta<ScheduleHistory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ScheduleHistory scheduleHistory = await db.ScheduleHistories.FindAsync(key);
            if (scheduleHistory == null)
            {
                return NotFound();
            }

            patch.Put(scheduleHistory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleHistoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(scheduleHistory);
        }

        // POST: odata/ScheduleHistories
        public async Task<IHttpActionResult> Post(ScheduleHistory scheduleHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ScheduleHistories.Add(scheduleHistory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ScheduleHistoryExists(scheduleHistory.ScheduleHistoryK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(scheduleHistory);
        }

        // PATCH: odata/ScheduleHistories(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<ScheduleHistory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ScheduleHistory scheduleHistory = await db.ScheduleHistories.FindAsync(key);
            if (scheduleHistory == null)
            {
                return NotFound();
            }

            patch.Patch(scheduleHistory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleHistoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(scheduleHistory);
        }

        // DELETE: odata/ScheduleHistories(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            ScheduleHistory scheduleHistory = await db.ScheduleHistories.FindAsync(key);
            if (scheduleHistory == null)
            {
                return NotFound();
            }

            db.ScheduleHistories.Remove(scheduleHistory);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ScheduleHistories(5)/Schedule
        [EnableQuery]
        public SingleResult<Schedule> GetSchedule([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.ScheduleHistories.Where(m => m.ScheduleHistoryK == key).Select(m => m.Schedule));
        }

        // GET: odata/ScheduleHistories(5)/User
        [EnableQuery]
        public SingleResult<User> GetUser([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.ScheduleHistories.Where(m => m.ScheduleHistoryK == key).Select(m => m.User));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ScheduleHistoryExists(Guid key)
        {
            return db.ScheduleHistories.Count(e => e.ScheduleHistoryK == key) > 0;
        }
    }
}
