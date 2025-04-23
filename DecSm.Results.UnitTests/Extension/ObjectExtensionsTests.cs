namespace DecSm.Results.UnitTests.Extension;

internal sealed class ObjectExtensionsTests
{
    [Test]
    public void ToResult_Creates_ResultOf()
    {
        // Arrange
        var value = new object();

        // Act
        var result = value.ToResult();

        // Assert
        result
            .ShouldNotBeNull()
            .ShouldSatisfyAllConditions(x => x.ValueOrDefault.ShouldBe(value));
    }

    [Test]
    public void ToResult_RejectsNullValue()
    {
        // Arrange
        object? value = null;

        // Act
        void Act()
        {
            _ = value.ToResult();
        }

        // Assert
        Assert.Throws<ArgumentNullException>(Act);
    }

    [Test]
    public void ToResultNullable_Creates_ResultOf()
    {
        // Arrange
        object? value = null;

        // Act
        var result = value.ToResultNullable();

        // Assert
        result
            .ShouldNotBeNull()
            .ShouldSatisfyAllConditions(x => x.ValueOrDefault.ShouldBeNull());
    }
}
