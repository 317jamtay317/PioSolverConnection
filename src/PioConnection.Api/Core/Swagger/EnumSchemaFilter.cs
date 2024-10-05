using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PioConnection.Api.Core.Swagger;

/// <summary>
/// A schema filter that enhances the OpenAPI schema for enum types by adding
/// a list of possible enum values and updating the schema description.
/// </summary>
public class EnumSchemaFilter : ISchemaFilter
{
    /// <summary>
    /// Applies the schema modifications to handle enums.
    /// If the context type is an enum, this method updates the schema to
    /// include all possible enum values and appends a description listing those values.
    /// </summary>
    /// <param name="schema">The OpenAPI schema to modify.</param>
    /// <param name="context">The schema filter context, containing metadata about the type.</param>
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        // Check if the type in the context is an enum.
        if (context.Type.IsEnum)
        {
            // Set the schema's Enum property to contain all enum names as OpenApiString values.
            schema.Enum = Enum.GetNames(context.Type)
                .Select(name => new OpenApiString(name))  // Convert each enum name to OpenApiString.
                .ToList<IOpenApiAny>();  // Convert the result to a list of IOpenApiAny.

            // Append a description listing the possible enum values.
            schema.Description += $"\nPossible values: {string.Join(", ", Enum.GetNames(context.Type))}";
        }
    }
}
