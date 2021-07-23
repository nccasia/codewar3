using MaiAnVat.Models;
using MaiAnVat.ServiceFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaiAnVat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        // GET: api/user
        [HttpGet]
        public IActionResult GetUser()
        {
            var users = userService.Find();
            return Ok(users.Select(x => new
            {
                x.Id,
                x.UserName,
                x.FirstName,
                x.LastName,
                x.DateOfBirth,
                x.PhoneNumber,
                x.LockoutEnabled,
                x.Status,
                x.MobileNumber,
                x.Email,
                x.LastLoginDateUtc
            }).ToList());
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers([FromQuery] Pagination pagination, [FromQuery] string searchTerm = null)
        {
            var users = userService.Find(searchTerm);

            if (pagination == null)
                pagination = new Pagination();

            return Ok(await GetPaginatedResponse(users.Select(x => new
            {
                x.Id,
                x.UserName,
                x.FirstName,
                x.LastName,
                x.DateOfBirth,
                x.PhoneNumber,
                x.LockoutEnabled,
                x.Status,
                x.MobileNumber,
                x.Email,
                x.LastLoginDateUtc,
                x.CreatedAtUtc
            }).OrderByDescending(x => x.CreatedAtUtc), pagination));

        }

        // GET: api/user/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userService.ReadAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/user/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }
            try
            {
                user.ModifiedByUserFk = UserK;
                await userService.UpdateAsync(id, user);
                return Ok("Update Success");
            }
            catch (Exception)
            {
                return BadRequest("Update Failed");
            }
        }

        // POST: api/user
        [HttpPost]
        public async Task<IActionResult> PostJobType([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            user.CreatedByUserFk = UserK;
            await userService.CreateAsync(user);

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }


        // DELETE: api/user/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await userService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Delete Failed");
            }
        }

    }
}
