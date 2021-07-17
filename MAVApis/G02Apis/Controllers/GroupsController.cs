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
    builder.EntitySet<Group>("Groups");
    builder.EntitySet<GroupPermission>("GroupPermissions"); 
    builder.EntitySet<JobAssignmentList>("JobAssignmentLists"); 
    builder.EntitySet<UserGroup>("UserGroups"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class GroupsController : ODataController
    {
        private MaiAnVatEntities db = new MaiAnVatEntities();

        // GET: odata/Groups
        [EnableQuery]
        public IQueryable<Group> GetGroups()
        {
            return db.Groups;
        }

        // GET: odata/Groups(5)
        [EnableQuery]
        public SingleResult<Group> GetGroup([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.Groups.Where(group => group.GroupK == key));
        }

        // PUT: odata/Groups(5)
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Delta<Group> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Group group = await db.Groups.FindAsync(key);
            if (group == null)
            {
                return NotFound();
            }

            patch.Put(group);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(group);
        }

        // POST: odata/Groups
        public async Task<IHttpActionResult> Post(Group group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Groups.Add(group);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GroupExists(group.GroupK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(group);
        }

        // PATCH: odata/Groups(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<Group> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Group group = await db.Groups.FindAsync(key);
            if (group == null)
            {
                return NotFound();
            }

            patch.Patch(group);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(group);
        }

        // DELETE: odata/Groups(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            Group group = await db.Groups.FindAsync(key);
            if (group == null)
            {
                return NotFound();
            }

            db.Groups.Remove(group);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Groups(5)/Group1
        [EnableQuery]
        public IQueryable<Group> GetGroup1([FromODataUri] Guid key)
        {
            return db.Groups.Where(m => m.GroupK == key).SelectMany(m => m.Group1);
        }

        // GET: odata/Groups(5)/Group2
        [EnableQuery]
        public SingleResult<Group> GetGroup2([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.Groups.Where(m => m.GroupK == key).Select(m => m.Group2));
        }

        // GET: odata/Groups(5)/GroupPermissions
        [EnableQuery]
        public IQueryable<GroupPermission> GetGroupPermissions([FromODataUri] Guid key)
        {
            return db.Groups.Where(m => m.GroupK == key).SelectMany(m => m.GroupPermissions);
        }

        // GET: odata/Groups(5)/JobAssignmentLists
        [EnableQuery]
        public IQueryable<JobAssignmentList> GetJobAssignmentLists([FromODataUri] Guid key)
        {
            return db.Groups.Where(m => m.GroupK == key).SelectMany(m => m.JobAssignmentLists);
        }

        // GET: odata/Groups(5)/UserGroups
        [EnableQuery]
        public IQueryable<UserGroup> GetUserGroups([FromODataUri] Guid key)
        {
            return db.Groups.Where(m => m.GroupK == key).SelectMany(m => m.UserGroups);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GroupExists(Guid key)
        {
            return db.Groups.Count(e => e.GroupK == key) > 0;
        }
    }
}
