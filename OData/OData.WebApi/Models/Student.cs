using System.ComponentModel.DataAnnotations;

namespace OData.WebApi.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        public string Name { get; set; }

        public Gender Gender { get; set; }

        public DateOnly BirthDay { get; set; }
    }
}
