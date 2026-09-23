using System.ComponentModel.DataAnnotations;

namespace Lab_Lesson09.Models
{
    public class Category
    {
        [Display(Name = "Mã danh mục")]
        public int Id { get; set; }

        [Display(Name = "Tên danh mục")]
        [Required(ErrorMessage = "Tên danh mục bắt buộc phải nhập")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Tên danh mục phải có từ 2 đến 100 ký tự")]
        public string Name { get; set; } = string.Empty;
    }
}
