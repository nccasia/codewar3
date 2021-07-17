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
    builder.EntitySet<MavMenu>("MavMenus");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class MavMenusController : ODataController
    {
        private MaiAnVatEntities db = new MaiAnVatEntities();

        // GET: odata/MavMenus
        [EnableQuery]
        public IQueryable<MavMenu> GetMavMenus()
        {
            return db.MavMenus;
        }

        // GET: odata/MavMenus(5)
        [EnableQuery]
        public SingleResult<MavMenu> GetMavMenu([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.MavMenus.Where(mavMenu => mavMenu.MavMenuK == key));
        }

        // PUT: odata/MavMenus(5)
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Delta<MavMenu> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MavMenu mavMenu = await db.MavMenus.FindAsync(key);
            if (mavMenu == null)
            {
                return NotFound();
            }

            patch.Put(mavMenu);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MavMenuExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mavMenu);
        }

        // POST: odata/MavMenus
        public async Task<IHttpActionResult> Post(MavMenu mavMenu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MavMenus.Add(mavMenu);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MavMenuExists(mavMenu.MavMenuK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(mavMenu);
        }

        // PATCH: odata/MavMenus(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<MavMenu> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MavMenu mavMenu = await db.MavMenus.FindAsync(key);
            if (mavMenu == null)
            {
                return NotFound();
            }

            patch.Patch(mavMenu);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MavMenuExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(mavMenu);
        }

        // DELETE: odata/MavMenus(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            MavMenu mavMenu = await db.MavMenus.FindAsync(key);
            if (mavMenu == null)
            {
                return NotFound();
            }

            db.MavMenus.Remove(mavMenu);
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

        private bool MavMenuExists(Guid key)
        {
            return db.MavMenus.Count(e => e.MavMenuK == key) > 0;
        }
    }
}
