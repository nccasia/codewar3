using AutoGenerateTestcase.APIs.RequestPages.Dto;
using AutoGenerateTestcase.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGenerateTestcase.APIs.RequestPages
{
    public class RequestPageAppService : AutoGenerateTestcaseAppServiceBase
    {
        public async Task<RequestPageDto> Create(RequestPageDto input)
        {
            input.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<RequestPage>(input));
            return input;
        }

        public async Task<RequestPageDto> Update(RequestPageDto input)
        {
            var requestPage = await WorkScope.GetAsync<RequestPage>(input.Id);
            ObjectMapper.Map(input, requestPage);
            await WorkScope.UpdateAsync(requestPage);
            return input;
        }

        public async Task<List<GetRequestPageDto>> Get(long requestId)
        {
            return await WorkScope.GetAll<RequestPage>()
                .Where(x => x.RequestId == requestId)
                .Select(x => new GetRequestPageDto
                {
                    Id = x.Id,
                    PageName = x.PageName,
                    PageType = x.PageType.ToString(),
                    RequestId = x.RequestId
                }).ToListAsync();
        }

        public async Task Delete(long Id)
        {
            await WorkScope.DeleteAsync<RequestPage>(Id);
        }

        [HttpPost]
        public async Task<GridResult<GetRequestPageDto>> GetAllPaging(GridParam input)
        {
            var rs = WorkScope.GetAll<RequestPage>().Select(x => new GetRequestPageDto
            {
                Id = x.Id,
                PageName = x.PageName,
                PageType = x.PageType.ToString(),
                RequestId = x.RequestId
            });
            return await rs.GetGridResult(rs, input);
        }
    }
}
