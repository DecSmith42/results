namespace DecSm.Results.Implementation.Reasons;

[PublicAPI]
public abstract record ReasonBase() : IReason
{
    private IReadOnlyDictionary<string, object> _data = ImmutableDictionary<string, object>.Empty;
    private string _message = string.Empty;

    protected ReasonBase(string message) : this()
    {
        _message = message;
    }

    public virtual string Message
    {
        get => _message;
        protected internal init => _message = value;
    }

    [SuppressMessage("Roslynator", "RCS1085:Use auto-implemented property")]
    public IReadOnlyDictionary<string, object> Data
    {
        get => _data;
        init => _data = value;
    }

    [Pure]
    public override string ToString()
    {
        var dataString = _data.Count > 0
            ? $", Data=[{string.Join(", ", _data.Select(x => $"{x.Key}={x.Value}"))}]"
            : string.Empty;

        return $"{GetType().Name}: '{_message}'{dataString}";
    }

    internal void SetState(string? message, List<KeyValuePair<string, object>>? data)
    {
        if (message is not null)
            _message = message;

        if (data is not null)
            _data = data.ToImmutableDictionary();
    }
}
