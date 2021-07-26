using System.ComponentModel.DataAnnotations;

namespace AutoGenerateTestcase.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}