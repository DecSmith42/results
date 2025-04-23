namespace DecSm.Results.UnitTests.Abstraction;

[SuppressMessage("ReSharper", "RedundantExplicitParamsArrayCreation")]
[SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
internal sealed class ReasonInterfaceTests
{
    [Test]
    public void ReasonInterface_ProvidesMessageProperty()
    {
        // Arrange
        var reasonInterface = typeof(IReason);

        // Act
        var messageProperty = reasonInterface.GetProperty("Message");

        // Assert
        messageProperty.ShouldSatisfyAllConditions([
            () => messageProperty.ShouldNotBeNull(),
            () => messageProperty?.PropertyType.ShouldBe(typeof(string)),
            () => messageProperty?.GetMethod.ShouldNotBeNull(),
            () => messageProperty?.SetMethod.ShouldBeNull(),
        ]);
    }
}
