using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class JobAttachment
    {
        public Guid JobAttachmentK { get; set; }
        public int Identity { get; set; }
        public Guid FileStorageFk { get; set; }
        public Guid ListCategoryFk { get; set; }
        public string FileName { get; set; }
        public string FileDescription { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFk { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public int? ModifiedByUserFk { get; set; }
        public bool IsDeleted { get; set; }
        public bool? IsDisabled { get; set; }

        public FileStorage FileStorageFkNavigation { get; set; }
        public ListCategory ListCategoryFkNavigation { get; set; }
    }
}
