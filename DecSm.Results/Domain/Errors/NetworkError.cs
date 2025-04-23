namespace DecSm.Results.Domain.Errors;

[PublicAPI]
public record NetworkError(Exception Exception) : ExceptionError(Exception)
{
    public NetworkError(string message) : this(new Exception(message)) { }

    public static NetworkError Default { get; } = new(new Exception("Network error"));

    [Pure]
    public override IReason Mask() =>
        new Error("Network error");
}
