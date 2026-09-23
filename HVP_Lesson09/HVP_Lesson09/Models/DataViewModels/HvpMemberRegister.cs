using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HVP_Lesson09.Models.DataViewModels
{
    public class HvpMemberRegister
    {
        [DisplayName("Mã thành viên")]
        public int HvpMemberId { get; set; }

        [DisplayName("Tên đăng nhập")]
        [Required(ErrorMessage = "Tên đăng nhập không để trống")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Tên đăng nhập có độ dài trong khoảng 3 - 20 ký tự")]
        public string HvpUserName { get; set; } = string.Empty;

        [DisplayName("Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [DataType(DataType.Password)]
        public string HvpPassword { get; set; } = string.Empty;

        [DisplayName("Hộp thư điện tử")]
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string? HvpEmail { get; set; }

        [DisplayName("Số điện thoại")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại phải có 10 chữ số và bắt đầu bằng số 0")]
        public string? HvpPhoneNumber { get; set; }

        [DisplayName("Họ và tên")]
        [Required(ErrorMessage = "Họ và tên không được để trống")]
        public string? HvpFullName { get; set; }

        [DisplayName("Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime? HvpBirthday { get; set; }
    }
}
