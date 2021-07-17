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
    builder.EntitySet<UserLogin>("UserLogins");
    builder.EntitySet<User>("Users"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class UserLoginsController : ODataController
    {
        private MaiAnVatEntities db = new MaiAnVatEntities();

        // GET: odata/UserLogins
        [EnableQuery]
        public IQueryable<UserLogin> GetUserLogins()
        {
            return db.UserLogins;
        }

        // GET: odata/UserLogins(5)
        [EnableQuery]
        public SingleResult<UserLogin> GetUserLogin([FromODataUri] int key)
        {
            return SingleResult.Create(db.UserLogins.Where(userLogin => userLogin.UserLoginK == key));
        }

        // PUT: odata/UserLogins(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<UserLogin> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserLogin userLogin = await db.UserLogins.FindAsync(key);
            if (userLogin == null)
            {
                return NotFound();
            }

            patch.Put(userLogin);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserLoginExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(userLogin);
        }

        // POST: odata/UserLogins
        public async Task<IHttpActionResult> Post(UserLogin userLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserLogins.Add(userLogin);
            await db.SaveChangesAsync();

            return Created(userLogin);
        }

        // PATCH: odata/UserLogins(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<UserLogin> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserLogin userLogin = await db.UserLogins.FindAsync(key);
            if (userLogin == null)
            {
                return NotFound();
            }

            patch.Patch(userLogin);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserLoginExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(userLogin);
        }

        // DELETE: odata/UserLogins(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            UserLogin userLogin = await db.UserLogins.FindAsync(key);
            if (userLogin == null)
            {
                return NotFound();
            }

            db.UserLogins.Remove(userLogin);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/UserLogins(5)/User
        [EnableQuery]
        public SingleResult<User> GetUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.UserLogins.Where(m => m.UserLoginK == key).Select(m => m.User));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserLoginExists(int key)
        {
            return db.UserLogins.Count(e => e.UserLoginK == key) > 0;
        }
    }
}
