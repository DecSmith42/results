namespace DecSm.Results.Extensions;

[PublicAPI]
public static class AsyncResultExtensions
{
    [Pure]
    public static async Task<Result> WithoutValue<T>(this Task<Result<T>> result) =>
        (await Result.FromResult(result)).WithoutValue();
}
