namespace DecSm.Results.Domain;

[PublicAPI]
public static class DomainExtensions
{
    [Pure]
    public static Result MaskReasons(this Result result)
    {
        if (result.Reason is null)
            return result;

        var maskedReason = result.Reason.MaskReasons(out var isChanged);

        return isChanged
            ? new()
            {
                Reason = maskedReason,
            }
            : result;
    }

    [Pure]
    public static Result<T> MaskReasons<T>(this Result<T> result)
    {
        if (result.Reason is null)
            return result;

        var maskedReason = result.Reason.MaskReasons(out var isDirty);

        return isDirty
            ? new()
            {
                ValueOrDefault = result.Value,
                Reason = maskedReason,
            }
            : result;
    }

    [Pure]
    public static IReason MaskReasons(this IReason reason) =>
        reason.MaskReasons(out _);

    [Pure]
    private static IReason MaskReasons(this IReason reason, out bool isChanged)
    {
        if (reason is IMaskableReason maskableReason)
        {
            isChanged = true;

            return maskableReason.Mask();
        }

        isChanged = false;

        if (reason is AggregateReason aggregateReason)
        {
            var maskedReasons = new List<IReason>(aggregateReason.Reasons.Length);

            // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator - performance
            foreach (var r in aggregateReason.Reasons)
                maskedReasons.Add(r.MaskReasons(out isChanged));

            return isChanged
                ? aggregateReason with
                {
                    // PERF: ToImmutableArray is faster than CollectionExpression
                    // ReSharper disable once UseCollectionExpression
                    Reasons = maskedReasons.ToImmutableArray(),
                }
                : aggregateReason;
        }

        if (reason is not Reason standardReason)
            return reason;

        var maskedCause = standardReason.Cause?.MaskReasons(out isChanged);

        return (maskedCause, isDirty: isChanged) switch
        {
            (null, _) => reason,
            (_, false) => standardReason,
            (_, true) => standardReason with
            {
                Cause = maskedCause,
            },
        };
    }

    [Pure]
    public static Result MaskNonAssemblyReasons(this Result result, Assembly[]? includeAssemblies = null)
    {
        if (result.Reason is null)
            return result;

        includeAssemblies = includeAssemblies is null
            ? [typeof(IResult).Assembly]
            : includeAssemblies
                .Append(typeof(IResult).Assembly)
                .ToArray();

        var maskedReason = result.Reason.MaskNonAssemblyReasons(includeAssemblies, out var reasonIsChanged);

        return reasonIsChanged
            ? new()
            {
                Reason = maskedReason,
            }
            : result;
    }

    [Pure]
    public static Result<T> MaskNonAssemblyReasons<T>(this Result<T> result, Assembly[]? includeAssemblies = null)
    {
        if (result.Reason is null)
            return result;

        includeAssemblies = includeAssemblies is null
            ? [typeof(IResult).Assembly]
            : includeAssemblies
                .Append(typeof(IResult).Assembly)
                .ToArray();

        var maskedReason = result.Reason.MaskNonAssemblyReasons(includeAssemblies, out var reasonIsChanged);

        return reasonIsChanged
            ? new()
            {
                ValueOrDefault = result.Value,
                Reason = maskedReason,
            }
            : result;
    }

    [PublicAPI]
    [Pure]
    public static IReason MaskNonAssemblyReasons(this IReason reason, Assembly[]? includeAssemblies = null)
    {
        includeAssemblies = includeAssemblies is null
            ? [typeof(IResult).Assembly]
            : includeAssemblies
                .Append(typeof(IResult).Assembly)
                .ToArray();

        return reason.MaskNonAssemblyReasons(includeAssemblies, out _);
    }

    [Pure]
    private static IReason MaskNonAssemblyReasons(this IReason reason, Assembly[] includeAssemblies, out bool isChanged)
    {
        var isIncluded = includeAssemblies.Contains(reason.GetType()
            .Assembly);

        switch (reason)
        {
            case Reason standardReason:
            {
                if (isIncluded)
                {
                    if (standardReason.Cause is null)
                    {
                        isChanged = false;

                        return reason;
                    }

                    var maskedCause = standardReason.Cause.MaskNonAssemblyReasons(includeAssemblies, out isChanged);

                    return isChanged
                        ? standardReason with
                        {
                            Cause = maskedCause,
                        }
                        : reason;
                }

                if (standardReason.Cause is not null)
                    return new Error
                    {
                        Message = standardReason.Message,
                        Data = standardReason.Data,
                        Cause = standardReason.Cause.MaskNonAssemblyReasons(includeAssemblies, out isChanged),
                    };

                isChanged = true;

                return new Error
                {
                    Message = standardReason.Message,
                    Data = standardReason.Data,
                };
            }
            case AggregateReason aggregateReason:
            {
                var maskedReasons = new List<IReason>(aggregateReason.Reasons.Length);

                isChanged = !isIncluded;

                // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator - performance
                foreach (var r in aggregateReason.Reasons)
                    maskedReasons.Add(r.MaskNonAssemblyReasons(includeAssemblies, out isChanged));

                if (isIncluded)
                    return isChanged
                        ? aggregateReason with
                        {
                            // PERF: ToImmutableArray is faster than CollectionExpression
                            // ReSharper disable once UseCollectionExpression
                            Reasons = maskedReasons.ToImmutableArray(),
                        }
                        : reason;

                isChanged = true;

                // PERF: ToImmutableArray is faster than CollectionExpression
                // ReSharper disable once UseCollectionExpression
                return new AggregateReason(maskedReasons.ToImmutableArray());
            }
            default:
            {
                if (isIncluded)
                {
                    isChanged = false;

                    return reason;
                }

                isChanged = true;

                return new Error
                {
                    Message = reason.Message,
                    Data = reason.Data,
                };
            }
        }
    }

    [Pure]
    public static Result<T> EnforceNotNull<T>(this Result<T?> result)
        where T : class =>
        result.IsFailed
            ? result!
            : result.Value is null
                ? Result.Failure<T>(ValueIsNullError.Default)
                : result!;

    [Pure]
    public static async Task<Result<T>> EnforceNotNull<T>(this Task<Result<T?>> result)
        where T : class =>
        (await Result.FromResult(result)).EnforceNotNull();
}
