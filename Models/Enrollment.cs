namespace AG_MT2.Models
{
    public class Enrollment
    {
        public Student Student { get; set; }
        public List<SelectableCourse> SelectableCourses { get; set; }

        public Enrollment()
        {
            SelectableCourses = new List<SelectableCourse>();
        }

        public Enrollment(Student student, List<SelectableCourse> selectableCourses)
        {
            this.Student = student;
            this.SelectableCourses = selectableCourses;
        }
    }
}
