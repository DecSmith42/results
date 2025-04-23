namespace DecSm.Results.Serialization;

/// <summary>
///     A serializable representation of an <see cref="Exception" />.
/// </summary>
[PublicAPI]
public sealed record SerializableException
{
    private static FieldInfo? _stackTraceField;
    private static FieldInfo? _innerExceptionField;
    private static FieldInfo? _messageField;

    /// <summary>
    ///     The fully-qualified name of the exception type.
    /// </summary>
    public string ExceptionType { get; init; } = string.Empty;

    /// <summary>
    ///     The exception <see cref="Exception.Message" /> property.
    /// </summary>
    public string Message { get; init; } = string.Empty;

    /// <summary>
    ///     The exception <see cref="Exception.StackTrace" /> property.
    /// </summary>
    public string? StackTrace { get; init; }

    /// <summary>
    ///     The exception <see cref="Exception.InnerException" /> property.
    /// </summary>
    public SerializableException? InnerException { get; init; }

    private static FieldInfo StackTraceField =>
        _stackTraceField ??= typeof(Exception).GetField("_stackTraceString", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static FieldInfo InnerExceptionField =>
        _innerExceptionField ??= typeof(Exception).GetField("_innerException", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static FieldInfo MessageField =>
        _messageField ??= typeof(Exception).GetField("_message", BindingFlags.Instance | BindingFlags.NonPublic)!;

    /// <summary>
    ///     Creates a <see cref="SerializableException" /> from an <see cref="Exception" />.
    /// </summary>
    /// <param name="exception">The exception to use.</param>
    /// <returns>A <see cref="SerializableException" /> representing the <paramref name="exception" />.</returns>
    public static SerializableException FromException(Exception exception) =>
        new()
        {
            ExceptionType = exception.GetType()
                                .AssemblyQualifiedName ??
                            string.Empty,
            Message = exception.Message,
            StackTrace = exception.StackTrace,
            InnerException = exception.InnerException is null
                ? null
                : FromException(exception.InnerException),
        };

    /// <summary>
    ///     Converts a <see cref="SerializableException" /> to an <see cref="Exception" />.
    /// </summary>
    /// <param name="exception">The <see cref="SerializableException" /> to convert.</param>
    /// <returns>An <see cref="Exception" /> representing the <paramref name="exception" />.</returns>
    /// <exception cref="JsonException">
    ///     Thrown when the <see cref="SerializableException.ExceptionType" /> cannot be found or
    ///     an instance cannot be created.
    /// </exception>
    public static Exception ToException(SerializableException exception)
    {
        var type = Type.GetType(exception.ExceptionType) ?? typeof(Exception);

        Exception? instance;

        if (type == typeof(Exception))
        {
            instance = new(exception.Message);
        }
        else if (type
                 .GetConstructors()
                 .Any(x => x.GetParameters() is [{ } info] && info.ParameterType == typeof(string)))
        {
            instance = (Exception)Activator.CreateInstance(type, exception.Message)!;
        }
        else
        {
            instance = (Exception)TypeHandler.CreateInstance(type);
            MessageField.SetValue(instance, exception.Message);
        }

        StackTraceField.SetValue(instance, exception.StackTrace);
        InnerExceptionField.SetValue(instance, exception.InnerException);

        return instance;
    }
}
