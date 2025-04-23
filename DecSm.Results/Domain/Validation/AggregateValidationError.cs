namespace DecSm.Results.Domain.Validation;

[PublicAPI]
public sealed record AggregateValidationError : AggregateReason, IValidationError
{
    public AggregateValidationError() { }

    public AggregateValidationError(IEnumerable<IReason> reasons)
    {
        // PERF: ToImmutableArray is faster than CollectionExpression
        // ReSharper disable once UseCollectionExpression
        Reasons = reasons.ToImmutableArray();
    }

    public AggregateValidationError(ImmutableArray<IReason> reasons)
    {
        Reasons = reasons;
    }
}
