// ReSharper disable once CheckNamespace - Extensions live in this namespace

namespace DecSm.Results.Extensions;

[PublicAPI]
public static class AsyncResultBindNoValueExtensions
{
    [Pure]
    public static async Task<Result> BindToResult(
        this Task<Result> result,
        Action bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        (await Result
            .FromResult(result, exceptionHandler)
            .ConfigureAwait(false)).BindToResult(bind, exceptionHandler);

    [Pure]
    public static async Task<Result> BindToResult(
        this Task<Result> result,
        Func<Task> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        await (await Result
            .FromResult(result, exceptionHandler)
            .ConfigureAwait(false)).BindToResult(bind, exceptionHandler);

    // - - - - -

    [Pure]
    public static async Task<Result> BindResult(
        this Task<Result> result,
        Func<Result> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        (await Result
            .FromResult(result, exceptionHandler)
            .ConfigureAwait(false)).BindResult(bind, exceptionHandler);

    [Pure]
    public static async Task<Result> BindResult(
        this Task<Result> result,
        Func<Task<Result>> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        await (await Result
            .FromResult(result, exceptionHandler)
            .ConfigureAwait(false)).BindResult(bind, exceptionHandler);
}
