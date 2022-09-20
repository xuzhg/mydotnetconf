using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace OData.WebApi.Models
{
    public static class EdmModelBuilder
    {
        public static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder buildler = new();

            buildler.EntitySet<School>("Schools");
            buildler.EntitySet<Student>("Students");

            buildler.EntityType<School>().Ignore(c => c.Emails);

            return buildler.GetEdmModel();
        }
    }
}
