namespace AG_MT2.Models
{
    public class SelectableCourse
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string CourseNumber { get; set; }
        public string CourseTitle { get; set; }

        public string CourseInfo { get; set; }
        public int CourseCredits { get; set; }
        public bool IsSelected { get; set; }


        public SelectableCourse()
        {
        }

        public SelectableCourse(int CourseId, string CourseNumber, string CourseTitle, int CourseCredits, bool IsSelected)
        {
            this.CourseId = CourseId;
            this.CourseNumber = CourseNumber;
            this.CourseTitle = CourseTitle;
            this.CourseCredits = CourseCredits;
            this.CourseInfo = CourseNumber + " " + CourseTitle;
            this.IsSelected = IsSelected;
        }

    }
}
