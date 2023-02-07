using Microsoft.AspNetCore.OData.Query.Expressions;
using Microsoft.OData.UriParser;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;

namespace OData.WebApi.Extensions;

public class SchoolStudentFilterBinder : FilterBinder
{
    /*JSON column array query seems not supported. See details at: https://github.com/OData/AspNetCoreOData/issues/816
    // and: https://github.com/dotnet/efcore/issues/30132
    public override Expression BindCountNode(CountNode node, QueryBinderContext context)
    {
        // $filter=Branches/$count eq 1
        if (node.Source is CollectionComplexNode collectionComplexNode && 
            string.Equals(collectionComplexNode.Property.Name, "Branches", StringComparison.OrdinalIgnoreCase))
        {
            Expression source = context.CurrentParameter;

            // $it.BranchAddresses
            PropertyInfo emailsProperty = context.ElementClrType.GetProperty("BranchAddresses");
            source = Expression.Property(source, emailsProperty);

            source.Type.IsCollection(out Type elementType);

            MethodInfo countMethod = GetLongCountMethodInfo(elementType);

            Expression countExpression = Expression.Call(null, countMethod, new[] { source });

            return countExpression;
        }

        return base.BindCountNode(node, context);
    }

    public override Expression BindCountNode(CountNode node, QueryBinderContext context)
    {
        // $filter=Branches/$count eq 1
        if (node.Source is CollectionComplexNode collectionComplexNode &&
            string.Equals(collectionComplexNode.Property.Name, "Branches", StringComparison.OrdinalIgnoreCase))
        {
            Expression source = context.CurrentParameter;

            // $it.Branches
            PropertyInfo branchesProperty = context.ElementClrType.GetProperty("Branches");
            Expression branchesSource = Expression.Property(source, branchesProperty);

            MethodInfo deserializeMethod = MethodInfoHelpers.JsonDeserializeMi.MakeGenericMethod(typeof(IList<Address>));
            Expression jsonSerializerOptions = Expression.New(typeof(JsonSerializerOptions));

            // JsonSerializer.Deserialize<IList<Address>>(it.Branches, new JsonSerializerOptions())
            Expression deserializeCallExpression = Expression.Call(null, deserializeMethod, new[] { branchesSource, jsonSerializerOptions });

           // MethodInfo enumerableSelectMethod = MethodInfoHelpers.EnumerableSelectMi.MakeGenericMethod(typeof(School), typeof(IList<Address>));

            // Select($it, ($it) => JsonSerializer.Deserialize<IList<Address>>(it.Branches, new JsonSerializerOptions()))
            //Expression selectCall = Expression.Call(null, enumerableSelectMethod, new[] { context.CurrentParameter, deserializeCallExpression });

            MethodInfo countMethod = MethodInfoHelpers.EnumerableLongCountMi.MakeGenericMethod(typeof(Address));

            Expression countExpression = Expression.Call(null, countMethod, new[] { deserializeCallExpression });

            return countExpression;
        }

        return base.BindCountNode(node, context);
    }
    */

    public override Expression BindAnyNode(AnyNode anyNode, QueryBinderContext context)
    {
        Expression source = context.CurrentParameter;

        if (anyNode.Source is CollectionPropertyAccessNode collectionPropertyAccessNode &&
            string.Equals(collectionPropertyAccessNode.Property.Name, "ContactEmails", StringComparison.OrdinalIgnoreCase))
        // ?$filter = ContactEmails / any(a: a eq 'help@mercury.com')
        {
            // $it.Emails
            PropertyInfo emailsProperty = context.ElementClrType.GetProperty("Emails");
            Expression propertyValue = Expression.Property(source, emailsProperty);

            Expression constExp = null;
            // It could be anything in the body, here, i just process the binary operator
            BinaryOperatorNode bodyOperator = anyNode.Body as BinaryOperatorNode;
            if (bodyOperator != null)
            {
                // Here, i just process the 'Eq' for simplicity. You can add more.
                if (bodyOperator.OperatorKind == BinaryOperatorKind.Equal &&
                    bodyOperator.Right is ConstantNode constNode)
                {
                    constExp = Expression.Constant(constNode.Value);
                }
            }

            if (constExp != null)
            {
                // string.Contains
                MethodInfo containsMethodInfo = ConstainsMethodInfo;

                // $it.Emails.Contains("....")
                return Expression.Call(propertyValue, containsMethodInfo, constExp);
            }
        }

        if (anyNode.Source is CollectionComplexNode collectionComplexNode &&
             string.Equals(collectionComplexNode.Property.Name, "Branches", StringComparison.OrdinalIgnoreCase))
        {
            // ?$filter=Branches/any(a: a/City eq 'Kallangur')

            // $it.Branches
            PropertyInfo branchesProperty = context.ElementClrType.GetProperty("Branches"); // Branches is JSON string from DB
            Expression propertyValue = Expression.Property(source, branchesProperty);

            Expression constExp = null;
            if (anyNode.Body is BinaryOperatorNode bodyOperatorNode)
            {
                // It could be anything in the body, here, i just process the binary operator
                // Here, i just process the 'Eq' for simplicity. You can add more.
                if (bodyOperatorNode.OperatorKind == BinaryOperatorKind.Equal &&
                    bodyOperatorNode.Right is ConstantNode constNode)
                {
                    // Since JSON array query doesn't fully support, Here's the trick:
                    // Let's search the combination: "City":"Kallangur"
                    if (bodyOperatorNode.Left is SingleValuePropertyAccessNode properyAccessNode)
                    {
                        string propertyName = properyAccessNode.Property.Name;
                        string constNodeValue = constNode.Value.ToString();

                        constExp = Expression.Constant($"\"{propertyName}\":\"{constNodeValue}\"");
                    }
                }
            }

            if (constExp != null)
            {
                // string.Contains
                MethodInfo containsMethodInfo = ConstainsMethodInfo;

                // $it.Branches.Contains("....")
                return Expression.Call(propertyValue, containsMethodInfo, constExp);
            }
        }

        return base.BindAnyNode(anyNode, context);
    }

    // change in operator
    public override Expression BindInNode(InNode inNode, QueryBinderContext context)
    {
        // $filter='xyz' in ContactEmails

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
                MethodInfo containsMethodInfo = ConstainsMethodInfo;

                // 
                return Expression.Call(propertyValue, containsMethodInfo, singleValue);
            }
        }

        return base.BindInNode(inNode, context);
    }

    private static MethodInfo ConstainsMethodInfo = GetContainsMethodInfo();

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
