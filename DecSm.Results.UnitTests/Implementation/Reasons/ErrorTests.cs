namespace DecSm.Results.UnitTests.Implementation.Reasons;

public class ErrorTests
{
    [Test]
    public void Error_CanBeCreated()
    {
        // Arrange
        const string reason = "Error";

        // Act
        var result = new Error(reason);

        // Assert
        result
            .ShouldNotBeNull()
            .ShouldSatisfyAllConditions(x => x.Message.ShouldBe(reason), x => x.Cause.ShouldBeNull(), x => x.Data.ShouldBeEmpty());
    }

    [Test]
    public void Error_CanBeCreatedWithCause()
    {
        // Arrange
        const string reason = "Error";
        var cause = new Error("Inner");

        // Act
        var result = new Error(reason, cause);

        // Assert
        result
            .ShouldNotBeNull()
            .ShouldSatisfyAllConditions(x => x.Message.ShouldBe(reason), x => x.Cause.ShouldBe(cause), x => x.Data.ShouldBeEmpty());
    }

    [Test]
    public async Task ToString_Prints_Empty_Correctly()
    {
        // Arrange
        var reason = new Error();

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
    public async Task ToString_Prints_WithMessage_Correctly()
    {
        // Arrange
        var reason = new Error("Test");

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
    public async Task ToString_Prints_Data_Correctly()
    {
        // Arrange
        var reason = new Error
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

    [Test]
    public async Task ToString_Prints_Cause_Correctly()
    {
        // Arrange
        var reason = new Error(new Error("Reason1"));

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
    public async Task ToString_Prints_All_Correctly()
    {
        // Arrange
        var reason = new Error("Test", new Error("Reason1"))
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
