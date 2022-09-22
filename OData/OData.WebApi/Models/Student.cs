using System.ComponentModel.DataAnnotations;

namespace OData.WebApi.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public int Grade { get; set; }

        public int SchoolId { get; set; }

        public DateOnly BirthDay { get; set; }
    }
}
