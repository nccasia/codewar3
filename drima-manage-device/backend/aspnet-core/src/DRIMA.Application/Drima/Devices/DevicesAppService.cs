using Abp.Authorization;
using Abp.UI;
using DRIMA.Drima.Devices.Dto;
using DRIMA.Entities;
using DRIMA.Extension;
using DRIMA.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DRIMA.Drima.Devices
{
    [AbpAuthorize]
    [Route("api/services/app/devices")]
    public class DevicesAppService : DRIMAAppServiceBase
    {
        [HttpPost]
        [Route("")]
        public async Task<DeviceDto> SaveDeviceAsync([FromBody] CreateDeviceDto input)
        {
            if (input == null)
            {
                throw new UserFriendlyException("Invalid input");
            }

            var deviceRepository = WorkScope.GetRepo<Device, Guid>();
            Device device = null;
            var isEdit = input.Id.HasValue;
            if (isEdit)
            {
                device = await WorkScope.GetAll<Device, Guid>()
                    .Where(i => i.Id == input.Id.Value)
                    .Include(i => i.Images)
                    .FirstOrDefaultAsync();
                if (device == null)
                {
                    throw new UserFriendlyException("Device does not exist");
                }
                // TODO: Delete existing images
            }

            device = input.PatchToEntity(device);
            // TODO: Add new images to device entity

            if (isEdit)
            {
                await deviceRepository.UpdateAsync(device);
            }
            else
            {
                var qrCodeGenerator = new QRCodeGenerator();
                var qrCodeData = qrCodeGenerator.CreateQrCode(device.Id.ToString(), QRCodeGenerator.ECCLevel.Q);
                var qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(20, Color.Black, Color.White, icon: null);

                using var stream = new MemoryStream();
                qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                var qrCodeImageFileBytes = stream.ToArray();
                var qrCodeImageEntity = new DeviceImage
                {
                    Base64 = Convert.ToBase64String(qrCodeImageFileBytes),
                    DeviceId = device.Id,
                    Type = Constants.Enums.DeviceImageType.QrCode,
                    Id = Guid.NewGuid()
                };
                device.Images.Add(qrCodeImageEntity);
                await deviceRepository.InsertAsync(device);
            }

            return ObjectMapper.Map<DeviceDto>(device);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<DeviceDto> Get(Guid id)
        {
            var device = await WorkScope.GetAll<Device, Guid>()
                    .Where(i => i.Id == id)
                    .Include(i => i.Images)
                    .FirstOrDefaultAsync();
            if (device == null)
            {
                throw new UserFriendlyException($"No device found with ID {id}");
            }

            var deviceDto = ObjectMapper.Map<DeviceDto>(device);
            if (deviceDto.GuaranteeDate.HasValue)
            {
                deviceDto.GuaranteeDate = DateTime.SpecifyKind(device.GuaranteeDate.Value, DateTimeKind.Utc);
            }
            return deviceDto;
        }

        [HttpPost]
        [Route("list")]
        public async Task<GridResult<DeviceDto>> GetList(GridParam input)
        {
            var query = WorkScope.GetAll<Device, Guid>().OrderBy(i => i.Name);
            
            var deviceGridResult = await query.GetGridResult(query, input);
            foreach (var item in deviceGridResult.Items)
            {
                if (item.GuaranteeDate.HasValue)
                {
                    item.GuaranteeDate = DateTime.SpecifyKind(item.GuaranteeDate.Value, DateTimeKind.Utc);
                }
            }

            return new GridResult<DeviceDto>(ObjectMapper.Map<List<DeviceDto>>(deviceGridResult.Items), deviceGridResult.TotalCount);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task Delete(Guid id)
        {
            var deviceRepository = WorkScope.GetRepo<Device, Guid>();
            var device = await deviceRepository.GetAsync(id);
            if (device == null)
            {
                throw new UserFriendlyException($"No device found with ID {id}");
            }

            await deviceRepository.DeleteAsync(device);
        }
    }
}
