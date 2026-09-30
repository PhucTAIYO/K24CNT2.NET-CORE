using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HvpLesson12.Models
{
    [Table("Banner")]
    public class Banner
    {
        [Key]
        [Display(Name = "Mã banner")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên banner không được để trống")]
        [StringLength(150, ErrorMessage = "Tên banner giới hạn 150 ký tự")]
        [Column(TypeName = "nvarchar(150)")]
        [Display(Name = "Tên banner")]
        public string Name { get; set; } = string.Empty;

        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        [Display(Name = "Hình ảnh")]
        public string? Image { get; set; }

        [StringLength(500, ErrorMessage = "Mô tả giới hạn 500 ký tự")]
        [Column(TypeName = "nvarchar(500)")]
        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Column(TypeName = "tinyint")]
        [Display(Name = "Trạng thái")]
        public byte Status { get; set; } = 1;
    }
}
