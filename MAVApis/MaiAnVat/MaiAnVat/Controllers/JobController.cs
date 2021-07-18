using MaiAnVat.Models;
using MaiAnVat.Models.CustomModels;
using MaiAnVat.ServiceFramework;
using MaiAnVat.ServiceFramework.JobAndJobType;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MaiAnVat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : BaseApiController
    {
        private readonly IJobService jobService;
        private readonly IJobTypeService jobTypeService;
        private readonly IListCategoryService listCategoryService;
        private readonly IWorkFlowStatusService workFlowStatusService;
        public JobController(IJobTypeService jobTypeService, IJobService jobService, IListCategoryService listCategoryService, IWorkFlowStatusService workFlowStatusService)
        {
            this.jobTypeService = jobTypeService;
            this.jobService = jobService;
            this.listCategoryService = listCategoryService;
            this.workFlowStatusService = workFlowStatusService;
        }

        // GET: api/job
        [HttpGet]
        public IActionResult GetJob()
        {
            var jobs = GetAllJobs(null);
            return Ok(jobs.OrderByDescending(x => x.CreatedAtUtc).ToList());
        }

        [HttpGet("jobs")]
        public async Task<IActionResult> GetJobs([FromQuery] Pagination pagination, [FromQuery] string searchTerm = null)
        {
            var jobs = GetAllJobs(searchTerm);
            if (pagination == null)
                pagination = new Pagination();

            return Ok(await GetPaginatedResponse(jobs.OrderByDescending(x => x.CreatedAtUtc), pagination));

        }

        // GET: api/job/272169AE-0F52-4544-A6EB-4607DE1D3796
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJob([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var job = await jobService.ReadAsync(id);

            if (job == null)
            {
                return NotFound();
            }

            return Ok(job);
        }

        // PUT: api/job/272169AE-0F52-4544-A6EB-4607DE1D3796
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJob([FromRoute] Guid id, [FromBody] Job job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != job.JobK)
            {
                return BadRequest();
            }
            try
            {
                await jobService.UpdateAsync(id, job);
                return Ok("Update Success");
            }
            catch (Exception)
            {
                return BadRequest("Update Failed");
            }
        }

        // POST: api/job
        [HttpPost]
        public async Task<IActionResult> PostJob([FromBody] Job job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await jobService.CreateAsync(job);

            return CreatedAtAction("GetJob", new { id = job.JobK }, job);
        }


        // DELETE: api/job/272169AE-0F52-4544-A6EB-4607DE1D3796
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await jobService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Delete Failed");
            }
        }


        #region API HELPER
        private IQueryable<JobDto> GetAllJobs(string searchTerm)
        {
            var qJobtypes = jobTypeService.Find();
            var qJobs = jobService.Find(!string.IsNullOrEmpty(searchTerm) ? searchTerm : null);
            var qListCategories = listCategoryService.Find();
            var qWorkFlowStatues = workFlowStatusService.Find();
            var jobs = from j in qJobs
                       join jt in qJobtypes on j.JobTypeFk equals jt.JobTypeK
                       join ls in qListCategories on j.JobStatusFk equals ls.ListCategoryK
                       join wf in qWorkFlowStatues on j.WorkflowStatusFk equals wf.WorkFlowStatusK
                       select new JobDto()
                       {
                           JobTypeFk = j.JobTypeFk,
                           Name = j.Name,
                           Title = j.Title,
                           Description = j.Description,
                           JobTypeName = jt.Name,
                           JobTypeColor = jt.ColorCode,
                           Satus = ls.Name,
                           WorkFlowStatus = wf.Name,
                           CreatedAtUtc = j.CreatedAtUtc,
                           IsActivated = j.IsActivated,
                           CustomerOrder = j.CustomerOrder,
                           RegistrationDeadline = j.RegistrationDeadline
                       };
            return jobs;
        }
        #endregion
    }
}
