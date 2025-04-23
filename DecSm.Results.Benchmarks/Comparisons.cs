namespace DecSm.Results.Benchmarks;

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net48)]
[SimpleJob(RuntimeMoniker.Net90)]
[SuppressMessage("Performance", "CA1822:Mark members as static")]
public class Comparisons
{
    [Benchmark]
    public Result DecSm_Result_Ok() =>
        Result.Ok();

    [Benchmark]
    public FluentResults.Result Fluent_Result_Ok() =>
        FluentResults.Result.Ok();

    // - - - - -

    [Benchmark]
    public Result DecSm_Result_Fail_With_Error() =>
        Result.Failure(new Error("Fail"));

    [Benchmark]
    public FluentResults.Result Fluent_Result_Fail_With_Error() =>
        FluentResults.Result.Fail(new FluentResults.Error("Fail"));

    // - - - - -

    [Benchmark]
    public Result DecSm_Result_Fail_With_Errors() =>
        Result.Create([new Error("Fail 1"), new Error("Fail 2"), new Error("Fail 3")]);

    [Benchmark]
    public FluentResults.Result Fluent_Result_Fail_With_Errors() =>
        FluentResults.Result.Fail([
            new FluentResults.Error("Fail 1"), new FluentResults.Error("Fail 2"), new FluentResults.Error("Fail 3"),
        ]);

    // - - - - -

    [Benchmark]
    public Result DecSm_Result_Ok_Bind_Ok() =>
        Result
            .Ok()
            .BindResult(Result.Ok);

    [Benchmark]
    public FluentResults.Result Fluent_Result_Ok_Bind_Ok() =>
        FluentResults.Result
            .Ok()
            .Bind(FluentResults.Result.Ok);

    // - - - - -

    [Benchmark]
    public Result DecSm_Result_Ok_Bind_Fail() =>
        Result
            .Ok()
            .BindResult(() => Result.Failure(new Error("Fail")));

    [Benchmark]
    public FluentResults.Result Fluent_Result_Ok_Bind_Fail() =>
        FluentResults.Result
            .Ok()
            .Bind(() => FluentResults.Result.Fail(new FluentResults.Error("Fail")));

    // - - - - -

    [Benchmark]
    public Result DecSm_Complex_1() =>
        Result
            .Create([new Success("Success 1"), new Success("Success 2")])
            .BindResult(() =>
                Result.Create([new Success("Success 3"), new Success("Success 4"), new Error("Error 3"), new Error("Error 4")]));

    [Benchmark]
    public FluentResults.Result Fluent_Complex_1() =>
        FluentResults.Result
            .Ok()
            .WithSuccesses([new FluentResults.Success("Success 1"), new FluentResults.Success("Success 2")])
            .Bind(() => FluentResults.Result
                .Ok()
                .WithSuccesses([new FluentResults.Success("Success 3"), new FluentResults.Success("Success 4")])
                .WithErrors([new FluentResults.Error("Error 3"), new FluentResults.Error("Error 4")]));
}
