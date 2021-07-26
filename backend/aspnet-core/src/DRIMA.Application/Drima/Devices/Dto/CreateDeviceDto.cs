using DRIMA.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using static DRIMA.Constants.Enums;

namespace DRIMA.Drima.Devices.Dto
{
    public class CreateDeviceDto
    {
        public Guid? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime? GuaranteeDate { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        [Required]
        public DeviceStatus Status { get; set; }
        public DateTime? BorrowedDate { get; set; }
        public long? BorrowedUserId { get; set; }
        public string Description { get; set; }

        public Device PatchToEntity(Device device)
        {
            if (device == null)
            {
                device = new Device { Id = Guid.NewGuid() };
            }
            device.Name = Name;
            device.GuaranteeDate = GuaranteeDate;
            device.Manufacturer = Manufacturer;
            device.Status = Status;
            device.BorrowedDate = BorrowedDate;
            device.BorrowedUserId = BorrowedUserId;
            device.Description = Description;

            return device;
        }
    }
}
