using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HVP_Lesson09.Models.DataViewModels
{
    public class PttMemberRegister
    {
        [DisplayName("Mã thành viên")]
        public int PttMemberId { get; set; }

        [DisplayName("Tên đăng nhập")]
        [Required(ErrorMessage = "Tên đăng nhập không để trống")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Tên đăng nhập có độ dài trong khoảng 2 - 20 ký tự")]
        public string PttUserName { get; set; } = string.Empty;

        [DisplayName("Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [DataType(DataType.Password)]
        public string PttPassword { get; set; } = string.Empty;

        [DisplayName("Hộp thư điện tử")]
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string? PttEmail { get; set; }

        [DisplayName("Số điện thoại")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại phải có 10 chữ số và bắt đầu bằng số 0")]
        public string? PttPhoneNumber { get; set; }

        [DisplayName("Họ và tên")]
        [Required(ErrorMessage = "Họ và tên không được để trống")]
        public string? PttFullName { get; set; }

        [DisplayName("Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime? PttBirthday { get; set; }
    }
}
