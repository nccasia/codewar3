using System.ComponentModel;

namespace DRIMA.Constants
{
    public class Enums
    {
        public enum DeviceStatus
        {
            AVAILABLE,
            USING,
            BROKEN
        }

        public enum RequestStatus
        {
            Pending,
            Rejected,
            Approved,
            Delivered
        }

        public enum RequestType
        {
            [Description("Borrow")]
            Borrow,
            [Description("Return")]
            Return,
            [Description("Buy New")]
            BuyNew
        }

        public enum RequestPriority
        {
            Low,
            Medium,
            High
        }

        public enum DeviceImageType
        {
            Normal,
            QrCode
        }
    }
}
