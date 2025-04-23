// ReSharper disable once CheckNamespace - Extensions live in this namespace

namespace DecSm.Results.Extensions;

[PublicAPI]
public static class ResultBindWithValueExtensions
{
    [Pure]
    public static Result<T> BindToResult<T>(this Result result, Func<T> bind, Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? new()
            {
                Reason = result.Reason,
            }
            : Result.From(bind, exceptionHandler);

    [Pure]
    public static async Task<Result<T>> BindToResult<T>(
        this Result result,
        Func<Task<T>> bind,
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
    public static Result<T> BindResult<T>(this Result result, Func<Result<T>> bind, Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? new()
            {
                Reason = result.Reason,
            }
            : Result.FromResult(bind, exceptionHandler);

    [Pure]
    public static async Task<Result<T>> BindResult<T>(
        this Result result,
        Func<Task<Result<T>>> bind,
        Func<Exception, IError>? exceptionHandler = null) =>
        result.IsFailed
            ? new()
            {
                Reason = result.Reason,
            }
            : await Result
                .FromResult(bind, exceptionHandler)
                .ConfigureAwait(false);
}
