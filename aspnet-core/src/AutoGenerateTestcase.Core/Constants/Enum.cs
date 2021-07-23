
namespace AutoGenerateTestcase.Constants
{
    public class Enum
    {
        public enum RequestStatus : byte
        {
            New = 0,
            Sent = 1,
            Process = 2,
            Reject = 3
        }
        public enum RequestPageType : byte
        {
            List = 0,
            SingleCreateEditForm = 1,
            MultipleCreateEditForm = 2,
            DetailsPage = 3
        }
        public enum PageFieldType : byte
        {
            String = 0,
            Int = 1,
            Float = 2,
            Dropdown = 3,
            DateTime = 4,
            Boolean = 5
        }
        public enum PageFieldConditionType : byte
        {
            Greater = 0,
            Smaller = 1,
            AppearAfterSelect = 2,
            Logic = 3
        }
    }
}

