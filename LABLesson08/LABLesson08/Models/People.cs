using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LABLesson08.Models
{
    public class People
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Họ và tên")]
        [Required(ErrorMessage = "Họ và tên không được để trống")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Họ và tên từ 3 đến 50 ký tự")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Địa chỉ email")]
        [Required(ErrorMessage = "Địa chỉ email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; } = string.Empty;

        [DisplayName("Số điện thoại")]
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại phải gồm 10 số và bắt đầu bằng số 0")]
        public string Phone { get; set; } = string.Empty;

        [DisplayName("Địa chỉ nơi ở")]
        [Required(ErrorMessage = "Địa chỉ nơi ở không được để trống")]
        public string Address { get; set; } = string.Empty;

        [DisplayName("Ảnh đại diện")]
        public string? Avatar { get; set; }

        [DisplayName("Ngày sinh nhật")]
        [Required(ErrorMessage = "Ngày sinh nhật không được để trống")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }

        [DisplayName("Giới thiệu bản thân")]
        public string? Bio { get; set; }

        [DisplayName("Giới tính")]
        [Required(ErrorMessage = "Vui lòng chọn giới tính")]
        public byte Gender { get; set; }
    }
}
