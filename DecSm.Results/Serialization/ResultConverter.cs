namespace DecSm.Results.Serialization;

/// <summary>
///     Converts a <see cref="Result" /> to or from JSON.
/// </summary>
[PublicAPI]
public sealed class ResultConverter : JsonConverter<Result>
{
    /// <summary>
    ///     Reads and converts the JSON to a <see cref="Result" />.
    /// </summary>
    /// <param name="reader">The reader to read.</param>
    /// <param name="typeToConvert">The type of object to convert.</param>
    /// <param name="options">Options for the serializer.</param>
    /// <returns>A result converted from JSON.</returns>
    /// <exception cref="JsonException">Thrown when JSON is in the incorrect format.</exception>
    public override Result Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var output = Result.Ok();

        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException("Expected start of JSON object.");

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                return output;

            if (reader.TokenType != JsonTokenType.PropertyName)
                throw new JsonException("Expected property name.");

            var outputName = reader.GetString();
            reader.Read();

            output = outputName switch
            {
                nameof(Result.Reason) => output.Reason switch
                {
                    AggregateReason ar => new()
                    {
                        Reason = new AggregateReason(ar.Reasons.Concat([ReasonConversion.ReadReason(ref reader, options)])),
                    },
                    not null => new()
                    {
                        Reason = new AggregateReason([output.Reason, ReasonConversion.ReadReason(ref reader, options)]),
                    },
                    _ => new()
                    {
                        Reason = ReasonConversion.ReadReason(ref reader, options),
                    },
                },
                _ => throw new JsonException("Unexpected property name."),
            };
        }

        throw new JsonException("Unexpected end of JSON.");
    }

    /// <summary>
    ///     Writes a <see cref="Result" /> to JSON.
    /// </summary>
    /// <param name="writer">The writer to write to.</param>
    /// <param name="value">The value to convert.</param>
    /// <param name="options">Options for the serializer.</param>
    public override void Write(Utf8JsonWriter writer, Result value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        if (value.Reason is not null)
        {
            writer.WritePropertyName(nameof(Result.Reason));
            ReasonConversion.WriteReason(writer, value.Reason, options);
        }

        writer.WriteEndObject();
    }
}
