// ReSharper disable once CheckNamespace - Extensions live in this namespace

namespace DecSm.Results.Extensions;

[PublicAPI]
public static class AsyncResultOfBindNoValueExtensions
{
    [Pure]
    public static async Task<Result> BindToResult<T>(
        this Task<Result<T>> result,
        Action bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        (await result.ConfigureAwait(false)).BindToResult(bind, exceptionHandler);

    [Pure]
    public static async Task<Result> BindToResult<T>(
        this Task<Result<T>> result,
        Func<Task> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        await (await Result
            .FromResult(result, exceptionHandler)
            .ConfigureAwait(false)).BindToResult(bind, exceptionHandler);

    // - - - - -

    [Pure]
    public static async Task<Result> BindToResult<T>(
        this Task<Result<T>> result,
        Action<T> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        (await result.ConfigureAwait(false)).BindToResult(bind, exceptionHandler);

    [Pure]
    public static async Task<Result> BindToResult<T>(
        this Task<Result<T>> result,
        Func<T, Task> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        await (await Result
            .FromResult(result, exceptionHandler)
            .ConfigureAwait(false)).BindToResult(bind, exceptionHandler);

    // - - - - -

    [Pure]
    public static async Task<Result> BindResult<T>(
        this Task<Result<T>> result,
        Func<Result> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        (await result.ConfigureAwait(false)).BindResult(bind, exceptionHandler);

    [Pure]
    public static async Task<Result> BindResult<T>(
        this Task<Result<T>> result,
        Func<Task<Result>> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        await (await Result
            .FromResult(result, exceptionHandler)
            .ConfigureAwait(false)).BindResult(bind, exceptionHandler);

    // - - - - -

    [Pure]
    public static async Task<Result> BindResult<T>(
        this Task<Result<T>> result,
        Func<T, Result> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        (await result.ConfigureAwait(false)).BindResult(bind, exceptionHandler);

    [Pure]
    public static async Task<Result> BindResult<T>(
        this Task<Result<T>> result,
        Func<T, Task<Result>> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        await (await Result
            .FromResult(result, exceptionHandler)
            .ConfigureAwait(false)).BindResult(bind, exceptionHandler);
}
