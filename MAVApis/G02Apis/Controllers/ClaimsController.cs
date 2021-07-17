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
    builder.EntitySet<Claim>("Claims");
    builder.EntitySet<User>("Users"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ClaimsController : ODataController
    {
        private MaiAnVatEntities db = new MaiAnVatEntities();

        // GET: odata/Claims
        [EnableQuery]
        public IQueryable<Claim> GetClaims()
        {
            return db.Claims;
        }

        // GET: odata/Claims(5)
        [EnableQuery]
        public SingleResult<Claim> GetClaim([FromODataUri] int key)
        {
            return SingleResult.Create(db.Claims.Where(claim => claim.UserClaimK == key));
        }

        // PUT: odata/Claims(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Claim> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Claim claim = await db.Claims.FindAsync(key);
            if (claim == null)
            {
                return NotFound();
            }

            patch.Put(claim);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClaimExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(claim);
        }

        // POST: odata/Claims
        public async Task<IHttpActionResult> Post(Claim claim)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Claims.Add(claim);
            await db.SaveChangesAsync();

            return Created(claim);
        }

        // PATCH: odata/Claims(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Claim> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Claim claim = await db.Claims.FindAsync(key);
            if (claim == null)
            {
                return NotFound();
            }

            patch.Patch(claim);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClaimExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(claim);
        }

        // DELETE: odata/Claims(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Claim claim = await db.Claims.FindAsync(key);
            if (claim == null)
            {
                return NotFound();
            }

            db.Claims.Remove(claim);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Claims(5)/User
        [EnableQuery]
        public SingleResult<User> GetUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.Claims.Where(m => m.UserClaimK == key).Select(m => m.User));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClaimExists(int key)
        {
            return db.Claims.Count(e => e.UserClaimK == key) > 0;
        }
    }
}
