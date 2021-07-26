using MaiAnVat.Models;
using MaiAnVat.Models.CustomModels;
using MaiAnVat.ServiceFramework;
using MaiVanVat.Security;
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
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UserDto user)
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
                if (!string.IsNullOrEmpty(user.Password))
                {
                    user.PasswordSalt = AuthenticationHelper.RamdomString(5);
                    user.PasswordHash = AuthenticationHelper.GetMd5Hash(user.PasswordSalt + user.Password);
                }
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
        public async Task<IActionResult> Post([FromBody] UserDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new User();
            user.PasswordSalt = AuthenticationHelper.RamdomString(5);
            user.PasswordHash = AuthenticationHelper.GetMd5Hash(user.PasswordSalt + model.Password);
            user.CreatedByUserFk = UserK;
            user.DateOfBirth = model.DateOfBirth;
            user.Email = model.Email;
            user.EmailConfirmed = true;
            user.UserName = model.UserName;
            user.Status = model.Status;
            user.PhoneNumber = model.PhoneNumber;
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
