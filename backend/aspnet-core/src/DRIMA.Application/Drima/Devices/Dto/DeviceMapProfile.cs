using AutoMapper;
using DRIMA.Entities;
using System.Linq;

namespace DRIMA.Drima.Devices.Dto
{
    public class DeviceMapProfile : Profile
    {
        public DeviceMapProfile()
        {
            CreateMap<CreateDeviceDto, Device>()
                .ForMember(e => e.Images, opt => opt.Ignore());
            CreateMap<Device, DeviceDto>()
                .ForMember(d => d.QrCodeBase64, opt => opt.MapFrom(e => e.Images.FirstOrDefault(i => i.Type == Constants.Enums.DeviceImageType.QrCode).Base64));
        }
    }
}
