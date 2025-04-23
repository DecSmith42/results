namespace DecSm.Results.UnitTests.Serialization;

[SuppressMessage("ReSharper", "RedundantExplicitParamsArrayCreation")]
internal sealed class ResultConverterTests
{
    [Test]
    public void Serialize_Deserialize_Result_With_Ok()
    {
        var options = new JsonSerializerOptions
        {
            Converters =
            {
                new ResultConverter(),
            },
        };

        var result = Result.Ok();

        var json = JsonSerializer.Serialize(result, options);

        var deserializedResult = JsonSerializer.Deserialize<Result>(json, options);

        deserializedResult.ShouldSatisfyAllConditions(x => x.ShouldNotBeNull(), x => x!.Reason.ShouldBeNull());
    }

    [Test]
    public void Serialize_Deserialize_ResultOf_With_Ok()
    {
        var options = new JsonSerializerOptions
        {
            Converters =
            {
                new ResultOfConverterFactory(),
            },
        };

        var result = Result.Ok("Hello");

        var json = JsonSerializer.Serialize(result, options);

        var deserializedResult = JsonSerializer.Deserialize<Result<string>>(json, options);

        deserializedResult.ShouldSatisfyAllConditions(x => x.ShouldNotBeNull(),
            x => x!.Value.ShouldBe("Hello"),
            x => x!.Reason.ShouldBeNull());
    }

    [Test]
    public void Serialize_Deserialize_ResultOf_With_Ok_Complex()
    {
        var options = new JsonSerializerOptions
        {
            Converters =
            {
                new ResultOfConverterFactory(),
            },
        };

        var result = Result
            .Ok("Hello")
            .WithSuccess(new Success("Yay").WithData("Key", "Value"))
            .WithSuccess(new Success("Yay2").WithData("Key2", "Value2"));

        var json = JsonSerializer.Serialize(result, options);

        var deserializedResult = JsonSerializer.Deserialize<Result<string>>(json, options);

        deserializedResult.ShouldSatisfyAllConditions([
            () => deserializedResult.ShouldNotBeNull(),
            () => deserializedResult!.Value.ShouldBe("Hello"),
            () => deserializedResult!.Reason.ShouldNotBeNull(),
            () => deserializedResult!
                .Reason
                .ShouldBeOfType<AggregateReason>()
                .ShouldSatisfyAllConditions([
                    () => ((AggregateReason)deserializedResult.Reason).Reasons.Length.ShouldBe(2),
                    () => ((AggregateReason)deserializedResult.Reason)
                        .Reasons[0]
                        .ShouldBeOfType<Success>()
                        .ShouldSatisfyAllConditions([
                            () => ((Success)((AggregateReason)deserializedResult.Reason).Reasons[0]).Message.ShouldBe("Yay"),
                            () => ((Success)((AggregateReason)deserializedResult.Reason).Reasons[0]).Data.Count.ShouldBe(1),
                            () => ((Success)((AggregateReason)deserializedResult.Reason).Reasons[0])
                                .Data["Key"]
                                .ShouldBe("Value"),
                        ]),
                    () => ((AggregateReason)deserializedResult.Reason)
                        .Reasons[1]
                        .ShouldBeOfType<Success>()
                        .ShouldSatisfyAllConditions([
                            () => ((Success)((AggregateReason)deserializedResult.Reason).Reasons[1]).Message.ShouldBe("Yay2"),
                            () => ((Success)((AggregateReason)deserializedResult.Reason).Reasons[1]).Data.Count.ShouldBe(1),
                            () => ((Success)((AggregateReason)deserializedResult.Reason).Reasons[1])
                                .Data["Key2"]
                                .ShouldBe("Value2"),
                        ]),
                ]),
        ]);
    }

    [Test]
    public void Serialize_Deserialize_Result_With_Success()
    {
        var options = new JsonSerializerOptions
        {
            Converters =
            {
                new ResultConverter(),
            },
        };

        var result = Result
            .Ok()
            .WithSuccess(new Success("Success"));

        var json = JsonSerializer.Serialize(result, options);

        var deserializedResult = JsonSerializer.Deserialize<Result>(json, options);

        deserializedResult.ShouldSatisfyAllConditions([
            () => deserializedResult.ShouldNotBeNull(),
            () => deserializedResult!
                .Reason
                .ShouldBeOfType<Success>()
                .ShouldSatisfyAllConditions([
                    () => deserializedResult.Reason.Message.ShouldBe("Success"), () => deserializedResult.Reason.Data.ShouldBeEmpty(),
                ]),
        ]);
    }

    [Test]
    public void Serialize_Deserialize_Result_With_Success_With_Metadata()
    {
        var options = new JsonSerializerOptions
        {
            Converters =
            {
                new ResultConverter(),
            },
        };

        var result = Result
            .Ok()
            .WithSuccess(new Success("Success").WithData("Key", "Value"));

        var json = JsonSerializer.Serialize(result, options);

        var deserializedResult = JsonSerializer.Deserialize<Result>(json, options);

        deserializedResult.ShouldSatisfyAllConditions([
            () => deserializedResult.ShouldNotBeNull(),
            () => deserializedResult!.Reason.ShouldSatisfyAllConditions([
                () => deserializedResult
                    .Reason
                    .ShouldBeOfType<Success>()
                    .ShouldSatisfyAllConditions([
                        () => deserializedResult.Reason.Message.ShouldBe("Success"),
                        () => deserializedResult.Reason.Data.Count.ShouldBe(1),
                        () => deserializedResult
                            .Reason
                            .Data["Key"]
                            .ShouldBe("Value"),
                    ]),
            ]),
        ]);
    }

    [Test]
    public void Serialize_Deserialize_Result_With_Fail()
    {
        var options = new JsonSerializerOptions
        {
            Converters =
            {
                new ResultConverter(),
            },
        };

        var result = Result.Failure(new Error("Error"));

        var json = JsonSerializer.Serialize(result, options);

        var deserializedResult = JsonSerializer.Deserialize<Result>(json, options);

        deserializedResult.ShouldSatisfyAllConditions([
            () => deserializedResult.ShouldNotBeNull(),
            () => deserializedResult!
                .Reason
                .ShouldBeOfType<Error>()
                .ShouldSatisfyAllConditions([
                    () => deserializedResult.Reason.Message.ShouldBe("Error"), () => deserializedResult.Reason.Data.ShouldBeEmpty(),
                ]),
        ]);
    }

    [Test]
    public void Serialize_Deserialize_Result_With_Fail_With_Metadata()
    {
        var options = new JsonSerializerOptions
        {
            Converters =
            {
                new ResultConverter(),
            },
        };

        var result = Result.Failure(new Error("Error").WithData("Key", "Value"));

        var json = JsonSerializer.Serialize(result, options);

        var deserializedResult = JsonSerializer.Deserialize<Result>(json, options);

        deserializedResult.ShouldSatisfyAllConditions(x => x.ShouldNotBeNull(),
            x => x!
                .Reason
                .ShouldBeOfType<Error>()
                .ShouldSatisfyAllConditions(y => y.Message.ShouldBe("Error"),
                    y => y.Data.Count.ShouldBe(1),
                    y => y
                        .Data["Key"]
                        .ShouldBe("Value")));
    }

    [Test]
    public void Serialize_Deserialize_Result_With_ExceptionError()
    {
        var options = new JsonSerializerOptions
        {
            Converters =
            {
                new ResultConverter(),
            },
        };

        var exception = new Exception("CustomMessage");

        var result = Result.Failure(new ExceptionError(exception));

        var json = JsonSerializer.Serialize(result, options);

        var deserializedResult = JsonSerializer.Deserialize<Result>(json, options);

        deserializedResult.ShouldSatisfyAllConditions(x => x.ShouldNotBeNull(),
            x => x!
                .Reason
                .ShouldBeOfType<ExceptionError>()
                .ShouldSatisfyAllConditions(y => y.Message.ShouldBe(exception.Message)));
    }
}
