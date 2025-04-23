namespace DecSm.Results.UnitTests.Abstraction;

[SuppressMessage("ReSharper", "RedundantExplicitParamsArrayCreation")]
internal sealed class ErrorInterfaceTests
{
    [Test]
    public void ErrorInterface_InheritsFromReasonInterface()
    {
        // Arrange
        var errorInterface = typeof(IError);

        // Act
        var reasonInterface = errorInterface.GetInterface("IReason");

        // Assert
        reasonInterface.ShouldNotBeNull();
    }
}
