namespace DecSm.Results.UnitTests.Implementation.Reasons;

internal sealed class SuccessTests
{
    [Test]
    public void Success_CanBeCreated()
    {
        // Arrange
        const string reason = "Success";

        // Act
        var result = new Success(reason);

        // Assert
        result
            .ShouldNotBeNull()
            .ShouldSatisfyAllConditions(x => x.Message.ShouldBe(reason), x => x.Cause.ShouldBeNull(), x => x.Data.ShouldBeEmpty());
    }

    [Test]
    public void Success_CanBeCreatedWithCause()
    {
        // Arrange
        const string reason = "Success";
        var cause = new Success("Inner");

        // Act
        var result = new Success(reason, cause);

        // Assert
        result
            .ShouldNotBeNull()
            .ShouldSatisfyAllConditions(x => x.Message.ShouldBe(reason), x => x.Cause.ShouldBe(cause), x => x.Data.ShouldBeEmpty());
    }

    [Test]
    public void SuccessWithData_CanBeCreated()
    {
        // Arrange
        const string reason = "Success";

        var data = new Dictionary<string, object>
        {
            { "Key", "Value" },
        };

        // Act
        var result = new Success(reason)
        {
            Data = data,
        };

        // Assert
        result
            .ShouldNotBeNull()
            .ShouldSatisfyAllConditions(x => x.Message.ShouldBe(reason),
                x => x.Cause.ShouldBeNull(),
                x => x.Data.ShouldBeEquivalentTo(data));
    }

    [Test]
    public async Task ToString_Prints_Empty_Correctly()
    {
        // Arrange
        var reason = new Success();

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
        var reason = new Success("Test");

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
        var reason = new Success
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
        var reason = new Success(new Success("Reason1"));

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
        var reason = new Success("Test", new Success("Reason1"))
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
