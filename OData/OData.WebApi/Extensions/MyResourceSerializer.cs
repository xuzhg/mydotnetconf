using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Formatter.Serialization;
using Microsoft.AspNetCore.OData.Query.Expressions;
using Microsoft.OData.Edm;
using Microsoft.OData;
using Microsoft.OData.UriParser;
using OData.WebApi.Models;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Extensions.Primitives;
using System.Text.Json;
using System.Collections.Generic;

namespace OData.WebApi.Extensions;

public class MyResourceSerializer : ODataResourceSerializer
{
    public MyResourceSerializer(IODataSerializerProvider serializerProvider) 
        : base(serializerProvider)
    {
    }

    // $search=A+
    public override ODataProperty CreateStructuralProperty(IEdmStructuralProperty structuralProperty, ResourceContext resourceContext)
    {
        if (string.Equals(structuralProperty.Name, "ContactEmails", StringComparison.OrdinalIgnoreCase))
        {
            ODataSerializerContext writeContext = resourceContext.SerializerContext;

            IODataEdmTypeSerializer serializer = SerializerProvider.GetEdmTypeSerializer(structuralProperty.Type);

            object propertyValue = resourceContext.GetPropertyValue(structuralProperty.Name);

            ODataValue value;
            if (propertyValue is null)
            {
                value = new ODataCollectionValue();
            }
            else if (propertyValue is string strValue)
            {
                IList<string> list = JsonSerializer.Deserialize<IList<string>>(strValue);

                ODataCollectionSerializer collectionSerializer = serializer as ODataCollectionSerializer;
                value = collectionSerializer.CreateODataValue(list, structuralProperty.Type, resourceContext.SerializerContext);
            }
            else
            {
                // It's a string already, because there's no $select=ContactEmails
                ODataCollectionSerializer collectionSerializer = serializer as ODataCollectionSerializer;
                value = collectionSerializer.CreateODataValue(propertyValue, structuralProperty.Type, resourceContext.SerializerContext);
            }

            return new ODataProperty
            {
                Name = "ContactEmails",
                Value = value
            };
        }

        return base.CreateStructuralProperty(structuralProperty, resourceContext);
    }
}
