namespace DecSm.Results.Abstraction;

[PublicAPI]
public interface IResult
{
    public bool IsFailed { get; }

    public IReason? Reason { get; }

    [Pure]
    public void Deconstruct(out bool isFailed, out IError? error);

    [Pure]
    public string ToString();
}
