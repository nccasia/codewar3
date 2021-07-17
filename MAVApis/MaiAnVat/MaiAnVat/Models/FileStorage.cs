using System;
using System.Collections.Generic;

namespace MaiAnVat.Models
{
    public partial class FileStorage
    {
        public Guid FileStorageK { get; set; }
        public string FileFolder { get; set; }
        public string FileName { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserFk { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
        public int? ModifiedByUserFk { get; set; }
        public bool IsDeleted { get; set; }
    }
}
