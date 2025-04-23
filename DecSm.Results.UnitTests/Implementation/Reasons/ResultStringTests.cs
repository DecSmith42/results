namespace DecSm.Results.UnitTests.Implementation.Reasons;

internal sealed class ResultToStringTests
{
    [Test]
    public void Result_Ok_ToString()
    {
        var result = Result.Ok();

        var text = result.ToString();

        text.ShouldBe("Result: Ok");
    }

    [Test]
    public void Result_Success_ToString()
    {
        var result = Result.Success(new Success("Test"));

        var text = result.ToString();

        text.ShouldBe("Result: Success, Reason=[Success: 'Test']");
    }

    [Test]
    public void Result_Failure_ToString()
    {
        var result = Result.Failure(new Error("Test"));

        var text = result.ToString();

        text.ShouldBe("Result: Failure, Reason=[Error: 'Test']");
    }

    [Test]
    public void Result_Failure_WithCause_ToString()
    {
        var result = Result.Failure(new Error("Test", new Error("Inner")));

        var text = result.ToString();

        text.ShouldBe("Result: Failure, Reason=[Error: 'Test', Cause=[Error: 'Inner']]");
    }

    [Test]
    public void Result_Failure_WithCause_WithData_ToString()
    {
        var result = Result.Failure(new Error("Test", new Error("Inner"))
        {
            Data = new Dictionary<string, object>
            {
                { "Key", "Value" },
            },
        });

        var text = result.ToString();

        text.ShouldBe("Result: Failure, Reason=[Error: 'Test', Data=[Key=Value], Cause=[Error: 'Inner']]");
    }

    [Test]
    public void Result_Failure_WithData_ToString()
    {
        var result = Result.Failure(new Error("Test")
        {
            Data = new Dictionary<string, object>
            {
                { "Key", "Value" },
            },
        });

        var text = result.ToString();

        text.ShouldBe("Result: Failure, Reason=[Error: 'Test', Data=[Key=Value]]");
    }

    [Test]
    public void Result_AggregateReason_ToString()
    {
        var result = Result.Create(new AggregateReason([new Error("Test1"), new Error("Test2")]));

        var text = result.ToString();

        text.ShouldBe("Result: Failure, Reason=[AggregateReason: '2 reasons', Reasons=[Error: 'Test1', Error: 'Test2']]");
    }

    [Test]
    public void Result_AggregateReason_WitMetadata_ToString()
    {
        var result = Result.Create(new AggregateReason([new Error("Test1"), new Error("Test2")])
        {
            Data = new Dictionary<string, object>
            {
                { "Key", "Value" },
            },
        });

        var text = result.ToString();

        text.ShouldBe("Result: Failure, Reason=[AggregateReason: '2 reasons', Data=[Key=Value], Reasons=[Error: 'Test1', Error: 'Test2']]");
    }
}
