namespace DecSm.Results.Implementation.Results;

[PublicAPI]
public sealed partial record Result : ResultBase
{
    // Result

    private static Result _cachedEmpty = new();

    [Pure]
    public static Result Ok() =>
        _cachedEmpty;

    [Pure]
    public static Result Create(IReason reason) =>
        new()
        {
            Reason = reason,
        };

    [Pure]
    public static Result Create(IEnumerable<IReason> reasons) =>
        new()
        {
            Reason = new AggregateReason(reasons),
        };

    [Pure]
    public static Result Create(ImmutableArray<IReason> reasons) =>
        new()
        {
            Reason = new AggregateReason(reasons),
        };

    [Pure]
    public static Result Success(ISuccess success) =>
        new()
        {
            Reason = success,
        };

    [Pure]
    public static Result Failure(IError error) =>
        new()
        {
            Reason = error,
        };

    // Result<T>

    [Pure]
    public static Result<TValue> Ok<TValue>(TValue value) =>
        new()
        {
            ValueOrDefault = value,
        };

    [Pure]
    public static Result<TValue> Create<TValue>(TValue value, IReason reason) =>
        new()
        {
            ValueOrDefault = value,
            Reason = reason,
        };

    [Pure]
    public static Result<TValue> Success<TValue>(TValue value, ISuccess success) =>
        new()
        {
            ValueOrDefault = value,
            Reason = success,
        };

    [Pure]
    public static Result<TValue> Failure<TValue>(IError error) =>
        new()
        {
            Reason = error,
        };

    // Util

    // ReSharper disable once SealedMemberInSealedClass - We need to keep it here for a record in netstandard
    [Pure]
    public sealed override string ToString() =>
        base.ToString();
}
