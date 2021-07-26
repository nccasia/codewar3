using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaiAnVat.Models.CustomModels
{
    public class JobCandicateDto
    {
        public RegistrationJob RegistrationJob { get; set; }
        public JobDto Job { get; set; }

        public Guid RegistrationJobK { get; set; }
        public string JobName { get; set; }
        public string JobType { get; set; }
        public Guid JobK { get; set; }
        public Guid JobTypeK { get; set; }
        private bool IsActive { get; set; }
        public bool IsAccepted { get; set; }
        public int CandiCateId { get; set; }
        public string UserName  { get; set; }
        public string Email { get; set; }
        public string ReviewStatus { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ReviewTimeStamp { get; set; }
    }
}
