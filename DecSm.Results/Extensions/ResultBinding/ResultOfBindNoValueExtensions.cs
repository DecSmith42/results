// ReSharper disable once CheckNamespace - Extensions live in this namespace

namespace DecSm.Results.Extensions;

[PublicAPI]
public static class ResultOfBindNoValueExtensions
{
    [Pure]
    public static Result BindToResult<T>(this Result<T> result, Action<T> bind, Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? new()
            {
                Reason = result.Reason,
            }
            : Result.From(() => bind(result.Value), exceptionHandler);

    [Pure]
    public static async Task<Result> BindToResult<T>(
        this Result<T> result,
        Func<Task> bind,
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
    public static Result BindResult<T>(this Result<T> result, Func<T, Result> bind, Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? new()
            {
                Reason = result.Reason,
            }
            : Result.FromResult(() => bind(result.Value), exceptionHandler);

    [Pure]
    public static async Task<Result> BindToResult<T>(
        this Result<T> result,
        Func<T, Task> bind,
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
    public static Result BindToResult<T>(this Result<T> result, Action bind, Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? new()
            {
                Reason = result.Reason,
            }
            : Result.From(bind, exceptionHandler);

    [Pure]
    public static async Task<Result> BindResult<T>(
        this Result<T> result,
        Func<Task<Result>> bind,
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
    public static Result BindResult<T>(this Result<T> result, Func<Result> bind, Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? new()
            {
                Reason = result.Reason,
            }
            : Result.FromResult(bind, exceptionHandler);

    [Pure]
    public static async Task<Result> BindResult<T>(
        this Result<T> result,
        Func<T, Task<Result>> bind,
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
