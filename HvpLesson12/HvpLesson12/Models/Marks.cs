using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HvpLesson12.Models
{
    [Table("Marks")]
    public class Marks
    {
        [Required(ErrorMessage = "Môn học không được để trống")]
        [Display(Name = "Môn học")]
        public int SubjectId { get; set; }

        [Required(ErrorMessage = "Sinh viên không được để trống")]
        [Display(Name = "Sinh viên")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Điểm số không được để trống")]
        [Range(0, 10, ErrorMessage = "Điểm số phải trong khoảng từ 0 đến 10")]
        [Display(Name = "Điểm số")]
        public float Score { get; set; }

        [ForeignKey("SubjectId")]
        [Display(Name = "Môn học")]
        public virtual Subjects? Subject { get; set; }

        [ForeignKey("StudentId")]
        [Display(Name = "Sinh viên")]
        public virtual Student? Student { get; set; }
    }
}
