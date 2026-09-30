using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HvpLesson12.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [Display(Name = "Mã sản phẩm")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(150, ErrorMessage = "Tên sản phẩm giới hạn 150 ký tự")]
        [Column(TypeName = "nvarchar(150)")]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "varchar(150)")]
        [Display(Name = "Hình ảnh")]
        public string? Image { get; set; }

        [Required(ErrorMessage = "Giá sản phẩm không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá sản phẩm phải lớn hơn hoặc bằng 0")]
        [Display(Name = "Giá bán")]
        public float Price { get; set; }

        [Display(Name = "Giá khuyến mãi")]
        public float SalePrice { get; set; }

        [Column(TypeName = "tinyint")]
        [Display(Name = "Trạng thái")]
        public byte Status { get; set; } = 1;

        [StringLength(1000, ErrorMessage = "Nội dung mô tả giới hạn 1000 ký tự")]
        [Column(TypeName = "ntext")]
        [Display(Name = "Mô tả")]
        public string? Descriptions { get; set; }

        [Required(ErrorMessage = "Danh mục sản phẩm không được để trống")]
        [Display(Name = "Danh mục")]
        public int CategoryId { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Khóa ngoại tới bảng Category
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
    }
}
