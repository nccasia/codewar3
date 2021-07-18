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
        public ListCategoryController(IListCategoryService listCategoryService)
        {
            this.listCategoryService = listCategoryService;
        }

        // GET: api/listcategory
        [HttpGet]
        public IActionResult GetAllListCategory()
        {
            var listcategories = GetAllListCategories(null);
            return Ok(listcategories.Select(x => new { Name = x.Name, Description = x.Description, CreatedAtUtc = x.CreatedAtUtc}).OrderByDescending(x => x.CreatedAtUtc).ToList());
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
        #endregion
    }
}
