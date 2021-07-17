using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class AccessTokens
    {
        public int AccessTokenId { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
        public DateTimeOffset EffectiveTime { get; set; }
        public int ExpiresIn { get; set; }
        public string UserAgent { get; set; }
        public string Ip { get; set; }
    }
}
