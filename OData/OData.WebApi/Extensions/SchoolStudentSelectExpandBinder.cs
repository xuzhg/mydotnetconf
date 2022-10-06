using Microsoft.AspNetCore.OData.Query.Expressions;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;
using OData.WebApi.Models;
using System.Linq.Expressions;
using System.Reflection;

namespace OData.WebApi.Extensions;

public class SchoolStudentSelectExpandBinder : SelectExpandBinder
{
    public SchoolStudentSelectExpandBinder(IFilterBinder filterBinder, IOrderByBinder orderByBinder)
        : base(filterBinder, orderByBinder)
    {
    }

    // 
    public override Expression CreatePropertyValueExpression(QueryBinderContext context, IEdmStructuredType elementType, IEdmProperty edmProperty, Expression source, FilterClause filterClause, ComputeClause computeClause = null)
    {
        if (edmProperty.Type.IsCollection())
        {
            IEdmModel model = context.Model;
            if (string.Equals(edmProperty.Name, "ContactEmails", StringComparison.OrdinalIgnoreCase))
            {
                PropertyInfo emailsProperty = source.Type.GetProperty("Emails");
                Expression propertyValue = Expression.Property(source, emailsProperty);

                return propertyValue;
            }
        }


        return base.CreatePropertyValueExpression(context, elementType, edmProperty, source, filterClause, computeClause);
    }
}
