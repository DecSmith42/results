namespace DecSm.Results.Implementation.Reasons;

[PublicAPI]
public record AggregateReason : ReasonBase
{
    private ImmutableArray<IReason> _reasons = ImmutableArray<IReason>.Empty;

    public AggregateReason() { }

    public AggregateReason(IEnumerable<IReason> reasons)
    {
        // PERF: ToImmutableArray is faster than CollectionExpression
        // ReSharper disable once UseCollectionExpression
        _reasons = reasons.ToImmutableArray();
    }

    public AggregateReason(ImmutableArray<IReason> reasons)
    {
        _reasons = reasons;
    }

    [SuppressMessage("Roslynator", "RCS1085:Use auto-implemented property")]
    public ImmutableArray<IReason> Reasons
    {
        get => _reasons;
        protected internal init => _reasons = value;
    }

    public override string Message =>
        _reasons.Length is 1
            ? _reasons[0].Message
            : $"{_reasons.Length.ToString()} reasons";

    public bool IsError => _reasons.Any(x => x is IError or AggregateReason { IsError: true });

    [Pure]
    public sealed override string ToString()
    {
        var dataExceptReasons = Data.ToArray();

        var dataExceptReasonsString = dataExceptReasons.Length > 0
            ? $", Data=[{string.Join(", ", dataExceptReasons.Select(x => $"{x.Key}={x.Value}"))}]"
            : string.Empty;

        var reasonsString = _reasons.Length > 0
            ? $", Reasons=[{string.Join(", ", Reasons.Select(x => x.ToString()))}]"
            : string.Empty;

        return $"{GetType().Name}: '{Message}'{dataExceptReasonsString}{reasonsString}";
    }

    internal void SetState(string? message, List<KeyValuePair<string, object>>? data, List<IReason>? reasons)
    {
        base.SetState(message, data);

        // PERF: ToImmutableArray is faster than CollectionExpression
        // ReSharper disable once UseCollectionExpression
        if (reasons is not null)
            _reasons = reasons.ToImmutableArray();
    }
}
