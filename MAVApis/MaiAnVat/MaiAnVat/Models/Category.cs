using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class Category
    {
        public Category()
        {
            CategoryClassification = new HashSet<CategoryClassification>();
            ListCategory = new HashSet<ListCategory>();
        }

        public Guid CategoryK { get; set; }
        public int Identity { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFk { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public int? ModifiedByUserFk { get; set; }
        public bool IsDeleted { get; set; }
        public bool? Editable { get; set; }
        public string Description { get; set; }
        public bool? IsDisabled { get; set; }

        public ICollection<CategoryClassification> CategoryClassification { get; set; }
        public ICollection<ListCategory> ListCategory { get; set; }
    }
}
