using System.Reflection;
using System.Text.Json;

namespace OData.WebApi.Extensions
{
    public static class MethodInfoHelpers
    {
        private static MethodInfo _JsonDeserializeMi;
        private static MethodInfo _EnumerableSelectMi;
        private static MethodInfo _EnumerableLongCountMi;

        public static MethodInfo JsonDeserializeMi
        {
            get
            {
                if (_JsonDeserializeMi == null)
                {
                    _JsonDeserializeMi = GetJsonSerializer();
                }

                return _JsonDeserializeMi;
            }
        }

        public static MethodInfo EnumerableSelectMi
        {
            get
            {
                if (_EnumerableSelectMi == null)
                {
                    _EnumerableSelectMi = GetEnumerableSelect();
                }

                return _EnumerableSelectMi;
            }
        }

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

        private static MethodInfo GetEnumerableSelect()
        {
            // public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector);
            var selects = typeof(Enumerable).GetMethods().Where(x => x.Name == "Select");
            foreach (var select in selects)
            {
                ParameterInfo[] parameters = select.GetParameters();
                if (parameters.Length != 2)
                {
                    continue;
                }

                if (parameters[1].ParameterType.GetGenericArguments().Length == 2)
                {
                    return select;
                }
            }

            throw new NotSupportedException();
        }

        private static MethodInfo GetJsonSerializer()
        {
            // public static TValue? Deserialize<TValue>(string json, JsonSerializerOptions? options = null);
            var deses = typeof(JsonSerializer).GetMethods().Where(x => x.Name == "Deserialize");

            foreach (var deserializeMethodInfo in deses)
            {
                ParameterInfo[] parameters = deserializeMethodInfo.GetParameters();
                if (parameters.Length != 2)
                {
                    continue;
                }

                if (parameters[0].ParameterType == typeof(string) && parameters[1].ParameterType == typeof(JsonSerializerOptions))
                {
                    return deserializeMethodInfo;
                }
            }

            throw new NotSupportedException();
        }

        private static MethodInfo GetLongCountMethodInfo()
        {
            var longCountMethods = typeof(Enumerable).GetMethods().Where(x => x.Name == "LongCount");

            return longCountMethods.First(x => x.GetParameters().Length == 1);
        }
    }
}
