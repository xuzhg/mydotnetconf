using Microsoft.AspNetCore.OData.Query.Expressions;
using Microsoft.OData.UriParser;
using OData.WebApi.Models;
using System.Linq.Expressions;
using System.Reflection;

namespace OData.WebApi.Extensions;

public class SchoolStudentFilterBinder : FilterBinder
{
    // change in operator
    public override Expression BindInNode(InNode inNode, QueryBinderContext context)
    {
        // $filter='xyz' in Collection

        if (inNode.Right is CollectionPropertyAccessNode rightCollectionPropertyAccessNode)
        {
            if (string.Equals(rightCollectionPropertyAccessNode.Property.Name, "ContactEmails", StringComparison.OrdinalIgnoreCase))
            {
                Expression singleValue = Bind(inNode.Left, context);

                PropertyInfo emailsProperty = context.ElementClrType.GetProperty("Emails");

                // $it
                Expression source = context.CurrentParameter;

                // $it.Emails
                Expression propertyValue = Expression.Property(source, emailsProperty);

                // string.Contains
                MethodInfo containsMethodInfo = GetContainsMethodInfo();

                // 
                return Expression.Call(propertyValue, containsMethodInfo, singleValue);
            }
        }

        return base.BindInNode(inNode, context);
    }

    private static MethodInfo GetContainsMethodInfo()
    {
        // If you want to ignore case, fetch the other overload of "Contains"
        var containsMethods = typeof(string).GetMethods().Where(x => x.Name == "Contains");

        foreach (var containsMethod in containsMethods)
        {
            ParameterInfo[] parameters = containsMethod.GetParameters();
            if (parameters.Length != 1)
            {
                continue;
            }

            if (parameters[0].ParameterType == typeof(string))
            {
                return containsMethod;
            }
        }

        throw new NotSupportedException();
    }
}
