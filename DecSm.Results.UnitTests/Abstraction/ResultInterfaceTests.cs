namespace DecSm.Results.UnitTests.Abstraction;

[SuppressMessage("ReSharper", "RedundantExplicitParamsArrayCreation")]
internal sealed class ResultInterfaceTests
{
    [Test]
    public void ResultInterface_ProvidesIsFailedProperty()
    {
        // Arrange
        var resultInterface = typeof(IResult);

        // Act
        var isFailedProperty = resultInterface.GetProperty("IsFailed");

        // Assert
        isFailedProperty.ShouldSatisfyAllConditions([
            () => isFailedProperty.ShouldNotBeNull(),
            () => isFailedProperty?.PropertyType.ShouldBe(typeof(bool)),
            () => isFailedProperty?.GetMethod.ShouldNotBeNull(),
            () => isFailedProperty?.SetMethod.ShouldBeNull(),
        ]);
    }

    [Test]
    public void ResultInterface_ProvidesReasonProperty()
    {
        // Arrange
        var resultInterface = typeof(IResult);

        // Act
        var reasonProperty = resultInterface.GetProperty("Reason");

        // Assert
        reasonProperty.ShouldSatisfyAllConditions([
            () => reasonProperty.ShouldNotBeNull(),
            () => reasonProperty?.PropertyType.ShouldBe(typeof(IReason)),
            () => reasonProperty?.GetMethod.ShouldNotBeNull(),
            () => reasonProperty?.SetMethod.ShouldBeNull(),
        ]);
    }

    [Test]
    public void ResultInterface_ProvidesDeconstructMethod()
    {
        // Arrange
        var resultInterface = typeof(IResult);

        // Act
        var deconstructMethod = resultInterface.GetMethod("Deconstruct");

        // Assert
        deconstructMethod.ShouldSatisfyAllConditions([
            () => deconstructMethod.ShouldNotBeNull(),
            () => deconstructMethod?.ReturnType.ShouldBe(typeof(void)),
            () => deconstructMethod
                ?.GetParameters()
                .ShouldSatisfyAllConditions([
                    () => deconstructMethod
                        .GetParameters()
                        .Length
                        .ShouldBe(2),
                    () => deconstructMethod
                        .GetParameters()[0]
                        .ParameterType
                        .ShouldBe(typeof(bool).MakeByRefType()),
                    () => deconstructMethod
                        .GetParameters()[1]
                        .ParameterType
                        .ShouldBe(typeof(IError).MakeByRefType()),
                ]),
        ]);
    }

    [Test]
    public void ResultInterface_ProvidesToStringMethod()
    {
        // Arrange
        var resultInterface = typeof(IResult);

        // Act
        var toStringMethod = resultInterface.GetMethod("ToString");

        // Assert
        toStringMethod.ShouldSatisfyAllConditions([
            () => toStringMethod.ShouldNotBeNull(),
            () => toStringMethod?.ReturnType.ShouldBe(typeof(string)),
            () => toStringMethod
                ?.GetParameters()
                .ShouldBeEmpty(),
        ]);
    }
}
