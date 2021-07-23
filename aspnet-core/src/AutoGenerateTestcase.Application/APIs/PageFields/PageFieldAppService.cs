
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoGenerateTestcase.Entities;
using AutoGenerateTestcase.APIs.PageFields.Dto;
using System.Linq;
using Abp.Authorization;
using AutoGenerateTestcase.Authorization;
using System.IO;
using NccCore.Paging;
using NccCore.Extension;
using AutoGenerateTestcase.Authorization.Users;

namespace AutoGenerateTestcase.APIs.PageFields
{
    public class PageFieldAppService: AutoGenerateTestcaseAppServiceBase
    {
        [HttpPost]
        //[AbpAuthorize(PermissionNames.CreatePageField)] TODO: ADD PERMISSION
        public async Task<PageFieldDto> Create(PageFieldDto input)
        {
            input.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<PageField>(input));
            return input;

        }
        [HttpPost]
        //[AbpAuthorize(PermissionNames.EditPageField)] TODO: ADD PERMISSION
        public async Task<PageFieldDto> Update(PageFieldDto input)
        {
            var item = await WorkScope.GetAsync<PageField>(input.Id);
            
            await WorkScope.UpdateAsync(ObjectMapper.Map(input, item));
            return input;
        }
        [HttpPost]
        //[AbpAuthorize(PermissionNames.ViewAllPageField)] TODO: ADD PERMISSION
        public async Task<List<GetPageFieldDto>> GetAllByPageId(long pageId)
        {
            var query = WorkScope.GetAll<PageField>().Where(s => s.RequestPageId == pageId)
                        .Select(x => new GetPageFieldDto
                        {
                            Id = x.Id,
                            Name = x.Name,
                            RequestPageId = x.RequestPageId,
                            Type = x.Type.ToString(),
                            MinValue = x.MinValue,
                            MaxValue = x.MaxValue,
                            Nullable = x.Nullable,
                            Note = x.Note
                        });
            return await query.ToListAsync();
        }
        //[AbpAuthorize(PermissionNames.DeletePageField)] TODO: ADD PERMISSION
        public async Task Delete(long id)
        {
            await WorkScope.DeleteAsync<PageField>(id);
        }
    }
}
