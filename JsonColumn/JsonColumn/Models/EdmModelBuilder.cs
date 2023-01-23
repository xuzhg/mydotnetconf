using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace JsonColumn.Models;

public class EdmModelBuilder
{
    public static IEdmModel GetEdmModel()
    {
        ODataConventionModelBuilder buildler = new();
        buildler.EntitySet<Author>("Authors");
        return buildler.GetEdmModel();
    }
}
