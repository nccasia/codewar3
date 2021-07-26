using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class CategoryClassification
    {
        public Guid CategoryClassificationK { get; set; }
        public int Identity { get; set; }
        public string Name { get; set; }
        public Guid? CategoryFk { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFk { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public int? ModifiedByUserFk { get; set; }
        public bool IsDeleted { get; set; }
        public string Description { get; set; }
        public bool? IsDisabled { get; set; }

        public Category CategoryFkNavigation { get; set; }
    }
}
