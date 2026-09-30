using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HvpLesson12.Models
{
    [Table("StdClass")]
    public class StdClass
    {
        [Key]
        [Display(Name = "Mã lớp")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên lớp không được để trống")]
        [StringLength(100, ErrorMessage = "Tên lớp giới hạn 100 ký tự")]
        [Column(TypeName = "nvarchar(100)")]
        [Display(Name = "Tên lớp")]
        public string ClassName { get; set; } = string.Empty;

        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
