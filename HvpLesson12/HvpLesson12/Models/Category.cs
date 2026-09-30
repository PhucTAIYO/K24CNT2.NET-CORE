using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HvpLesson12.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        [Display(Name = "Mã danh mục")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [StringLength(100, ErrorMessage = "Tên danh mục giới hạn 100 ký tự")]
        [Column(TypeName = "nvarchar(100)")]
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "tinyint")]
        [Display(Name = "Trạng thái")]
        public byte Status { get; set; } = 1;

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // danh sách sản phẩm theo danh mục
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
