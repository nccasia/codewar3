using AutoMapper;
using DRIMA.Entities;
using DRIMA.Extension;

namespace DRIMA.Drima.Requests.Dto
{
    public class RequestMapProfile : Profile
    {
        public RequestMapProfile()
        {
            CreateMap<Request, RequestDto>()
                .ForMember(d => d.Type, opt => opt.MapFrom(e => EnumerationEx.GetEnumDescription(e.Type)))
                .ForMember(d => d.Status, opt => opt.MapFrom(e => e.Status.ToString()))
                .ForMember(d => d.Priority, opt => opt.MapFrom(e => e.Priority.ToString()));
        }
    }
}
