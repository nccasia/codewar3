using MaiAnVat.Models;
using MaiAnVat.ServiceFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MaiAnVat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : BaseApiController
    {
        private readonly IGroupService groupService;
        private readonly IPermissionService permissionService;
        public GroupController(IGroupService groupService, IPermissionService permissionService)
        {
            this.groupService = groupService;
            this.permissionService = permissionService;
        }

        // GET: api/group
        [HttpGet]
        public IActionResult GetGroup()
        {
            var groups = groupService.Find();
            return Ok(groups.Select(x => new
            {
                x.GroupK,
                x.Name,
                x.Description,
                x.Status,
                x.GroupPermission,
                x.CreatedAtUtc
            }).ToList());
        }

        // GET: api/permission
        [HttpGet("permissions")]
        public IActionResult GetPermissions()
        {
            var permissions = permissionService.Find();
            return Ok(permissions.Select(x => new
            {
                x.PermissionK,
                x.Description,
            }).ToList());
        }

        [HttpGet("groups")]
        public async Task<IActionResult> GetGroups([FromQuery] Pagination pagination, [FromQuery] string searchTerm = null)
        {
            var groups = groupService.Find(searchTerm);

            if (pagination == null)
                pagination = new Pagination();

            return Ok(await GetPaginatedResponse(groups.Select(x => new
            {
                x.GroupK,
                x.Name,
                x.Description,
                x.Status,
                x.GroupPermission,
                x.CreatedAtUtc
            }).OrderByDescending(x => x.CreatedAtUtc), pagination));

        }

        // GET: api/group/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroup([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var group = await groupService.ReadAsync(id);

            if (group == null)
            {
                return NotFound();
            }

            return Ok(group);
        }

        // PUT: api/group/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] Group group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != group.GroupK)
            {
                return BadRequest();
            }
            try
            {
                group.ModifiedByUserFk = UserK;
                await groupService.UpdateAsync(id, group);
                return Ok("Update Success");
            }
            catch (Exception)
            {
                return BadRequest("Update Failed");
            }
        }

        // POST: api/group
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Group model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            model.CreatedByUserFk = UserK;
            await groupService.CreateAsync(model);

            return CreatedAtAction("GetGroup", new { id = model.GroupK }, model);
        }


        // DELETE: api/group/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobType([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await groupService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Delete Failed");
            }
        }
    }
}
