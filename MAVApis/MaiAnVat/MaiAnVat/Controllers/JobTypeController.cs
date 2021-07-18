using MaiAnVat.Models;
using MaiAnVat.Models.CustomModels;
using MaiAnVat.ServiceFramework.Job;
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
    public class JobTypeController : BaseApiController
    {
        private readonly IJobTypeService jobTypeService;
        public JobTypeController(IJobTypeService jobTypeService)
        {
            this.jobTypeService = jobTypeService;
        }

        // GET: api/jobtype
        [HttpGet]
        public IActionResult GetJobType()
        {
            var jobtypes = jobTypeService.Find();
            return Ok(jobtypes.Select(x => new
            {
                x.JobTypeK,
                x.Name,
                x.DefaultTimeInHours,
                x.Description,
                x.IsException,
                x.ColorCode
            }).ToList());
        }

        [HttpGet("jobtypes")]
        public async Task<IActionResult> Getjobtypes([FromQuery] Pagination pagination, [FromQuery] string searchTerm = null)
        {
            var jobtypes = jobTypeService.Find(searchTerm);

            if (pagination == null)
                pagination = new Pagination();

            return Ok(await GetPaginatedResponse(jobtypes.Select(x => new
            {
                x.JobTypeK,
                x.Name,
                x.DefaultTimeInHours,
                x.Description,
                x.IsException,
                x.ColorCode,
                x.CreatedAtUtc
            }).OrderByDescending(x => x.CreatedAtUtc), pagination));

        }

        // GET: api/jobtype/272169AE-0F52-4544-A6EB-4607DE1D3796
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobType([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jobtype = await jobTypeService.ReadAsync(id);

            if (jobtype == null)
            {
                return NotFound();
            }

            return Ok(jobtype);
        }

        // PUT: api/jobtype/272169AE-0F52-4544-A6EB-4607DE1D3796
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobType([FromRoute] Guid id, [FromBody] JobType jobType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jobType.JobTypeK)
            {
                return BadRequest();
            }
            try
            {
                await jobTypeService.UpdateAsync(id, jobType);
                return Ok("Update Success");
            }
            catch (Exception)
            {
                return BadRequest("Update Failed");
            }
        }

        // POST: api/jobtype
        [HttpPost]
        public async Task<IActionResult> PostJobType([FromBody] JobType jobType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await jobTypeService.CreateAsync(jobType);

            return CreatedAtAction("GetJobType", new { id = jobType.JobTypeK }, jobType);
        }


        // DELETE: api/jobtype/272169AE-0F52-4544-A6EB-4607DE1D3796
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobType([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await jobTypeService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Delete Failed");
            }
        }


        //[HttpPost]
        //public IActionResult Create([FromBody] JobType jobType)
        //{
        //    var jobtype = jobTypeService.Create(jobType);
        //    if (jobtype != null)
        //    {
        //        return Ok();
        //    }
        //    return BadRequest("Create failed");
        //}
        //[HttpPut]
        //public IActionResult Update([FromBody] JobType jobType)
        //{
        //    var jobtype = jobTypeService.Create(jobType);
        //    if (jobtype != null)
        //    {
        //        return Ok();
        //    }
        //    return BadRequest("Create failed");
        //}
    }
}
