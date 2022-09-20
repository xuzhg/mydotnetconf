namespace OData.WebApi.Models
{
    public class @Class
    {
        public int ClassId { get; set; }

        public string ClassName { get; set; }

        public IList<Student> Students { get; set; }
    }
}
