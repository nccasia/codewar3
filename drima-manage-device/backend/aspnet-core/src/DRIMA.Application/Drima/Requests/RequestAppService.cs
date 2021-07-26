using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using DRIMA.Drima.Requests.Dto;
using DRIMA.Entities;
using DRIMA.Extension;
using DRIMA.Paging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DRIMA.Constants.Enums;

namespace DRIMA.Drima.Requests
{
    [AbpAuthorize]
    [Route("api/services/app/requests")]
    public class RequestAppService : DRIMAAppServiceBase
    {
        [HttpPost]
        [Route("")]
        public async Task<RequestDto> SaveRequestAsync([FromBody] CreateRequestDto input)
        {
            if (input == null)
            {
                throw new UserFriendlyException("Invalid input");
            }

            if (input.Type == RequestType.Return)
            {
                if (!input.DeviceId.HasValue)
                {
                    throw new UserFriendlyException("Return device request require DeviceId");
                }
                var deviceRepository = WorkScope.GetRepo<Device, Guid>();
                var device = await deviceRepository.GetAsync(input.DeviceId.Value);
                if (device == null)
                {
                    throw new UserFriendlyException("Device does not exist");
                }
            }

            var requestRepository = WorkScope.GetRepo<Request, Guid>();
            Request request = null;
            var isEdit = input.Id.HasValue;
            if (isEdit)
            {
                request = await requestRepository.GetAsync(input.Id.Value);
                if (request == null)
                {
                    throw new UserFriendlyException("Request does not exist");
                }
            }

            request = input.PatchToEntity(request);

            if (isEdit)
            {
                await requestRepository.UpdateAsync(request);
            }
            else
            {
                await requestRepository.InsertAsync(request);
            }

            return ObjectMapper.Map<RequestDto>(request);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<RequestDto> Get(Guid id)
        {
            var requestRepository = WorkScope.GetRepo<Request, Guid>();
            var request = await requestRepository.GetAsync(id);
            if (request == null)
            {
                throw new UserFriendlyException($"No request found with ID {id}");
            }

            return ObjectMapper.Map<RequestDto>(request);
        }

        [HttpPost]
        [Route("list")]
        public async Task<GridResult<RequestDto>> GetList(GridParam input)
        {
            var query = WorkScope.GetAll<Request, Guid>().OrderByDescending(i => i.CreationTime);

            var requestGridResult = await query.GetGridResult(query, input);

            return new GridResult<RequestDto>(ObjectMapper.Map<List<RequestDto>>(requestGridResult.Items), requestGridResult.TotalCount);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(Guid id)
        {
            var requestRepository = WorkScope.GetRepo<Request, Guid>();
            var request = await requestRepository.GetAsync(id);
            if (request == null)
            {
                throw new UserFriendlyException($"No request found with ID {id}");
            }

            await requestRepository.DeleteAsync(request);
        }
    }
}
