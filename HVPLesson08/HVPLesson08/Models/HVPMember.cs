using System.ComponentModel;

namespace HVPLesson08.Models
{
    public class HVPMember
    {
        public string HVPMemberId { get; set; }
        public string HVPUserName { get; set; }
        public string HVPPassword { get; set; }

        [DisplayName("Họ và tên")]
        public string HVPFullName { get; set; }
        public string HVPEmail { get; set; }
    }
}
