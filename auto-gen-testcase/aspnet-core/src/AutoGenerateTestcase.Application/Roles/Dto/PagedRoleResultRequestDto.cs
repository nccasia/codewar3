using Abp.Application.Services.Dto;

namespace AutoGenerateTestcase.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

