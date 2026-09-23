using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lab_Lesson09.Models.Attributes;
using Microsoft.AspNetCore.Http;

namespace Lab_Lesson09.Models
{
    public class Product : IValidatableObject
    {
        [Display(Name = "Mã sản phẩm")]
        public int Id { get; set; }

        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm bắt buộc phải nhập.")]
        [StringLength(150, MinimumLength = 6, ErrorMessage = "Tên sản phẩm phải có từ 6 đến 150 ký tự.")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Tên file hình ảnh")]
        public string? Image { get; set; }

        [Display(Name = "File hình ảnh")]
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        [Display(Name = "Giá chuẩn (VNĐ)")]
        [Required(ErrorMessage = "Giá chuẩn bắt buộc phải nhập.")]
        [Range(100000f, float.MaxValue, ErrorMessage = "Giá chuẩn phải nhỏ nhất là 100.000 VNĐ.")]
        public float Price { get; set; }

        [Display(Name = "Giá khuyến mãi (VNĐ)")]
        [Required(ErrorMessage = "Giá khuyến mãi bắt buộc phải nhập.")]
        public float SalePrice { get; set; }

        [Display(Name = "Mô tả sản phẩm")]
        [Required(ErrorMessage = "Mô tả sản phẩm bắt buộc phải nhập.")]
        [StringLength(1500, ErrorMessage = "Mô tả sản phẩm không được vượt quá 1500 ký tự.")]
        [NoSensitiveWords("die", "admin", "fack", "fuck", "bitch", "hack", "spam", ErrorMessage = "Mô tả không được chứa các từ nhạy cảm (ví dụ: die, admin, fack...).")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Danh mục sản phẩm")]
        [Required(ErrorMessage = "Vui lòng chọn danh mục cho sản phẩm.")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn một danh mục hợp lệ.")]
        public int CategoryId { get; set; }

        // Navigation property
        public Category? Category { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Kiểm tra SalePrice không âm và nhỏ hơn giá chuẩn ít nhất 10% (SalePrice <= Price * 0.9f)
            if (SalePrice < 0)
            {
                yield return new ValidationResult("Giá khuyến mãi không được âm (phải >= 0).", new[] { nameof(SalePrice) });
            }
            else if (Price >= 100000f && SalePrice > (Price * 0.9f))
            {
                float maxAllowedSalePrice = Price * 0.9f;
                yield return new ValidationResult(
                    $"Giá khuyến mãi ({SalePrice:N0} đ) phải nhỏ hơn giá chuẩn ít nhất 10% (tối đa là {maxAllowedSalePrice:N0} đ).",
                    new[] { nameof(SalePrice) }
                );
            }
        }
    }
}
