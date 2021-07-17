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
    builder.EntitySet<UserLoginHistory>("UserLoginHistories");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class UserLoginHistoriesController : ODataController
    {
        private MaiAnVatEntities db = new MaiAnVatEntities();

        // GET: odata/UserLoginHistories
        [EnableQuery]
        public IQueryable<UserLoginHistory> GetUserLoginHistories()
        {
            return db.UserLoginHistories;
        }

        // GET: odata/UserLoginHistories(5)
        [EnableQuery]
        public SingleResult<UserLoginHistory> GetUserLoginHistory([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.UserLoginHistories.Where(userLoginHistory => userLoginHistory.UserLoginHistoryK == key));
        }

        // PUT: odata/UserLoginHistories(5)
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Delta<UserLoginHistory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserLoginHistory userLoginHistory = await db.UserLoginHistories.FindAsync(key);
            if (userLoginHistory == null)
            {
                return NotFound();
            }

            patch.Put(userLoginHistory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserLoginHistoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(userLoginHistory);
        }

        // POST: odata/UserLoginHistories
        public async Task<IHttpActionResult> Post(UserLoginHistory userLoginHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserLoginHistories.Add(userLoginHistory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserLoginHistoryExists(userLoginHistory.UserLoginHistoryK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(userLoginHistory);
        }

        // PATCH: odata/UserLoginHistories(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<UserLoginHistory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserLoginHistory userLoginHistory = await db.UserLoginHistories.FindAsync(key);
            if (userLoginHistory == null)
            {
                return NotFound();
            }

            patch.Patch(userLoginHistory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserLoginHistoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(userLoginHistory);
        }

        // DELETE: odata/UserLoginHistories(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            UserLoginHistory userLoginHistory = await db.UserLoginHistories.FindAsync(key);
            if (userLoginHistory == null)
            {
                return NotFound();
            }

            db.UserLoginHistories.Remove(userLoginHistory);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserLoginHistoryExists(Guid key)
        {
            return db.UserLoginHistories.Count(e => e.UserLoginHistoryK == key) > 0;
        }
    }
}
