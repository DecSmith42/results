// ReSharper disable once CheckNamespace - Extensions live in this namespace

namespace DecSm.Results.Extensions;

[PublicAPI]
public static class ResultOfBindWithValueExtensions
{
    [Pure]
    public static Result<TNew> BindToResult<T, TNew>(
        this Result<T> result,
        Func<TNew> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? new()
            {
                Reason = result.Reason,
            }
            : Result.From(bind, exceptionHandler);

    [Pure]
    public static async Task<Result<TNew>> BindToResult<T, TNew>(
        this Result<T> result,
        Func<Task<TNew>> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? new()
            {
                Reason = result.Reason,
            }
            : await Result
                .From(bind, exceptionHandler)
                .ConfigureAwait(false);

    // - - - - -

    [Pure]
    public static Result<TNew> BindToResult<T, TNew>(
        this Result<T> result,
        Func<T, TNew> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? new()
            {
                Reason = result.Reason,
            }
            : Result.From(() => bind(result.Value), exceptionHandler);

    [Pure]
    public static async Task<Result<TNew>> BindToResult<T, TNew>(
        this Result<T> result,
        Func<T, Task<TNew>> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? new()
            {
                Reason = result.Reason,
            }
            : await Result
                .From(() => bind(result.Value), exceptionHandler)
                .ConfigureAwait(false);

    // - - - - -

    [Pure]
    public static Result<TNew> BindResult<T, TNew>(
        this Result<T> result,
        Func<Result<TNew>> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? new()
            {
                Reason = result.Reason,
            }
            : Result.FromResult(bind, exceptionHandler);

    [Pure]
    public static async Task<Result<TNew>> BindResult<T, TNew>(
        this Result<T> result,
        Func<Task<Result<TNew>>> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? new()
            {
                Reason = result.Reason,
            }
            : await Result
                .FromResult(bind, exceptionHandler)
                .ConfigureAwait(false);

    // - - - - -

    [Pure]
    public static Result<TNew> BindResult<T, TNew>(
        this Result<T> result,
        Func<T, Result<TNew>> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? new()
            {
                Reason = result.Reason,
            }
            : Result.FromResult(() => bind(result.Value), exceptionHandler);

    [Pure]
    public static async Task<Result<TNew>> BindResult<T, TNew>(
        this Result<T> result,
        Func<T, Task<Result<TNew>>> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? new()
            {
                Reason = result.Reason,
            }
            : await Result
                .FromResult(() => bind(result.Value), exceptionHandler)
                .ConfigureAwait(false);
}
