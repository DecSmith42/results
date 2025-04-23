namespace DecSm.Results.Implementation.Reasons;

[PublicAPI]
public record Error : Reason, IError
{
    public Error() { }

    public Error(string message) : base(message) { }

    public Error(IReason cause) : base(cause) { }

    public Error(string message, IReason cause) : base(message, cause) { }

    public static Error Default { get; } = new();
}
