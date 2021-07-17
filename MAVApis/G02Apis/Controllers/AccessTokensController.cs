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
    builder.EntitySet<AccessToken>("AccessTokens");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AccessTokensController : ODataController
    {
        private MaiAnVatEntities db = new MaiAnVatEntities();

        // GET: odata/AccessTokens
        [EnableQuery]
        public IQueryable<AccessToken> GetAccessTokens()
        {
            return db.AccessTokens;
        }

        // GET: odata/AccessTokens(5)
        [EnableQuery]
        public SingleResult<AccessToken> GetAccessToken([FromODataUri] int key)
        {
            return SingleResult.Create(db.AccessTokens.Where(accessToken => accessToken.AccessTokenId == key));
        }

        // PUT: odata/AccessTokens(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<AccessToken> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AccessToken accessToken = await db.AccessTokens.FindAsync(key);
            if (accessToken == null)
            {
                return NotFound();
            }

            patch.Put(accessToken);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccessTokenExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(accessToken);
        }

        // POST: odata/AccessTokens
        public async Task<IHttpActionResult> Post(AccessToken accessToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AccessTokens.Add(accessToken);
            await db.SaveChangesAsync();

            return Created(accessToken);
        }

        // PATCH: odata/AccessTokens(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<AccessToken> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AccessToken accessToken = await db.AccessTokens.FindAsync(key);
            if (accessToken == null)
            {
                return NotFound();
            }

            patch.Patch(accessToken);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccessTokenExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(accessToken);
        }

        // DELETE: odata/AccessTokens(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            AccessToken accessToken = await db.AccessTokens.FindAsync(key);
            if (accessToken == null)
            {
                return NotFound();
            }

            db.AccessTokens.Remove(accessToken);
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

        private bool AccessTokenExists(int key)
        {
            return db.AccessTokens.Count(e => e.AccessTokenId == key) > 0;
        }
    }
}
