namespace DecSm.Results.UnitTests.Extension;

internal sealed class ReasonExtensionsTests
{
    private record CustomReason : ReasonBase;

    [Test]
    public void Trim_EmptyReason_ReturnsUnmodifiedReason()
    {
        // Arrange
        var reason = new Success("Reason");

        // Act
        var result = reason.Trim();

        // Assert
        result.ShouldBe(reason);
    }

    [Test]
    public void Trim_CustomReason_ReturnsUnmodifiedReason()
    {
        // Arrange
        var reason = new CustomReason();

        // Act
        var result = reason.Trim();

        // Assert
        result.ShouldBe(reason);
    }

    [Test]
    public void Trim_AggregateEmptyReason_ReturnsDefaultSuccess()
    {
        // Arrange
        var reason = new AggregateReason(ImmutableArray<IReason>.Empty);

        // Act
        var result = reason.Trim();

        // Assert
        result.ShouldBe(Success.Default);
    }

    [Test]
    public void Trim_AggregateSingleReason_ReturnsSingleReason()
    {
        // Arrange
        var success = new Success("Reason");
        var reason = new AggregateReason(new[] { success });

        // Act
        var result = reason.Trim();

        // Assert
        result.ShouldBe(success);
    }

    [Test]
    public void Trim_AggregateMultipleReasons_ReturnsUnmodifiedAggregateReason()
    {
        // Arrange
        var reason = new AggregateReason([new Success("Success"), new Error("Error")]);

        // Act
        var result = reason.Trim();

        // Assert
        result.ShouldBe(reason);
    }

    [Test]
    public void Trim_AggregateMultipleReasons_ReturnsTrimmedAggregateReason()
    {
        // Arrange
        var success = new Success("Success");
        var error = new Error("Error");
        var reason = new AggregateReason([success, new AggregateReason([error])]);

        // Act
        var result = reason.Trim();

        // Assert
        result
            .ShouldBeOfType<AggregateReason>()
            .ShouldSatisfyAllConditions(x => x.Reasons.Length.ShouldBe(2),
                x => x
                    .Reasons[0]
                    .ShouldBe(success),
                x => x
                    .Reasons[1]
                    .ShouldBe(error));
    }
}
