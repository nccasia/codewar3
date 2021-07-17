using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class Group
    {
        public Group()
        {
            GroupPermission = new HashSet<GroupPermission>();
            InverseParentGroupFkNavigation = new HashSet<Group>();
            JobAssignmentList = new HashSet<JobAssignmentList>();
            UserGroup = new HashSet<UserGroup>();
        }

        public Guid GroupK { get; set; }
        public int Identity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFk { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public int? ModifiedByUserFk { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? ParentGroupFk { get; set; }

        public Group ParentGroupFkNavigation { get; set; }
        public ICollection<GroupPermission> GroupPermission { get; set; }
        public ICollection<Group> InverseParentGroupFkNavigation { get; set; }
        public ICollection<JobAssignmentList> JobAssignmentList { get; set; }
        public ICollection<UserGroup> UserGroup { get; set; }
    }
}
