using Abp.Domain.Entities;
using DRIMA.Constants;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRIMA.Entities
{
    public class DeviceImage : Entity<Guid>
    {
        [Column(nameof(Base64), TypeName = "varchar(MAX)")]
        public string Base64 { get; set; }

        public Guid? DeviceId { get; set; }

        public Device Device { get; set; }

        [Column(TypeName = "tinyint")]
        public Enums.DeviceImageType Type { get; set; }
    }
}
