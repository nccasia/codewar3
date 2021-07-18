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
    public class WorkFlowStatusController : BaseApiController
    {
        private readonly IWorkFlowStatusService workFlowStatusService;
        public WorkFlowStatusController(IWorkFlowStatusService workFlowStatusService)
        {
            this.workFlowStatusService = workFlowStatusService;
        }

        // GET: api/workflowstatus
        [HttpGet]
        public IActionResult GetAllWorkFlowStatus()
        {
            var workFlowStatuses = GetAllWorkFlowStatues(null);
            return Ok(workFlowStatuses.Select(x => new { Name = x.Name, Description = x.Description, StatusCode = x.StatusCode, CreatedAtUtc = x.CreatedAtUtc}).OrderByDescending(x => x.CreatedAtUtc).ToList());
        }

        [HttpGet("workflowstatues")]
        public async Task<IActionResult> GetWorkFlowStatues([FromQuery] Pagination pagination, [FromQuery] string searchTerm = null)
        {
            var workFlowStatuses = GetAllWorkFlowStatues(searchTerm);
            if (pagination == null)
                pagination = new Pagination();

            return Ok(await GetPaginatedResponse(workFlowStatuses.Select(x => new { Name = x.Name, Description = x.Description, StatusCode = x.StatusCode, CreatedAtUtc = x.CreatedAtUtc }).OrderByDescending(x => x.CreatedAtUtc), pagination));

        }

        // GET: api/workflowstatus/272169AE-0F52-4544-A6EB-4607DE1D3796
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkFlowStatusByKey([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workFlowStatus = await workFlowStatusService.ReadAsync(id);

            if (workFlowStatus == null)
            {
                return NotFound();
            }

            return Ok(workFlowStatus);
        }

        // PUT: api/workflowstatus/272169AE-0F52-4544-A6EB-4607DE1D3796
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkFlowStatus([FromRoute] Guid id, [FromBody] WorkFlowStatus workFlowStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workFlowStatus.WorkFlowStatusK)
            {
                return BadRequest();
            }
            try
            {
                await workFlowStatusService.UpdateAsync(id, workFlowStatus);
                return Ok("Update Success");
            }
            catch (Exception)
            {
                return BadRequest("Update Failed");
            }
        }

        // POST: api/workflowstatus
        [HttpPost]
        public async Task<IActionResult> PostWorkFlowStatus([FromBody] WorkFlowStatus workFlowStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await workFlowStatusService.CreateAsync(workFlowStatus);

            return CreatedAtAction("GetWorkFlowStatusByKey", new { id = workFlowStatus.WorkFlowStatusK }, workFlowStatus);
        }


        // DELETE: api/workflowstatus/272169AE-0F52-4544-A6EB-4607DE1D3796
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkFlowStatus([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await workFlowStatusService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Delete Failed");
            }
        }

        #region API HELPER
        private IQueryable<WorkFlowStatus> GetAllWorkFlowStatues(string searchTerm)
        {
            var qWorkFlowStatues = workFlowStatusService.Find(!string.IsNullOrEmpty(searchTerm) ? searchTerm : null);
            return qWorkFlowStatues;
        }
        #endregion
    }
}
