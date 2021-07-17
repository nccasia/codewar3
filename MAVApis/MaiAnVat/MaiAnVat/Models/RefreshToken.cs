using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class RefreshToken
    {
        public Guid RefreshTokenK { get; set; }
        public int? UserFk { get; set; }
        public DateTime? IssuedUtc { get; set; }
        public DateTime? ExpiresUtc { get; set; }
        public string ProtectedTicket { get; set; }
    }
}
