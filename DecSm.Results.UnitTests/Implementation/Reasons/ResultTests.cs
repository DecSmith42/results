namespace DecSm.Results.UnitTests.Implementation.Reasons;

internal sealed class ResultTests
{
    [Test]
    public void Result_Ok_ReturnsOkResult()
    {
        var result = Result.Ok();

        result.ShouldSatisfyAllConditions(() => result.IsFailed.ShouldBeFalse(),
            () => result.Reason.ShouldBeNull(),
            () => result
                .ToString()
                .ShouldBe("Result: Ok"));
    }

    [Test]
    public void Result_FailWithReason_ReturnsFailedResultWithReason()
    {
        var error = new Error("Error message");
        var result = Result.Failure(error);

        result.ShouldSatisfyAllConditions(() => result.IsFailed.ShouldBeTrue(),
            () => result.Reason.ShouldBe(error),
            () => result
                .ToString()
                .ShouldBe("Result: Failure, Reason=[Error: 'Error message']"));
    }

    [Test]
    public void Result_FailWithException_ReturnsFailedResultWithExceptionError()
    {
        var exception = new Exception("Exception message");
        var result = Result.Failure(new ExceptionError(exception));

        result.ShouldSatisfyAllConditions(() => result.IsFailed.ShouldBeTrue(),
            () => result
                .Reason
                .ShouldBeOfType<ExceptionError>()
                .Exception
                .ShouldBe(exception),
            () => result
                .ToString()
                .ShouldStartWith("Result: Failure, Reason=[Exception: 'Exception message'"));
    }
}
