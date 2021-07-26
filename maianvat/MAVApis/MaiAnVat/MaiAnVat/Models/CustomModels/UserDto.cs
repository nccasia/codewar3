using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaiAnVat.Models.CustomModels
{
    public class UserDto : User
    {
        public string Password { get; set; }
    }

    public class UserDTO
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public bool? Status { get; set; }
        public DateTime? DateOfBirth { get; set; }
        //public ICollection<UserGroup> UserGroup { get; set; }
        public string Password { get; set; }
    }
}
