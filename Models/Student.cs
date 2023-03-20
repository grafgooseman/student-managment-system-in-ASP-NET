using System.ComponentModel.DataAnnotations;

namespace AG_MT2.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [StringLength(15, MinimumLength = 3, ErrorMessage = "{0} must be between 3 and 15 characters")]
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "{0} is required")]
        public string? LastName { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [CustomDateRegex("^\\d{2}/\\d{2}/\\d{4}$", "01/01/1981", "12/31/2000")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[Range(typeof(DateTime), "01/01/1981", "12/31/2000", ErrorMessage = "Date of Birth must be between Jan 01, 1981 and Dec 31, 2000")] // Just doesnt work
        public DateTime DateOfBirth { get; set; }


        public List<Course> Courses { get; set; }

        public string FullName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }

        public Student()
        {
            Courses = new List<Course>();
        }
    }
}
