// ReSharper disable once CheckNamespace - Extensions live in this namespace

namespace DecSm.Results.Extensions;

[PublicAPI]
public static class AsyncResultOfBindWithValueExtensions
{
    [Pure]
    public static async Task<Result<TNew>> BindToResult<T, TNew>(
        this Task<Result<T>> result,
        Func<TNew> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        (await result.ConfigureAwait(false)).BindToResult(bind, exceptionHandler);

    [Pure]
    public static async Task<Result<TNew>> BindToResult<T, TNew>(
        this Task<Result<T>> result,
        Func<Task<TNew>> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        await (await result.ConfigureAwait(false)).BindToResult(bind, exceptionHandler);

    // - - - - -

    [Pure]
    public static async Task<Result<TNew>> BindToResult<T, TNew>(
        this Task<Result<T>> result,
        Func<T, TNew> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        (await result.ConfigureAwait(false)).BindToResult(bind, exceptionHandler);

    [Pure]
    public static async Task<Result<TNew>> BindToResult<T, TNew>(
        this Task<Result<T>> result,
        Func<T, Task<TNew>> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        await (await result.ConfigureAwait(false)).BindToResult(bind, exceptionHandler);

    // - - - - -

    [Pure]
    public static async Task<Result<TNew>> BindResult<T, TNew>(
        this Task<Result<T>> result,
        Func<Result<TNew>> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        (await result.ConfigureAwait(false)).BindResult(bind, exceptionHandler);

    [Pure]
    public static async Task<Result<TNew>> BindResult<T, TNew>(
        this Task<Result<T>> result,
        Func<Task<Result<TNew>>> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        await (await result.ConfigureAwait(false)).BindResult(bind, exceptionHandler);

    // - - - - -

    [Pure]
    public static async Task<Result<TNew>> BindResult<T, TNew>(
        this Task<Result<T>> result,
        Func<T, Result<TNew>> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        (await result.ConfigureAwait(false)).BindResult(bind, exceptionHandler);

    [Pure]
    public static async Task<Result<TNew>> BindResult<T, TNew>(
        this Task<Result<T>> result,
        Func<T, Task<Result<TNew>>> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        await (await result.ConfigureAwait(false)).BindResult(bind, exceptionHandler);
}
