namespace DecSm.Results.Abstraction;

[PublicAPI]
public interface IReason
{
    string Message { get; }

    IReadOnlyDictionary<string, object> Data { get; }

    [Pure]
    public string ToString();
}
