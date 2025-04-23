namespace DecSm.Results.Extensions;

[PublicAPI]
public static class ResultCheckExtensions
{
    [Pure]
    public static bool HasError<TError>(this ResultBase result)
        where TError : IError =>
        result.Reason is TError || (result.Reason is AggregateReason aggregateReason && aggregateReason.HasError<TError>());

    [Pure]
    public static IEnumerable<TError>? GetErrors<TError>(this ResultBase result)
        where TError : IError =>
        result.Reason switch
        {
            null => null,
            TError error => [error],
            AggregateReason aggregateReason => aggregateReason.GetErrors<TError>(),
            _ => null,
        };

    [Pure]
    public static bool HasError(this ResultBase result, Func<IError, bool> predicate) =>
        (result.Reason is IError error && predicate(error)) ||
        (result.Reason is AggregateReason aggregateReason && aggregateReason.HasError(predicate));

    [Pure]
    public static IEnumerable<IError>? GetErrors(this ResultBase result, Func<IError, bool> predicate) =>
        result.Reason switch
        {
            null => null,
            IError error when predicate(error) => [error],
            AggregateReason aggregateReason => aggregateReason.GetErrors(predicate),
            _ => null,
        };
}
