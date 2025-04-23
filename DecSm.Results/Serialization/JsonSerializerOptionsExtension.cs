namespace DecSm.Results.Serialization;

/// <summary>
///     Provides extension methods for the <see cref="Result" />
///     class to support serializing and deserializing of <see cref="Result{T}" />
///     and <see cref="Result" /> types.
/// </summary>
/// <remarks>
///     This extension allows for easy configuration of JsonSerializerOptions to support
///     serialization and deserialization of complex types <see cref="Result{T}" /> and <see cref="JsonSerializerOptions" />.
///     It does this by adding the necessary converters to JsonSerializerOptions.
/// </remarks>
[PublicAPI]
public static class JsonSerializerOptionsExtensions
{
    /// <summary>
    ///     Configures <see cref="JsonSerializerOptions" /> instance
    ///     for serializing and deserializing <see cref="Result" /> and <see cref="Result{T}" /> types.
    /// </summary>
    /// <param name="options">An instance of JsonSerializerOptions which needs to be configured.</param>
    /// <returns>The configured <see cref="JsonSerializerOptions" /> instance.</returns>
    /// <remarks>
    ///     This method adds <see cref="ResultConverter" /> and <see cref="ResultOfConverterFactory" />
    ///     to the Converters collection of the JsonSerializerOptions.
    /// </remarks>
    public static JsonSerializerOptions ConfigureForResults(this JsonSerializerOptions options)
    {
        #if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(options);
        #else
        if (options is null)
            throw new ArgumentNullException(nameof(options));
        #endif

        options.Converters.Add(new ResultConverter());
        options.Converters.Add(new ResultOfConverterFactory());

        return options;
    }
}
