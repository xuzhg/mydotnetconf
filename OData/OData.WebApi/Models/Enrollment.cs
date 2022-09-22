using System.ComponentModel.DataAnnotations;

namespace OData.WebApi.Models
{
    public class Enrollment
    {
        public string CourseTitle { get; set; }

        public int Grade { get; set; }
    }
}
