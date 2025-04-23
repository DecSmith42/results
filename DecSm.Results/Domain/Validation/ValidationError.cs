namespace DecSm.Results.Domain.Validation;

[PublicAPI]
public sealed record ValidationError : Error, IValidationError
{
    public ValidationError() { }

    public ValidationError(string message) : base(message) { }

    public ValidationError(IReason cause) : base(cause) { }

    public ValidationError(string message, IReason cause) : base(message, cause) { }
}
