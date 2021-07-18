using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaiAnVat.Models.CustomModels
{
    public class JobDto
    {
        public Guid JobK { get; set; }
        public Guid JobStatusFk { get; set; }
        public Guid JobTypeFk { get; set; }
        public bool IsActivated { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeactivatedUtc { get; set; }
        public int? DeactivatedUserFk { get; set; }
        public Guid WorkflowStatusFk { get; set; }
        public string CustomerOrder { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? RegistrationDeadline { get; set; }
        public string JobTypeName { get; set; }
        public string JobTypeColor { get; set; }
        public string Satus { get; set; }
        public string WorkFlowStatus { get; set; }
    }
}
