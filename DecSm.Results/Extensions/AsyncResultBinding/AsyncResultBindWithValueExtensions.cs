// ReSharper disable once CheckNamespace - Extensions live in this namespace

namespace DecSm.Results.Extensions;

[PublicAPI]
public static class AsyncResultBindWithValueExtensions
{
    [Pure]
    public static async Task<Result<T>> BindToResult<T>(
        this Task<Result> result,
        Func<T> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        (await Result
            .FromResult(result, exceptionHandler)
            .ConfigureAwait(false)).BindToResult(bind, exceptionHandler);

    [Pure]
    public static async Task<Result<T>> BindToResult<T>(
        this Task<Result> result,
        Func<Task<T>> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        await (await Result
            .FromResult(result, exceptionHandler)
            .ConfigureAwait(false)).BindToResult(bind, exceptionHandler);

    // - - - - -

    [Pure]
    public static async Task<Result<T>> BindResult<T>(
        this Task<Result> result,
        Func<Result<T>> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        (await Result
            .FromResult(result, exceptionHandler)
            .ConfigureAwait(false)).BindResult(bind, exceptionHandler);

    [Pure]
    public static async Task<Result<T>> BindResult<T>(
        this Task<Result> result,
        Func<Task<Result<T>>> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        await (await Result
            .FromResult(result, exceptionHandler)
            .ConfigureAwait(false)).BindResult(bind, exceptionHandler);
}
