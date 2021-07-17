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
    builder.EntitySet<Schedule>("Schedules");
    builder.EntitySet<Job>("Jobs"); 
    builder.EntitySet<ListCategory>("ListCategories"); 
    builder.EntitySet<ScheduleHistory>("ScheduleHistories"); 
    builder.EntitySet<User>("Users"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class SchedulesController : ODataController
    {
        private MaiAnVatEntities db = new MaiAnVatEntities();

        // GET: odata/Schedules
        [EnableQuery]
        public IQueryable<Schedule> GetSchedules()
        {
            return db.Schedules;
        }

        // GET: odata/Schedules(5)
        [EnableQuery]
        public SingleResult<Schedule> GetSchedule([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.Schedules.Where(schedule => schedule.ScheduleK == key));
        }

        // PUT: odata/Schedules(5)
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Delta<Schedule> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Schedule schedule = await db.Schedules.FindAsync(key);
            if (schedule == null)
            {
                return NotFound();
            }

            patch.Put(schedule);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(schedule);
        }

        // POST: odata/Schedules
        public async Task<IHttpActionResult> Post(Schedule schedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Schedules.Add(schedule);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ScheduleExists(schedule.ScheduleK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(schedule);
        }

        // PATCH: odata/Schedules(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<Schedule> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Schedule schedule = await db.Schedules.FindAsync(key);
            if (schedule == null)
            {
                return NotFound();
            }

            patch.Patch(schedule);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(schedule);
        }

        // DELETE: odata/Schedules(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            Schedule schedule = await db.Schedules.FindAsync(key);
            if (schedule == null)
            {
                return NotFound();
            }

            db.Schedules.Remove(schedule);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Schedules(5)/Job
        [EnableQuery]
        public SingleResult<Job> GetJob([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.Schedules.Where(m => m.ScheduleK == key).Select(m => m.Job));
        }

        // GET: odata/Schedules(5)/ListCategory
        [EnableQuery]
        public SingleResult<ListCategory> GetListCategory([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.Schedules.Where(m => m.ScheduleK == key).Select(m => m.ListCategory));
        }

        // GET: odata/Schedules(5)/ScheduleHistories
        [EnableQuery]
        public IQueryable<ScheduleHistory> GetScheduleHistories([FromODataUri] Guid key)
        {
            return db.Schedules.Where(m => m.ScheduleK == key).SelectMany(m => m.ScheduleHistories);
        }

        // GET: odata/Schedules(5)/User
        [EnableQuery]
        public SingleResult<User> GetUser([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.Schedules.Where(m => m.ScheduleK == key).Select(m => m.User));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ScheduleExists(Guid key)
        {
            return db.Schedules.Count(e => e.ScheduleK == key) > 0;
        }
    }
}
