namespace DecSm.Results.UnitTests.Implementation.Reasons;

internal sealed class ExceptionErrorTests
{
    [Test]
    public void ExceptionError_CanBeCreated()
    {
        // Arrange
        var exception = new Exception("Hello");

        // Act
        var result = new ExceptionError(exception);

        // Assert
        result
            .ShouldNotBeNull()
            .ShouldSatisfyAllConditions(x => x.Message.ShouldBe("Hello"),
                x => x.Exception.ShouldBe(exception),
                x => x.Data.ShouldBeEmpty());
    }

    [Test]
    public async Task ToString_Prints_Exception_Correctly()
    {
        // Arrange
        var reason = new ExceptionError(new("Hello"));

        // Act
        var result = reason.ToString();

        // Assert
        var verify = await Verify(new
        {
            Input = reason,
            Result = result,
        });

        await TestContext.Out.WriteLineAsync(verify.Text);
    }

    [Test]
    public async Task ToString_Prints_CustomException_Correctly()
    {
        // Arrange
        var reason = new ExceptionError(new InvalidOperationException("Oh no"));

        // Act
        var result = reason.ToString();

        // Assert
        var verify = await Verify(new
        {
            Input = reason,
            Result = result,
        });

        await TestContext.Out.WriteLineAsync(verify.Text);
    }

    [Test]
    public async Task ToString_Prints_ExceptionWithData_Correctly()
    {
        // Arrange
        var reason = new ExceptionError(new("Hello"))
        {
            Data = new Dictionary<string, object>
            {
                ["Key"] = "Value",
            },
        };

        // Act
        var result = reason.ToString();

        // Assert
        var verify = await Verify(new
        {
            Input = reason,
            Result = result,
        });

        await TestContext.Out.WriteLineAsync(verify.Text);
    }
}
