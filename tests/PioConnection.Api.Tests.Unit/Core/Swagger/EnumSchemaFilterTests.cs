using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using NSubstitute;
using PioConnection.Api.Core.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PioConnection.Api.Tests.Unit.Core.Swagger;


public class EnumSchemaFilterTests
{
    private readonly EnumSchemaFilter _filter;

    public EnumSchemaFilterTests()
    {
        _filter = new EnumSchemaFilter();
    }

    // A sample enum for testing
    private enum SampleEnum
    {
        ValueOne,
        ValueTwo,
        ValueThree
    }

    [Fact]
    public void Apply_ShouldAddEnumValues_WhenTypeIsEnum()
    {
        // Arrange
        var schema = new OpenApiSchema();
        var context = CreateSchemaFilterContext(typeof(SampleEnum));

        // Act
        _filter.Apply(schema, context);

        // Assert
        var expectedEnumValues = Enum.GetNames(typeof(SampleEnum)).ToList();
        var actualEnumValues = schema.Enum.Cast<OpenApiString>().Select(e => e.Value).ToList();

        Assert.Equal(expectedEnumValues.Count, actualEnumValues.Count);
        Assert.All(expectedEnumValues, expected => Assert.Contains(expected, actualEnumValues));
    }

    [Fact]
    public void Apply_ShouldAppendEnumValuesToDescription_WhenTypeIsEnum()
    {
        // Arrange
        var schema = new OpenApiSchema { Description = "Test description" };
        var context = CreateSchemaFilterContext(typeof(SampleEnum));

        // Act
        _filter.Apply(schema, context);

        // Assert
        var expectedDescription = "Test description\nPossible values: ValueOne, ValueTwo, ValueThree";
        Assert.Equal(expectedDescription, schema.Description);
    }

    [Fact]
    public void Apply_ShouldNotModifySchema_WhenTypeIsNotEnum()
    {
        // Arrange
        var schema = new OpenApiSchema { Description = "Test description" };
        var context = CreateSchemaFilterContext(typeof(string));  // Non-enum type

        // Act
        _filter.Apply(schema, context);

        // Assert
        Assert.True(schema.Enum == null || !schema.Enum.Any(), "Enum list should be null or empty");
        Assert.Equal("Test description", schema.Description); // Description should not be modified
    }

    // Helper method to create a real SchemaFilterContext for testing
    private SchemaFilterContext CreateSchemaFilterContext(Type type)
    {
        var schemaRepository = new SchemaRepository();
        return new SchemaFilterContext(type, null, schemaRepository);
    }
}