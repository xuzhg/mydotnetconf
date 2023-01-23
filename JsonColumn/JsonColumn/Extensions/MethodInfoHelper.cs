using JsonColumn.Models;
using System.Reflection;

namespace JsonColumn.Extensions;

public static class MethodInfoHelper
{
    private static MethodInfo _EnumerableLongCountMi;

    public static MethodInfo EnumerableLongCountMi
    {
        get
        {
            if (_EnumerableLongCountMi == null)
            {
                _EnumerableLongCountMi = GetLongCountMethodInfo();
            }

            return _EnumerableLongCountMi;
        }
    }

    private static MethodInfo GetLongCountMethodInfo()
    {
        var longCountMethods = typeof(Enumerable).GetMethods().Where(x => x.Name == "LongCount");

        return longCountMethods.First(x => x.GetParameters().Length == 1);
    }
}
