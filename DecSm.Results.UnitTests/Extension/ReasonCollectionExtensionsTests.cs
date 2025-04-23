namespace DecSm.Results.UnitTests.Extension;

internal sealed class ReasonCollectionExtensionsTests
{
    [Test]
    public void AggregateReasons_WithOneReason_ReturnsReason()
    {
        // Arrange
        var reason = new Success("Success");

        // Act
        var result = new[] { reason }.AggregateReasons();

        // Assert
        result.ShouldBe(reason);
    }

    [Test]
    public void AggregateReasons_WithMultipleReasons_ReturnsAggregateReason()
    {
        // Arrange
        var reasons = new IReason[] { new Success("Success"), new Error("Error") };

        // Act
        var result = reasons.AggregateReasons();

        // Assert
        result.ShouldBeOfType<AggregateReason>();
    }
}
