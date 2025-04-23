namespace DecSm.Results.UnitTests.Serialization;

internal sealed class JsonSerializerOptionsExtensionsTests
{
    [Test]
    public void ConfigureForResults_WithNullOptions_ThrowsArgumentNullException()
    {
        // Arrange
        JsonSerializerOptions? options = null;

        // Act
        var exception = Should.Throw<ArgumentNullException>(() => options!.ConfigureForResults());

        // Assert
        exception.ParamName.ShouldBe(nameof(options));
    }

    [Test]
    public void ConfigureForResults_AddsResultConverters()
    {
        // Arrange
        var options = new JsonSerializerOptions();

        // Act
        options.ConfigureForResults();

        // Assert
        options.Converters.ShouldSatisfyAllConditions(converters => converters.ShouldContain(x => x is ResultConverter),
            converters => converters.ShouldContain(x => x is ResultOfConverterFactory));
    }
}
