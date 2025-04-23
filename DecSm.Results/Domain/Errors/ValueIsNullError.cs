namespace DecSm.Results.Domain.Errors;

[PublicAPI]
public record ValueIsNullError : Error, IMaskableReason
{
    public ValueIsNullError() : base("A value is null") { }

    public ValueIsNullError(string message) : base(message) { }

    public ValueIsNullError(IError causedBy) : base("A value is null", causedBy) { }

    public ValueIsNullError(string message, IError causedBy) : base(message, causedBy) { }

    public new static ValueIsNullError Default { get; } = new();

    [Pure]
    public IReason Mask() =>
        Error.Default;
}
