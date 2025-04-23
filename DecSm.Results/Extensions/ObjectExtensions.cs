namespace DecSm.Results.Extensions;

[PublicAPI]
public static class ObjectExtensions
{
    [Pure]
    public static Result<TValue> ToResult<TValue>(this TValue value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        return new()
        {
            ValueOrDefault = value,
        };
    }

    [Pure]
    public static Result<TValue?> ToResultNullable<TValue>(this TValue? value) =>
        new()
        {
            ValueOrDefault = value,
        };

    [Pure]
    internal static string PrintValueWithLabel<T>(this T? value, string label)
    {
        if (value is null)
            return string.Empty;

        var valueText = value.ToString();

        return valueText is { Length: > 0 }
            ? valueText.StartsWith('[') && valueText.EndsWith(']')
                ? $"{label}={valueText}"
                : $"{label}='{valueText}'"
            : string.Empty;
    }

    internal static IReadOnlyDictionary<TK, TV> Add<TD, TK, TV>(this TD dictionary, TK key, TV value)
        where TD : IReadOnlyDictionary<TK, TV>
        where TK : notnull
    {
        var dict = dictionary as Dictionary<TK, TV> ?? dictionary.ToDictionary(x => x.Key, x => x.Value);

        dict.Add(key, value);

        return dict;
    }

    internal static IReadOnlyDictionary<TK, TV> AddRange<TD, TK, TV>(this TD dictionary, IEnumerable<KeyValuePair<TK, TV>> values)
        where TD : IReadOnlyDictionary<TK, TV>
        where TK : notnull
    {
        var dict = dictionary as Dictionary<TK, TV> ?? dictionary.ToDictionary(x => x.Key, x => x.Value);

        foreach (var (key, value) in values)
            dict.Add(key, value);

        return dict;
    }
}
