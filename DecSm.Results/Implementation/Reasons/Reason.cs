namespace DecSm.Results.Implementation.Reasons;

[PublicAPI]
public abstract record Reason : ReasonBase
{
    private IReason? _cause;

    protected Reason() { }

    protected Reason(IReason cause)
    {
        _cause = cause;
    }

    protected Reason(string message) : base(message) { }

    protected Reason(string message, IReason cause) : base(message)
    {
        _cause = cause;
    }

    [SuppressMessage("Roslynator", "RCS1085:Use auto-implemented property")]
    public IReason? Cause
    {
        get => _cause;
        protected internal init => _cause = value;
    }

    [Pure]
    public sealed override string ToString()
    {
        var typeNameString = GetType()
            .Name;

        var messageString = Message is { Length: > 0 }
            ? $"'{Message}'"
            : string.Empty;

        var dataString = Data.Count > 0
            ? $"Data=[{string.Join(", ", Data.Select(x => $"{x.Key}={x.Value}"))}]"
            : string.Empty;

        var causedByString = _cause is not null
            ? $"Cause=[{_cause}]"
            : string.Empty;

        return (messageString.Length > 0, dataString.Length > 0, causedByString.Length > 0) switch
        {
            (true, true, true) => $"{typeNameString}: {messageString}, {dataString}, {causedByString}",
            (true, true, false) => $"{typeNameString}: {messageString}, {dataString}",
            (true, false, true) => $"{typeNameString}: {messageString}, {causedByString}",
            (true, false, false) => $"{typeNameString}: {messageString}",
            (false, true, true) => $"{typeNameString}: {dataString}, {causedByString}",
            (false, true, false) => $"{typeNameString}: {dataString}",
            (false, false, true) => $"{typeNameString}: {causedByString}",
            (false, false, false) => typeNameString,
        };
    }

    internal void SetState(string? message, List<KeyValuePair<string, object>>? data, IReason? cause)
    {
        base.SetState(message, data);
        _cause = cause;
    }
}
