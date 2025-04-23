namespace DecSm.Results.Implementation.Reasons;

[PublicAPI]
public record ExceptionError(Exception Exception) : ReasonBase, IError, IMaskableReason
{
    public Exception Exception { get; protected init; } = Exception;

    public override string Message => Exception.Message;

    [Pure]
    public sealed override string ToString()
    {
        var type = GetType();
        var exceptionType = Exception.GetType();

        var typeNameString = type == typeof(ExceptionError)
            ? exceptionType.Name
            : $"{type.Name}:{exceptionType.Name}";

        var message = Message;
        var exceptionMessage = Exception.Message;

        var messageString = message == exceptionMessage
            ? message.Length > 0
                ? $"'{message}'"
                : string.Empty
            : message.Length > 0
                ? exceptionMessage.Length > 0
                    ? $"'{message}' ({exceptionMessage})"
                    : $"'{message}'"
                : exceptionMessage.Length > 0
                    ? $"'{exceptionMessage}'"
                    : string.Empty;

        var dataString = Data.Count > 0
            ? $"Data=[{string.Join(", ", Data.Select(x => $"{x.Key}={x.Value}"))}]"
            : string.Empty;

        return (messageString.Length > 0, dataString.Length > 0) switch
        {
            (true, true) => $"{typeNameString}: {messageString}, {dataString}",
            (true, false) => $"{typeNameString}: {messageString}",
            (false, true) => $"{typeNameString}: {dataString}",
            (false, false) => typeNameString,
        };
    }

    [Pure]
    public virtual IReason Mask() =>
        Error.Default;
}
