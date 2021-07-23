using MaiAnVat.Models;
using MaiAnVat.Models.CustomModels;
using MaiAnVat.ServiceFramework;
using MaiAnVat.ServiceFramework.JobAndJobType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        private readonly IRegistrationJobService registrationJobService;
        public JobController(IJobTypeService jobTypeService, IJobService jobService, IListCategoryService listCategoryService, IWorkFlowStatusService workFlowStatusService, IRegistrationJobService registrationJobService)
        {
            this.jobTypeService = jobTypeService;
            this.jobService = jobService;
            this.listCategoryService = listCategoryService;
            this.workFlowStatusService = workFlowStatusService;
            this.registrationJobService = registrationJobService;
        }

        // GET: api/job
        [HttpGet]
        public IActionResult GetJob()
        {
            var jobs = GetAllJobs(null);
            return Ok(jobs.OrderByDescending(x => x.CreatedAtUtc).ToList());
        }

        // GET: api/job/unRegistedjobs
        [HttpGet("unRegistedjobs")]
        [HttpGet]
        public async Task<IActionResult> GetJobForRegister([FromQuery] Pagination pagination, [FromQuery] string searchTerm = null)
        {

            var jobs = GetJobForRegister(searchTerm);
            if (pagination == null)
                pagination = new Pagination();

            return Ok(await GetPaginatedResponse(jobs.OrderByDescending(x => x.CreatedAtUtc), pagination));
        }
        // GET: api/job/myJob
        [HttpGet("myJob")]
        public async Task<IActionResult> GetMyJob([FromQuery] Pagination pagination, [FromQuery] string searchTerm = null)
        {

            var jobs = GetMyJob(searchTerm);
            if (pagination == null)
                pagination = new Pagination();

            return Ok(await GetPaginatedResponse(jobs.OrderByDescending(x => x.CreatedAtUtc), pagination));
        }
        // GET: api/job/candicate
        [HttpGet("candicate")]
        public async Task<IActionResult> GetCandicate([FromQuery] Guid JobK, [FromQuery] Pagination pagination, [FromQuery] string searchTerm = null)
        {

            var jobs = GetJobCandiCate(JobK, searchTerm);
            if (pagination == null)
                pagination = new Pagination();

            return Ok(await GetPaginatedResponse(jobs.OrderByDescending(x => x.CreateTime), pagination));
        }          
        
        [HttpGet("all-candicate")]
        public async Task<IActionResult> GetAllCandicate([FromQuery] Pagination pagination, [FromQuery] string searchTerm = null)
        {

            var jobs = GetAllJobCandiCate(searchTerm);
            if (pagination == null)
                pagination = new Pagination();

            return Ok(await GetPaginatedResponse(jobs.OrderByDescending(x => x.CreateTime), pagination));
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
                job.ModifiedByUserFk = UserK;
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
            job.CreatedByUserFk = UserK;
            await jobService.CreateAsync(job);

            return CreatedAtAction("GetJob", new { id = job.JobK }, job);
        }

        // POST: api/registerJob
        [HttpPost("registerJob")]
        public async Task<IActionResult> RegisterJob([FromBody] RegistrationJob registrationJob)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            registrationJob.CreatedByUserFk = UserK;
            registrationJob.CreatedAtUtc = DateTime.Now;
            await registrationJobService.CreateAsync(registrationJob);

            return Ok();

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
                       join wf in qWorkFlowStatues on j.WorkflowStatusFk equals wf.WorkFlowStatusK into jwFG
                       from f in jwFG.DefaultIfEmpty()
                       select new JobDto()
                       {
                           JobK = j.JobK,
                           WorkflowStatusFk = f != default ? f.WorkFlowStatusK : default,
                           JobTypeFk = j.JobTypeFk,
                           Name = j.Name,
                           Title = j.Title,
                           Description = j.Description,
                           JobTypeName = jt.Name,
                           JobTypeColor = jt.ColorCode,
                           WorkFlowStatus = f != default ? f.Description: "",
                           CreatedAtUtc = j.CreatedAtUtc,
                           IsActivated = j.IsActivated,
                           CustomerOrder = j.CustomerOrder,
                           RegistrationDeadline = j.RegistrationDeadline
                       };
            return jobs;
        }

        private IQueryable<JobDto> GetJobForRegister(string searchTerm)
        {
            var jobs = registrationJobService.Find(x => x.IsAccepted || x.CreatedByUserFk == UserK).Select(x => x.JobFk).ToList();
            var allJobs = GetAllJobs(searchTerm);
            var unRegistedJob = allJobs.Where(x => jobs.Count > 0 ? !jobs.Contains(x.JobK) : true);
            return unRegistedJob;
        }

        private IQueryable<MyJobDto> GetMyJob(string searchTerm)
        {
            var registedJobs = registrationJobService
                .Find(x => x.CreatedByUserFk == UserK)
                .GroupBy(x => x.JobFk, (key, g) => g.OrderByDescending(e => e.CreatedAtUtc).First())
                .Select(x=>x);
            var allJobs = GetAllJobs(searchTerm);

            var myJob = from rJ in registedJobs
                        join j in allJobs on rJ.JobFk equals j.JobK
                        select new MyJobDto
                        {
                            JobK = j.JobK,
                            WorkflowStatusFk = j.WorkflowStatusFk,
                            JobTypeFk = j.JobTypeFk,
                            Name = j.Name,
                            Title = j.Title,
                            Description = j.Description,
                            JobTypeName = j.Name,
                            JobTypeColor = j.JobTypeColor,
                            WorkFlowStatus = j.WorkFlowStatus,
                            CreatedAtUtc = rJ.CreatedAtUtc,
                            IsActivated = j.IsActivated,
                            CustomerOrder = j.CustomerOrder,
                            RegistrationDeadline = j.RegistrationDeadline,
                            IsAccepted = rJ.IsAccepted
                        };
            return myJob;
        }

        private IQueryable<JobCandicateDto> GetJobCandiCate(Guid JobK, string searchTerm)
        {

            using (MaiAnVatContext db = new MaiAnVatContext())
            {
                var registedJobs = registrationJobService
                .Find(x => x.JobFk == JobK)
                .GroupBy(x => x.CreatedByUserFk, (key, g) => g.OrderByDescending(e => e.CreatedAtUtc).First())
                .Select(x => x);
                List<User> users = db.User.ToList();
                if (!string.IsNullOrEmpty(searchTerm))
                    users = users.Where(x => x.UserName.ToLower().Contains(searchTerm.ToLower())).ToList();
                var candicates = from rJ in registedJobs
                                 join u in users on rJ.CreatedByUserFk equals u.Id
                                 select new JobCandicateDto
                                 {
                                     CandiCateId = u.Id,
                                     Email = u.Email,
                                     IsAccepted = rJ.IsAccepted,
                                     UserName = u.UserName,
                                     CreateTime = rJ.CreatedAtUtc
                                 };
                return candicates;

            }
        }

        private IQueryable<JobCandicateDto> GetAllJobCandiCate(string searchTerm)
        {

            using (MaiAnVatContext db = new MaiAnVatContext())
            {
                var registedJobs = registrationJobService
                .Find()
                .GroupBy(x => new { x.JobFk, x.CreatedByUserFk }, (key, g) => g.OrderByDescending(e => e.CreatedAtUtc).First())
                .Select(x => x);
                List<User> users = db.User.ToList();
                if (!string.IsNullOrEmpty(searchTerm))
                    users = users.Where(x => x.UserName.ToLower().Contains(searchTerm.ToLower())).ToList();
                var candicates = from rJ in registedJobs
                                 join u in users on rJ.CreatedByUserFk equals u.Id
                                 join j in db.Job on rJ.JobFk equals j.JobK
                                 join jT in db.JobType on j.JobTypeFk equals jT.JobTypeK
                                 select new JobCandicateDto
                                 {
                                     JobName = j.Name,
                                     JobK = j.JobK,
                                     JobType = jT.Description,
                                     JobTypeK = jT.JobTypeK,
                                     CandiCateId = u.Id,
                                     Email = u.Email,
                                     IsAccepted = rJ.IsAccepted,
                                     UserName = u.UserName,
                                     CreateTime = rJ.CreatedAtUtc
                                 };
                return candicates;

            }
        }
        #endregion
    }
}
