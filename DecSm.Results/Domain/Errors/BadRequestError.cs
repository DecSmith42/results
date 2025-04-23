namespace DecSm.Results.Domain.Errors;

[PublicAPI]
public record BadRequestError : Error
{
    public BadRequestError() : base("Bad request") { }

    public BadRequestError(string message) : base(message) { }

    public BadRequestError(IError causedBy) : base("Bad request", causedBy) { }

    public BadRequestError(string message, IError causedBy) : base(message, causedBy) { }

    public new static BadRequestError Default { get; } = new();
}
