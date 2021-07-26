using MaiAnVat.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
namespace MaiAnVat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator, Admin")]
    public class ValuesController : BaseApiController
    {
        [HttpGet("getactiondemo")]
        public ActionResult<string> GetActionDemo()
        {
            var role = Roles;
            return Ok(UserK);
        }
    }
}
