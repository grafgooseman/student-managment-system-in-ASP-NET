using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AG_MT2.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Course Number")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Course number must be 5 digits.")]
        public string? CourseNumber { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Title")]
        public string? Title { get; set; }

        [Range(3, 6)]
        [Display(Name = "Credits")]
        public int Credits { get; set; }

        public List<Student> Students { get; set; }

        public Course()
        {
            Students = new List<Student>();
        }
    }
}
