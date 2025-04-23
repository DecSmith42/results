namespace DecSm.Results.Extensions;

[PublicAPI]
public static class ResultExtensions
{
    [Pure]
    public static TResult WithReason<TResult>(this TResult result, IReason reason)
        where TResult : ResultBase =>
        result.Reason switch
        {
            AggregateReason ar => result with
            {
                Reason = new AggregateReason(ar.Reasons.Concat([reason])),
            },
            not null => result with
            {
                Reason = new AggregateReason([result.Reason, reason]),
            },
            _ => result with
            {
                Reason = reason,
            },
        };

    [Pure]
    public static TResult WithReasons<TResult>(this TResult result, IEnumerable<IReason> reasons)
        where TResult : ResultBase =>
        result.Reason switch
        {
            AggregateReason ar => result with
            {
                Reason = new AggregateReason(ar.Reasons.Concat(reasons)),
            },
            not null => result with
            {
                Reason = new AggregateReason(reasons.Concat([result.Reason])),
            },
            _ => result with
            {
                Reason = new AggregateReason(reasons),
            },
        };

    [Pure]
    public static TResult WithError<TResult>(this TResult result, IError error)
        where TResult : ResultBase =>
        WithReason(result, error);

    [Pure]
    public static TResult WithErrors<TResult>(this TResult result, IEnumerable<IError> errors)
        where TResult : ResultBase =>
        WithReasons(result, errors);

    [Pure]
    public static TResult WithSuccess<TResult>(this TResult result, ISuccess success)
        where TResult : ResultBase =>
        WithReason(result, success);

    [Pure]
    public static TResult WithSuccesses<TResult>(this TResult result, IEnumerable<ISuccess> successes)
        where TResult : ResultBase =>
        WithReasons(result, successes);

    [Pure]
    public static Result WithoutValue<T>(this Result<T> result) =>
        new()
        {
            Reason = result.Reason,
        };
}
