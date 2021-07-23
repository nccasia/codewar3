using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaiAnVat.Models.CustomModels
{
    public class JobCandicateDto
    {
        public bool IsAccepted { get; set; }
        public int CandiCateId { get; set; }
        public string UserName  { get; set; }
        public string Email { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
