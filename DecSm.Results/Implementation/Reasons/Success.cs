namespace DecSm.Results.Implementation.Reasons;

[PublicAPI]
public record Success : Reason, ISuccess
{
    public Success() { }

    public Success(string message) : base(message) { }

    public Success(IReason cause) : base(cause) { }

    public Success(string message, IReason cause) : base(message, cause) { }

    public static Success Default { get; } = new();
}
