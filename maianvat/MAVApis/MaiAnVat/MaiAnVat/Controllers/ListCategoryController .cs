using MaiAnVat.Common;
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
    public class ListCategoryController : BaseApiController
    {
        private readonly IListCategoryService listCategoryService;
        private readonly ICategoryService categoryService;
        public ListCategoryController(IListCategoryService listCategoryService, ICategoryService categoryService)
        {
            this.listCategoryService = listCategoryService;
            this.categoryService = categoryService;
        }

        // GET: api/listcategory
        [HttpGet]
        public IActionResult GetAllListCategory()
        {
            var listcategories = GetAllListCategories(null);
            return Ok(listcategories.Select(x => new { ListCategoryK = x.ListCategoryK,  Name = x.Name, Description = x.Description, CreatedAtUtc = x.CreatedAtUtc}).OrderByDescending(x => x.CreatedAtUtc).ToList());
        }

        // GET: api/listcategory/categories
        [HttpGet("categories")]
        public IActionResult GetAllCategory()
        {
            var categories = GetAllCategories(null);
            return Ok(categories.Select(x => new { CategoryK = x.CategoryK, Name = x.Name, Description = x.Description, CreatedAtUtc = x.CreatedAtUtc }).OrderByDescending(x => x.CreatedAtUtc).ToList());
        }

        // GET: api/listcategory/jobtypes
        [HttpGet("jobtypes")]
        public IActionResult GetListCategoryJobTypes()
        {
            var listcategories = GetAllListCategoryByCategoryName(Constants.JOB_TYPE);
            var result = listcategories
                .Select(x => new { ListCategoryK = x.ListCategoryK, Name = x.Name, Description = x.Description, CreatedAtUtc = x.CreatedAtUtc })
                .OrderByDescending(x => x.CreatedAtUtc).ToList();
            return Ok(result);
        }

        // GET: api/listcategory/jobRejectReasons
        [HttpGet("jobRejectReasons")]
        public IActionResult GetListCategoryJobRejectReason()
        {
            var listcategories = GetAllListCategoryByCategoryName(Constants.JOB_REJECTED_REASON);
            var result = listcategories
                .Select(x => new { ListCategoryK = x.ListCategoryK, Name = x.Name, Description = x.Description, CreatedAtUtc = x.CreatedAtUtc })
                .OrderByDescending(x => x.CreatedAtUtc).ToList();
            return Ok(result);
        }

        // GET: api/listcategory/actiontypes
        [HttpGet("actiontypes")]
        public IActionResult GetListCategoryActionTypes()
        {
            var listcategories = GetAllListCategoryByCategoryName(Constants.SCHEDULE_TYPE);
            return Ok(listcategories.Select(x => new { ListCategoryK = x.ListCategoryK, Name = x.Name, Description = x.Description, CreatedAtUtc = x.CreatedAtUtc }).OrderByDescending(x => x.CreatedAtUtc).ToList());
        }

        // GET: api/listcategory/satatustypes
        [HttpGet("satatustypes")]
        public IActionResult GetListCategoryStatusTypes()
        {
            var listcategories = GetAllListCategoryByCategoryName(Constants.STATUS_TYPE);
            return Ok(listcategories.Select(x => new { ListCategoryK = x.ListCategoryK, Name = x.Name, Description = x.Description, CreatedAtUtc = x.CreatedAtUtc }).OrderByDescending(x => x.CreatedAtUtc).ToList());
        }

        // GET: api/listcategory/scheduletypes
        [HttpGet("scheduletypes")]
        public IActionResult GetListCategoryScheduleTypes()
        {
            var listcategories = GetAllListCategoryByCategoryName(Constants.SCHEDULE_TYPE);
            return Ok(listcategories.Select(x => new { ListCategoryK = x.ListCategoryK, Name = x.Name, Description = x.Description, CreatedAtUtc = x.CreatedAtUtc }).OrderByDescending(x => x.CreatedAtUtc).ToList());
        }


        [HttpGet("listcategories")]
        public async Task<IActionResult> GetListCategories([FromQuery] Pagination pagination, [FromQuery] string searchTerm = null)
        {
            var listcategories = GetAllListCategories(searchTerm);
            if (pagination == null)
                pagination = new Pagination();

            return Ok(await GetPaginatedResponse(listcategories.Select(x => new { Name = x.Name, Description = x.Description, CreatedAtUtc = x.CreatedAtUtc }).OrderByDescending(x => x.CreatedAtUtc), pagination));

        }

        // GET: api/listcategory/272169AE-0F52-4544-A6EB-4607DE1D3796
        [HttpGet("{id}")]
        public async Task<IActionResult> GetListCategoryByKey([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var listcategory = await listCategoryService.ReadAsync(id);

            if (listcategory == null)
            {
                return NotFound();
            }

            return Ok(listcategory);
        }

        // PUT: api/listcategory/272169AE-0F52-4544-A6EB-4607DE1D3796
        [HttpPut("{id}")]
        public async Task<IActionResult> Putlistcategory([FromRoute] Guid id, [FromBody] ListCategory listcategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != listcategory.ListCategoryK)
            {
                return BadRequest();
            }
            try
            {
                await listCategoryService.UpdateAsync(id, listcategory);
                return Ok("Update Success");
            }
            catch (Exception)
            {
                return BadRequest("Update Failed");
            }
        }

        // POST: api/listcategory
        [HttpPost]
        public async Task<IActionResult> PostListCategory([FromBody] ListCategory listcategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await listCategoryService.CreateAsync(listcategory);

            return CreatedAtAction("GetListCategoryByKey", new { id = listcategory.ListCategoryK }, listcategory);
        }


        // DELETE: api/listcategory/272169AE-0F52-4544-A6EB-4607DE1D3796
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListCategory([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await listCategoryService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Delete Failed");
            }
        }

        #region API HELPER
        private IQueryable<ListCategory> GetAllListCategories(string searchTerm)
        {
            var qListCategories = listCategoryService.Find(!string.IsNullOrEmpty(searchTerm) ? searchTerm : null);
            return qListCategories;
        }
        private IQueryable<Category> GetAllCategories(string searchTerm)
        {
            var qCategories = categoryService.Find(!string.IsNullOrEmpty(searchTerm) ? searchTerm : null);
            return qCategories;
        }

        private IQueryable<ListCategory> GetAllListCategoryByCategoryName(string categoryName)
        {
            var qListCategories = listCategoryService.GetListCategoryByCategoryName(categoryName);
            return qListCategories;
        }
        #endregion
    }
}
