namespace DecSm.Results.Extensions;

[PublicAPI]
public static class ReasonCollectionExtensions
{
    [Pure]
    public static IReason AggregateReasons(this ImmutableArray<IReason> reasons) =>
        reasons.Length > 1
            ? new AggregateReason(reasons)
            : reasons[0];

    [Pure]
    public static IReason AggregateReasons(this IEnumerable<IReason> reasons) =>
        reasons
            .ToImmutableArray()
            .AggregateReasons();
}
