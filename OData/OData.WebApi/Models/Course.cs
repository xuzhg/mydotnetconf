namespace OData.WebApi.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public Address MailAddress { get; set; }


        public IList<string> ContactEmails { get; set; }

        public IList<Student> Students { get; set; }
    }
}
