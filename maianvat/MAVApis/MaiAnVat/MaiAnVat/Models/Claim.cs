using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class Claim
    {
        public int UserClaimK { get; set; }
        public int UserFk { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }

        public User UserFkNavigation { get; set; }
    }
}
