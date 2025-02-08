using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Runtime.Serialization;

namespace Sudoku.Api.Swagger;

public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            schema.Enum.Clear();
            schema.Type = "string";
            
            foreach (var name in Enum.GetNames(context.Type))
            {
                var enumMemberAttribute = context.Type
                    .GetField(name)?
                    .GetCustomAttributes(true)
                    .OfType<EnumMemberAttribute>()
                    .FirstOrDefault();

                var enumValue = enumMemberAttribute?.Value ?? name;
                schema.Enum.Add(new Microsoft.OpenApi.Any.OpenApiString(enumValue));
            }
        }
    }
} 