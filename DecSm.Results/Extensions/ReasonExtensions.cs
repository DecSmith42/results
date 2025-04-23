namespace DecSm.Results.Extensions;

[PublicAPI]
public static class ReasonExtensions
{
    [Pure]
    public static T WithData<T>(this T reason, string metadataName, object metadataValue)
        where T : ReasonBase =>
        reason with
        {
            Data = reason.Data.Add(metadataName, metadataValue),
        };

    [Pure]
    public static T WithData<T>(this T reason, KeyValuePair<string, object> metadata)
        where T : ReasonBase =>
        reason with
        {
            Data = reason.Data.Add(metadata.Key, metadata.Value),
        };

    [Pure]
    public static T WithData<T>(this T reason, params KeyValuePair<string, object>[] metadata)
        where T : ReasonBase =>
        reason with
        {
            Data = reason.Data.AddRange(metadata),
        };

    [Pure]
    public static T WithData<T>(this T reason, Dictionary<string, object> metadata)
        where T : ReasonBase =>
        reason with
        {
            Data = reason.Data.AddRange(metadata),
        };

    [Pure]
    public static IReason Trim<T>(this T reason)
        where T : IReason =>
        reason.Trim(out _);

    [Pure]
    internal static IReason Trim<T>(this T reason, out bool isModified)
        where T : IReason
    {
        switch (reason)
        {
            case Reason { Cause: null }:

                isModified = false;

                return reason;

            case Reason standardReason:

                var trimmedCause = standardReason.Cause.Trim(out var causeIsModified);

                if (causeIsModified && standardReason.Data.Count is 0)
                {
                    isModified = true;

                    return standardReason with
                    {
                        Cause = trimmedCause,
                    };
                }

                isModified = false;

                return standardReason;

            case AggregateReason aggregateReason:

                switch (aggregateReason.Reasons.Length)
                {
                    // ReSharper disable once SuspiciousTypeConversion.Global
                    // Allow for the possibility of an inherited class that implements IError
                    case 0 when aggregateReason.Data.Count is 0:

                        isModified = true;

                        return aggregateReason is IError
                            ? Error.Default
                            : Success.Default;

                    case 1 when aggregateReason.Data.Count is 0:

                        isModified = true;

                        return aggregateReason.Reasons[0];

                    case 0 or 1:

                        isModified = false;

                        return aggregateReason;

                    default:

                        var trimmedReasonsModified = false;
                        var trimmedReasons = new List<IReason>(aggregateReason.Reasons.Length);

                        // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
                        // Out parameter does not work with LINQ
                        foreach (var subReason in aggregateReason.Reasons)
                            trimmedReasons.Add(subReason.Trim(out trimmedReasonsModified));

                        if (trimmedReasonsModified)
                        {
                            isModified = true;

                            return aggregateReason with
                            {
                                // PERF: ToImmutableArray is faster than CollectionExpression
                                // ReSharper disable once UseCollectionExpression
                                Reasons = trimmedReasons.ToImmutableArray(),
                            };
                        }

                        isModified = false;

                        return aggregateReason;
                }

            default:

                isModified = false;

                return reason;
        }
    }
}
