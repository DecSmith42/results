namespace DecSm.Results.Extensions;

[PublicAPI]
public static class AggregateReasonExtensions
{
    [Pure]
    public static bool HasError<TError>(this AggregateReason instance)
        where TError : IError =>
        instance.Reasons.Any(x => x is TError || (x is AggregateReason aggregateReason && aggregateReason.HasError<TError>()));

    [Pure]
    public static IEnumerable<TError>? GetErrors<TError>(this AggregateReason instance)
        where TError : IError
    {
        if (instance.Reasons.Length is 0)
            return null;

        var errorList = new List<TError>();

        foreach (var reason in instance.Reasons)
            switch (reason)
            {
                case TError error:
                    errorList.Add(error);

                    break;
                case AggregateReason aggregateReason when aggregateReason.GetErrors<TError>() is { } aggregateErrors:
                    errorList.AddRange(aggregateErrors);

                    break;
            }

        return errorList.Count > 0
            ? errorList
            : null;
    }

    [Pure]
    public static bool HasError(this AggregateReason instance, Func<IError, bool> predicate) =>
        instance.Reasons.Any(x =>
            (x is IError error && predicate(error)) || (x is AggregateReason aggregateReason && aggregateReason.HasError(predicate)));

    [Pure]
    public static IEnumerable<IError>? GetErrors(this AggregateReason instance, Func<IError, bool> predicate)
    {
        if (instance.Reasons.Length is 0)
            return null;

        var errorList = new List<IError>();

        foreach (var reason in instance.Reasons)
            switch (reason)
            {
                case IError error when predicate(error):
                    errorList.Add(error);

                    break;
                case AggregateReason aggregateReason when aggregateReason.GetErrors(predicate) is { } aggregateErrors:
                    errorList.AddRange(aggregateErrors);

                    break;
            }

        return errorList.Count > 0
            ? errorList
            : null;
    }
}
