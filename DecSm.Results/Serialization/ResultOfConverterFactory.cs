namespace DecSm.Results.Serialization;

/// <summary>
///     Provides a factory for creating JSON converters for <see cref="Result{T}" />.
/// </summary>
[PublicAPI]
public sealed class ResultOfConverterFactory : JsonConverterFactory
{
    /// <summary>
    ///     Determines whether the <see cref="ResultOfConverter{T}" /> can convert an object of a specified type.
    /// </summary>
    /// <param name="typeToConvert">The type of object to check for convertibility.</param>
    /// <returns>'true' if the converter can convert typeToConvert; otherwise, 'false'.</returns>
    public override bool CanConvert(Type typeToConvert)
    {
        if (!typeToConvert.IsGenericType)
            return false;

        var genericType = typeToConvert.GetGenericTypeDefinition();

        return genericType == typeof(Result<>);
    }

    /// <summary>
    ///     Creates a converter for a specified type.
    /// </summary>
    /// <param name="typeToConvert">The type to create a converter for.</param>
    /// <param name="options">Options for the serializer.</param>
    /// <returns>A new converter for the specified type.</returns>
    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var genericType = typeToConvert
            .GetGenericArguments()[0];

        var converterType = typeof(ResultOfConverter<>).MakeGenericType(genericType);

        return (JsonConverter?)Activator.CreateInstance(converterType);
    }
}
