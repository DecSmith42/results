namespace DecSm.Results.Implementation.Results;

[PublicAPI]
public abstract record ResultBase : IResult
{
    [JsonIgnore]
    public bool IsFailed => Reason is IError or AggregateReason { IsError: true };

    public IReason? Reason { get; init; }

    [Pure]
    public void Deconstruct(out bool isFailed, out IError? error)
    {
        isFailed = IsFailed;
        error = Reason as IError;
    }

    [Pure]
    public override string ToString()
    {
        var statusString = IsFailed
            ? "Failure"
            : Reason is not null
                ? "Success"
                : "Ok";

        var reasonsString = Reason is not null
            ? $", Reason=[{Reason.ToString()}]"
            : string.Empty;

        return $"Result: {statusString}{reasonsString}";
    }
}
