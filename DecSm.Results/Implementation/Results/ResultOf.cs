namespace DecSm.Results.Implementation.Results;

[PublicAPI]
public sealed record Result<T> : ResultBase
{
    [JsonIgnore]
    public T Value
    {
        get
        {
            if (IsFailed)
                throw new InvalidOperationException($"Result is in status failed. Value is not set. Having: {Reason as IError}");

            return ValueOrDefault!;
        }
    }

    public T? ValueOrDefault { get; internal init; }

    // ReSharper disable once SealedMemberInSealedClass - We need to keep it here for a record in netstandard
    [Pure]
    public sealed override string ToString()
    {
        var text = base.ToString();

        return IsFailed
            ? text
            : text is { Length: > 0 }
                ? text.StartsWith('[') && text.EndsWith(']')
                    ? $"{nameof(Value)}={text}"
                    : $"{nameof(Value)}='{text}'"
                : string.Empty;
    }

    [Pure]
    public Result<T> WithValue(T value) =>
        IsFailed
            ? this
            : this with
            {
                ValueOrDefault = value,
            };

    [Pure]
    public Result WithoutValue() =>
        new()
        {
            Reason = Reason,
        };

    [Pure]
    public Result<TNewValue> TransformValue<TNewValue>(Func<T, TNewValue> valueTransform) =>
        new()
        {
            ValueOrDefault = IsFailed
                ? default!
                : valueTransform(ValueOrDefault!),
            Reason = Reason,
        };

    [Pure]
    public void Deconstruct(out bool isFailed, out IError? error, out T? valueOrDefault)
    {
        isFailed = IsFailed;
        error = Reason as IError;
        valueOrDefault = ValueOrDefault;
    }
}
