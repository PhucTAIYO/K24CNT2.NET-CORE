using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HvpLesson12.Models
{
    [Table("Student")]
    public class Student
    {
        [Key]
        [Display(Name = "Mã sinh viên")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sinh viên không được để trống")]
        [StringLength(100, ErrorMessage = "Tên sinh viên giới hạn 100 ký tự")]
        [Column(TypeName = "nvarchar(100)")]
        [Display(Name = "Họ và tên")]
        public string StudentName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(100, ErrorMessage = "Email giới hạn 100 ký tự")]
        [Column(TypeName = "nvarchar(100)")]
        [Display(Name = "Email")]
        public string StudentEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [StringLength(50, ErrorMessage = "Số điện thoại giới hạn 50 ký tự")]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Điện thoại")]
        public string StudentPhone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        [StringLength(150, ErrorMessage = "Địa chỉ giới hạn 150 ký tự")]
        [Column(TypeName = "nvarchar(150)")]
        [Display(Name = "Địa chỉ")]
        public string StudentAddress { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Ảnh đại diện giới hạn 100 ký tự")]
        [Column(TypeName = "nvarchar(100)")]
        [Display(Name = "Ảnh đại diện")]
        public string? StudentAvatar { get; set; }

        [Required(ErrorMessage = "Ngày sinh không được để trống")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày sinh")]
        public DateTime StudentBirthday { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn lớp học")]
        [Display(Name = "Lớp học")]
        public int ClassId { get; set; }

        [ForeignKey("ClassId")]
        [Display(Name = "Lớp học")]
        public virtual StdClass? StdClass { get; set; }

        public virtual ICollection<Marks> Marks { get; set; } = new List<Marks>();
    }
}
