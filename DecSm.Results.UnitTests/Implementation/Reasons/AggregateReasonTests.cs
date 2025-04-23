namespace DecSm.Results.UnitTests.Implementation.Reasons;

internal sealed class AggregateReasonTests
{
    [Test]
    public void AggregateReason_CanBeCreated()
    {
        // Arrange
        var reasons = new List<IReason>
        {
            new Success("Reason1"),
            new Error("Reason2"),
        };

        // Act
        var result = new AggregateReason(reasons);

        // Assert
        result.ShouldSatisfyAllConditions(() => result.ShouldNotBeNull(),
            () => result.Reasons.Length.ShouldBe(2),
            () => result.Message.ShouldBe("2 reasons"),
            () => result.Data.ShouldBeEmpty());
    }

    [Test]
    public async Task ToString_Prints_Empty_Correctly()
    {
        // Arrange
        var reason = new AggregateReason();

        // Act
        var result = reason.ToString();

        // Assert
        await Verify(new
        {
            Input = reason,
            Result = result,
        });
    }

    [Test]
    public async Task ToString_Prints_Empty_WithData_Correctly()
    {
        // Arrange
        var reason = new AggregateReason
        {
            Data = new Dictionary<string, object>
            {
                ["Key"] = "Value",
            },
        };

        // Act
        var result = reason.ToString();

        // Assert
        await Verify(new
        {
            Input = reason,
            Result = result,
        });
    }

    [Test]
    public async Task ToString_Prints_SingleReason_Correctly()
    {
        // Arrange
        var reason = new AggregateReason(new List<IReason>
        {
            new Error("Reason1"),
        });

        // Act
        var result = reason.ToString();

        // Assert
        await Verify(new
        {
            Input = reason,
            Result = result,
        });
    }

    [Test]
    public async Task ToString_Prints_SingleReason_WithData_Correctly()
    {
        // Arrange
        var reason = new AggregateReason(new List<IReason>
        {
            new Error("Reason1"),
        })
        {
            Data = new Dictionary<string, object>
            {
                ["Key"] = "Value",
            },
        };

        // Act
        var result = reason.ToString();

        // Assert
        await Verify(new
        {
            Input = reason,
            Result = result,
        });
    }

    [Test]
    public async Task ToString_Prints_MultipleReasons_Correctly()
    {
        // Arrange
        var reason = new AggregateReason(new List<IReason>
        {
            new Error("Reason1"),
            new Error("Reason2"),
        });

        // Act
        var result = reason.ToString();

        // Assert
        await Verify(new
        {
            Input = reason,
            Result = result,
        });
    }

    [Test]
    public async Task ToString_Prints_MultipleReasons_WithData_Correctly()
    {
        // Arrange
        var reason = new AggregateReason(new List<IReason>
        {
            new Error("Reason1"),
            new Error("Reason2"),
        })
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
    public async Task ToString_Prints_NestedReasons_Correctly()
    {
        // Arrange
        var reason = new AggregateReason(new List<IReason>
        {
            new Error("Reason1"),
            new AggregateReason(new List<IReason>
            {
                new Error("Reason2"),
            }),
        });

        // Act
        var result = reason.ToString();

        // Assert
        await Verify(new
        {
            Input = reason,
            Result = result,
        });
    }

    [Test]
    public void IsError_Returns_True_When_AnyReason_Is_Error()
    {
        // Arrange
        var reason = new AggregateReason(new List<IReason>
        {
            new Error("Reason1"),
            new Success("Reason2"),
        });

        // Act
        var result = reason.IsError;

        // Assert
        result.ShouldBeTrue();
    }

    [Test]
    public void IsError_Returns_True_When_AnyReason_Is_AggregateReason_With_Error()
    {
        // Arrange
        var reason = new AggregateReason(new List<IReason>
        {
            new Error("Reason1"),
            new AggregateReason(new List<IReason>
            {
                new Error("Reason2"),
            }),
        });

        // Act
        var result = reason.IsError;

        // Assert
        result.ShouldBeTrue();
    }

    [Test]
    public void IsError_Returns_False_When_NoReason_Is_Error()
    {
        // Arrange
        var reason = new AggregateReason(new List<IReason>
        {
            new Success("Reason1"),
            new Success("Reason2"),
        });

        // Act
        var result = reason.IsError;

        // Assert
        result.ShouldBeFalse();
    }

    [Test]
    public void IsError_Returns_False_When_NoReason_Is_AggregateReason_With_Error()
    {
        // Arrange
        var reason = new AggregateReason(new List<IReason>
        {
            new Success("Reason1"),
            new AggregateReason(new List<IReason>
            {
                new Success("Reason2"),
            }),
        });

        // Act
        var result = reason.IsError;

        // Assert
        result.ShouldBeFalse();
    }

    [Test]
    public void IsError_Returns_False_When_NoReasons()
    {
        // Arrange
        var reason = new AggregateReason();

        // Act
        var result = reason.IsError;

        // Assert
        result.ShouldBeFalse();
    }
}
