namespace DecSm.Results.Serialization;

/// <summary>
///     Converts a <see cref="Result{T}" /> to or from JSON.
/// </summary>
/// <typeparam name="T">The type of the result.</typeparam>
[PublicAPI]
public class ResultOfConverter<T> : JsonConverter<Result<T>>
{
    /// <summary>
    ///     Reads and converts the JSON to a <see cref="Result{T}" />.
    /// </summary>
    /// <param name="reader">The reader to read.</param>
    /// <param name="typeToConvert">The type of object to convert.</param>
    /// <param name="options">Options for the serializer.</param>
    /// <returns>A result converted from JSON.</returns>
    /// <exception cref="JsonException">Thrown when JSON is in the incorrect format.</exception>
    public override Result<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Result<T>? output = null;
        IReason? reason = null;

        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException("Expected start of JSON object.");

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                if (output is null)
                    return null;

                return reason is null
                    ? output
                    : output.Reason switch
                    {
                        AggregateReason ar => output with
                        {
                            Reason = new AggregateReason(ar.Reasons.Concat([reason])),
                        },
                        not null => output with
                        {
                            Reason = new AggregateReason([output.Reason, reason]),
                        },
                        _ => output with
                        {
                            Reason = reason,
                        },
                    };
            }

            if (reader.TokenType != JsonTokenType.PropertyName)
                throw new JsonException("Expected property name.");

            switch (reader.GetString())
            {
                case nameof(IResult.Reason):
                    reader.Read();
                    reason = ReasonConversion.ReadReason(ref reader, options);

                    break;
                case nameof(Result<T>.ValueOrDefault):
                    ReadValue(ref reader, ref output, options);

                    break;
                default:
                    throw new JsonException("Unexpected property name.");
            }
        }

        throw new JsonException("Unexpected end of JSON.");
    }

    /// <summary>
    ///     Writes a <see cref="Result{T}" /> to JSON.
    /// </summary>
    /// <param name="writer">The writer to write to.</param>
    /// <param name="value">The value to convert.</param>
    /// <param name="options">Options for the serializer.</param>
    public override void Write(Utf8JsonWriter writer, Result<T> value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        if (value.Reason is not null)
        {
            writer.WritePropertyName(nameof(Result.Reason));
            ReasonConversion.WriteReason(writer, value.Reason, options);
        }

        writer.WritePropertyName(nameof(Result<T>.ValueOrDefault));
        JsonSerializer.Serialize(writer, value.ValueOrDefault, options);

        writer.WriteEndObject();
    }

    /// <summary>
    ///     Reads a value from the JSON reader.
    /// </summary>
    /// <param name="reader">The reader to read from.</param>
    /// <param name="output">The output to produce.</param>
    /// <param name="options">Options for the serializer.</param>
    /// <exception cref="JsonException">Thrown when JSON is in the incorrect format.</exception>
    private static void ReadValue(ref Utf8JsonReader reader, ref Result<T>? output, JsonSerializerOptions options)
    {
        reader.Read();

        if (reader.TokenType == JsonTokenType.Null)
        {
            output = (output ?? new Result<T>()).WithValue(default!);

            return;
        }

        var value = JsonSerializer.Deserialize<T>(ref reader, options);
        output = (output ?? new Result<T>()).WithValue(value ?? throw new JsonException("Expected value."));
    }
}
