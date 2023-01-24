namespace OData.WebApi.Extensions
{
    public static class TypeHelpers
    {
        public static bool IsCollection(this Type clrType, out Type elementType)
        {
            elementType = clrType;
            if (clrType == null)
            {
                return false;
            }

            // see if this type should be ignored.
            if (clrType == typeof(string))
            {
                return false;
            }

            Type collectionInterface
                = clrType.GetInterfaces()
                    .Union(new[] { clrType })
                    .FirstOrDefault(
                        t => t.IsGenericType
                             && t.GetGenericTypeDefinition() == typeof(IEnumerable<>));

            if (collectionInterface != null)
            {
                elementType = collectionInterface.GetGenericArguments().Single();
                return true;
            }

            return false;
        }
    }
}
