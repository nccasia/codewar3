using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class UserLogin
    {
        public int UserLoginK { get; set; }
        public int UserFk { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }

        public User UserFkNavigation { get; set; }
    }
}
