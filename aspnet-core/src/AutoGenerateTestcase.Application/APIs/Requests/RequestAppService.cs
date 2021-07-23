
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoGenerateTestcase.Entities;
using AutoGenerateTestcase.APIs.Requests.Dto;
using System.Linq;
using Abp.Authorization;
using AutoGenerateTestcase.Authorization;
using System.IO;
using NccCore.Paging;
using NccCore.Extension;
using AutoGenerateTestcase.Authorization.Users;

namespace AutoGenerateTestcase.APIs.Requests
{
    public class RequestAppService: AutoGenerateTestcaseAppServiceBase
    {
        [HttpPost]
        //[AbpAuthorize(PermissionNames.CreateRequest)] TODO: ADD PERMISSION
        public async Task<RequestDto> Create(RequestDto input)
        {
            input.Status = Constants.Enum.RequestStatus.New;
            var item = ObjectMapper.Map<Request>(input);
            input.Id = await WorkScope.InsertAndGetIdAsync(item);
            return input;

        }
        [HttpPost]
        //[AbpAuthorize(PermissionNames.EditRequest)] TODO: ADD PERMISSION
        public async Task<RequestDto> Update(RequestDto input)
        {
            var item = await WorkScope.GetAsync<Request>(input.Id);
            ObjectMapper.Map(input, item);
            await WorkScope.UpdateAsync(item);
            return input;
        }
        [HttpPost]
        //[AbpAuthorize(PermissionNames.ViewAllRequest)] TODO: ADD PERMISSION
        public async Task<GridResult<RequestDto>> GetAllPaging(GridParam input)
        {
            var query = from r in WorkScope.GetAll<Request>()
                        join u in WorkScope.GetAll<User>() on
                        r.CreatorUserId equals u.Id
                        select new RequestDto
                        {
                            Id = r.Id,
                            Name = r.Name,
                            Status = r.Status,
                            Deadline = r.Deadline,
                            //UserName = u.FullName
                        };
            return await query.GetGridResult(query, input);
        }
        //[AbpAuthorize(PermissionNames.DeleteRequest)] TODO: ADD PERMISSION
        public async Task Delete(long id)
        {
            // TODO: Can't delete request when have Page;
            var isExistItem = await WorkScope.GetAll<RequestPage>().AnyAsync(x => x.RequestId == id);
            if (isExistItem)
            {
                throw new UserFriendlyException("Request with Id '" + id + "' has already created associated Pages. Please delete them before deleting the request.");
            }
            await WorkScope.DeleteAsync<Request>(id);
        }
    }
}
