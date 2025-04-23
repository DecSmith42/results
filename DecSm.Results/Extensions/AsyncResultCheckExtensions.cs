namespace DecSm.Results.Extensions;

[PublicAPI]
public static class AsyncResultCheckExtensions
{
    [Pure]
    public static async Task<bool> HasError<TError>(this Task<ResultBase> result)
        where TError : IError =>
        (await result.ConfigureAwait(false)).HasError<TError>();

    [Pure]
    public static async Task<IEnumerable<TError>?> GetErrors<TError>(this Task<ResultBase> result)
        where TError : IError =>
        (await result.ConfigureAwait(false)).GetErrors<TError>();

    [Pure]
    public static async Task<bool> HasError(this Task<ResultBase> result, Func<IError, bool> predicate) =>
        (await result.ConfigureAwait(false)).HasError(predicate);

    [Pure]
    public static async Task<IEnumerable<IError>?> GetErrors(this Task<ResultBase> result, Func<IError, bool> predicate) =>
        (await result.ConfigureAwait(false)).GetErrors(predicate);
}
