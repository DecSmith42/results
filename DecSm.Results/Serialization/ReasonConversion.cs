namespace DecSm.Results.Serialization;

internal static class ReasonConversion
{
    public static ReasonBase ReadReason(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException("Expected start of JSON object.");

        var type = default(Type);
        string? message = null;
        List<IReason>? causes = null;
        List<KeyValuePair<string, object>>? data = null;
        List<KeyValuePair<string, object?>>? additionalProperties = null;
        List<PropertyInfo>? additionalPropertyInfos = null;

        while (reader.Read())
        {
            if (reader.TokenType is JsonTokenType.EndObject)
            {
                if (type is null)
                    throw new JsonException("Expected $type.");

                var instance = TypeHandler.CreateInstance(type);

                if (additionalProperties is { Count: > 0 } && additionalPropertyInfos is not null)
                    foreach (var (key, value) in additionalProperties)
                        additionalPropertyInfos
                            .Find(x => x.Name == key)
                            ?.SetValue(instance, value);

                if (typeof(AggregateReason).IsAssignableFrom(type))
                {
                    var aggregateReason = (AggregateReason)instance;
                    aggregateReason.SetState(message, data, causes);

                    return aggregateReason;
                }

                if (typeof(Reason).IsAssignableFrom(type))
                {
                    var reason = (Reason)instance;
                    reason.SetState(message, data, causes?.FirstOrDefault());

                    return reason;
                }

                if (!typeof(ReasonBase).IsAssignableFrom(type))
                    throw new JsonException("Expected end of JSON object.");

                var reasonBase = (ReasonBase)instance;
                reasonBase.SetState(message, data);

                return reasonBase;
            }

            if (reader.TokenType != JsonTokenType.PropertyName)
                throw new JsonException("Expected property name.");

            switch (reader.GetString())
            {
                case "$type":

                    reader.Read();

                    if (reader.TokenType != JsonTokenType.String)
                        throw new JsonException("Expected string.");

                    type = TypeHandler.ReadType(ref reader);

                    break;

                case nameof(ReasonBase.Data):

                    data = ReadData(ref reader, options);

                    break;

                case nameof(ReasonBase.Message):

                    reader.Read();
                    message = reader.GetString() ?? string.Empty;

                    break;

                case nameof(Reason.Cause):

                    reader.Read();
                    (causes ?? []).Add(ReadReason(ref reader, options));

                    break;

                case nameof(AggregateReason.Reasons):

                    causes = ReadReasons(ref reader, options);

                    break;

                default:

                    if (type is null)
                        throw new JsonException("Expected $type.");

                    var key = reader.GetString() ?? string.Empty;

                    additionalPropertyInfos ??= GetAdditionalPropertyInfos(type);

                    var property = additionalPropertyInfos.Find(x => x.Name == key);

                    if (property is null)
                    {
                        reader.Read();
                        reader.Skip();

                        continue;
                    }

                    var propertyType = property.PropertyType;

                    if (typeof(Exception).IsAssignableFrom(propertyType))
                        propertyType = typeof(SerializableException);

                    var value = JsonSerializer.Deserialize(ref reader, propertyType, options);

                    if (value is SerializableException serializableException)
                        value = SerializableException.ToException(serializableException);

                    if (additionalProperties is not null)
                        additionalProperties.Add(new(key, value));
                    else
                        additionalProperties = [new(key, value)];

                    break;
            }
        }

        throw new JsonException("Unexpected end of JSON.");
    }

    private static List<PropertyInfo> GetAdditionalPropertyInfos(Type type) =>
        type
            .GetProperties()
            .Where(x => x is
            {
                CanWrite: true,
                Name: not (nameof(ReasonBase.Message)
                or nameof(ReasonBase.Data)
                or nameof(Reason.Cause)
                or nameof(AggregateReason.Reasons)),
            })
            .ToList();

    private static List<KeyValuePair<string, object>> ReadData(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        reader.Read();

        if (reader.TokenType is not JsonTokenType.StartObject)
            throw new JsonException("Expected start of JSON object.");

        var data = new List<KeyValuePair<string, object>>();

        while (reader.Read())
        {
            if (reader.TokenType is JsonTokenType.EndObject)
                return data;

            if (reader.TokenType is not JsonTokenType.PropertyName)
                throw new JsonException("Expected property name.");

            var key = reader.GetString() ?? string.Empty;

            reader.Read();

            switch (reader.TokenType)
            {
                case JsonTokenType.Null:

                    data.Add(new(key, null!));

                    continue;

                case JsonTokenType.True:

                    data.Add(new(key, true));

                    continue;

                case JsonTokenType.False:

                    data.Add(new(key, false));

                    continue;

                case JsonTokenType.Number:

                    data.Add(new(key, reader.GetDecimal()));

                    continue;

                case JsonTokenType.String:

                    data.Add(new(key, reader.GetString() ?? string.Empty));

                    continue;

                case JsonTokenType.StartObject:

                    data.Add(new(key, ReadTypedObject(ref reader, options)));

                    continue;

                case JsonTokenType.None:
                case JsonTokenType.EndObject:
                case JsonTokenType.StartArray:
                case JsonTokenType.EndArray:
                case JsonTokenType.PropertyName:
                case JsonTokenType.Comment:
                default:

                    data.Add(new(key, JsonSerializer.Deserialize<object>(ref reader, options)!));

                    continue;
            }
        }

        throw new JsonException("Unexpected end of JSON.");
    }

    private static object ReadTypedObject(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        reader.Read();

        if (reader.TokenType == JsonTokenType.PropertyName)
            reader.Read();

        var type = TypeHandler.ReadType(ref reader);

        reader.Read();

        object instance;

        if (typeof(IReason).IsAssignableFrom(type))
        {
            reader.Read();
            instance = ReadReason(ref reader, options);
        }
        else if (type.IsArray)
        {
            instance = ReadReasons(ref reader, options)
                .ToArray();
        }
        else if (type.IsGenericType &&
                 type.GetGenericTypeDefinition() == typeof(ImmutableArray<>) &&
                 type
                     .GetGenericArguments()
                     .First() ==
                 typeof(IReason))
        {
            instance = ReadReasons(ref reader, options)
                .ToImmutableArray();
        }
        else
        {
            reader.Read();
            instance = JsonSerializer.Deserialize(ref reader, type, options)!;
        }

        if (instance is SerializableException sex)
            instance = SerializableException.ToException(sex);

        reader.Read();

        if (reader.TokenType != JsonTokenType.EndObject)
            throw new JsonException("Expected end of JSON object.");

        return instance;
    }

    private static List<IReason> ReadReasons(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        reader.Read();

        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException("Expected start of JSON array.");

        var reasons = new List<IReason>();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
                return reasons;

            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException("Expected start of JSON object.");

            reasons.Add(ReadReason(ref reader, options));
        }

        throw new JsonException("Unexpected end of JSON.");
    }

    public static void WriteReason(Utf8JsonWriter writer, IReason value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        TypeHandler.WriteType(writer, value.GetType());

        // Write message

        writer.WritePropertyName(nameof(ReasonBase.Message));
        writer.WriteStringValue(value.Message);

        // Write cause or reasons

        switch (value)
        {
            case Reason { Cause: not null } reason:
                writer.WritePropertyName(nameof(Reason.Cause));
                WriteReason(writer, reason.Cause, options);

                break;
            case AggregateReason aggregateReason:
                writer.WritePropertyName(nameof(AggregateReason.Reasons));
                WriteReasons(writer, aggregateReason.Reasons, options);

                break;
        }

        // Write data

        writer.WritePropertyName(nameof(ReasonBase.Data));
        writer.WriteStartObject();

        foreach (var pair in value.Data)
        {
            writer.WritePropertyName(pair.Key);
            writer.WriteStartObject();

            // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
            var type = pair.Value?.GetType() ?? typeof(object);
            TypeHandler.WriteType(writer, type);

            writer.WritePropertyName("$value");

            if (pair.Value is null)
            {
                writer.WriteNullValue();
            }
            else
            {
                if (typeof(Exception).IsAssignableFrom(type))
                    type = typeof(SerializableException);

                if (type.IsArray || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ImmutableArray<>)))
                    WriteReasons(writer, (IEnumerable<IReason>)pair.Value, options);
                else
                    JsonSerializer.Serialize(writer,
                        type == typeof(SerializableException)
                            ? SerializableException.FromException((Exception)pair.Value)
                            : pair.Value,
                        options);
            }

            writer.WriteEndObject();
        }

        writer.WriteEndObject();

        // Write other properties

        var properties = value
            .GetType()
            .GetProperties();

        foreach (var property in properties)
        {
            if (property.Name is nameof(ReasonBase.Message)
                or nameof(ReasonBase.Data)
                or nameof(Reason.Cause)
                or nameof(AggregateReason.Reasons))
                continue;

            writer.WritePropertyName(property.Name);

            var propertyValue = property.GetValue(value);

            if (propertyValue is Exception ex)
                propertyValue = SerializableException.FromException(ex);

            JsonSerializer.Serialize(writer, propertyValue, options);
        }

        writer.WriteEndObject();
    }

    private static void WriteReasons(Utf8JsonWriter writer, IEnumerable<IReason> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        foreach (var reason in value)
            WriteReason(writer, reason, options);

        writer.WriteEndArray();
    }
}
