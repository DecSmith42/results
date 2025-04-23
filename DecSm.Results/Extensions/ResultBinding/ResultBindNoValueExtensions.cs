// ReSharper disable once CheckNamespace - Extensions live in this namespace

namespace DecSm.Results.Extensions;

[PublicAPI]
public static class ResultBindNoValueExtensions
{
    [Pure]
    public static Result BindToResult(this Result result, Action bind, Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? result
            : Result.From(bind, exceptionHandler);

    [Pure]
    public static async Task<Result> BindToResult(this Result result, Func<Task> bind, Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? result
            : await Result
                .From(bind, exceptionHandler)
                .ConfigureAwait(false);

    // - - - - -

    [Pure]
    public static Result BindResult(this Result result, Func<Result> bind, Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? result
            : Result.FromResult(bind, exceptionHandler);

    [Pure]
    public static async Task<Result> BindResult(
        this Result result,
        Func<Task<Result>> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? result
            : await Result
                .FromResult(bind, exceptionHandler)
                .ConfigureAwait(false);
}
