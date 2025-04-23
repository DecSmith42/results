namespace DecSm.Results.UnitTests.Abstraction;

[SuppressMessage("ReSharper", "RedundantExplicitParamsArrayCreation")]
internal sealed class SuccessInterfaceTests
{
    [Test]
    public void SuccessInterface_InheritsFromReasonInterface()
    {
        // Arrange
        var successInterface = typeof(ISuccess);

        // Act
        var reasonInterface = successInterface.GetInterface("IReason");

        // Assert
        reasonInterface.ShouldNotBeNull();
    }
}
