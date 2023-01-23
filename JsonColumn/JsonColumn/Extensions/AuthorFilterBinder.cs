using JsonColumn.Models;
using Microsoft.AspNetCore.OData.Query.Expressions;
using Microsoft.OData.UriParser;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;

namespace JsonColumn.Extensions
{
    public class AuthorFilterBinder : FilterBinder
    {
        public override Expression BindCountNode(CountNode node, QueryBinderContext context)
        {
            // $filter=Addresses/$count eq 1
            if (node.Source is CollectionComplexNode collectionComplexNode &&
                string.Equals(collectionComplexNode.Property.Name, "Addresses", StringComparison.OrdinalIgnoreCase))
            {
                Expression source = context.CurrentParameter;

                // $it.Addresses
                PropertyInfo addressesProperty = context.ElementClrType.GetProperty("Addresses");
                Expression addressesSource = Expression.Property(source, addressesProperty);

                MethodInfo countMethod = MethodInfoHelper.EnumerableLongCountMi.MakeGenericMethod(typeof(Address));

                Expression countExpression = Expression.Call(null, countMethod, new[] { addressesSource });

                return countExpression;
            }

            return base.BindCountNode(node, context);
        }
    }
}
