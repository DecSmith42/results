namespace DecSm.Results.Serialization;

internal static class TypeHandler
{
    [Pure]
    public static object CreateInstance(Type type)
    {
        #if NET8_0_OR_GREATER
        return type
            .GetConstructors()
            .Any(x => x.GetParameters()
                .Length is 0)
            ? Activator.CreateInstance(type)!
            : RuntimeHelpers.GetUninitializedObject(type);
        #else
        return type
            .GetConstructors()
            .Any(x => x.GetParameters()
                .Length is 0)
            ? Activator.CreateInstance(type)!
            : FormatterServices.GetUninitializedObject(type);
        #endif
    }

    public static Type ReadType(ref Utf8JsonReader reader)
    {
        var typeName = reader.GetString() ?? string.Empty;

        typeName = typeName switch
        {
            "string" => typeof(string).AssemblyQualifiedName,
            "int" => typeof(int).AssemblyQualifiedName,
            "bool" => typeof(bool).AssemblyQualifiedName,
            "decimal" => typeof(decimal).AssemblyQualifiedName,
            "float" => typeof(float).AssemblyQualifiedName,
            "double" => typeof(double).AssemblyQualifiedName,
            "long" => typeof(long).AssemblyQualifiedName,
            "short" => typeof(short).AssemblyQualifiedName,
            "byte" => typeof(byte).AssemblyQualifiedName,
            "char" => typeof(char).AssemblyQualifiedName,
            _ => typeName,
        };

        if (typeName!.StartsWith("immutablearray["))
        {
            var innerType = typeName
                .Split('[')
                .Last()
                .Split(']')
                .First();

            return typeof(ImmutableArray<>).MakeGenericType(Type.GetType(innerType) ??
                                                            throw new JsonException($"Could not find type '{innerType}'."));
        }

        var foundType = Type.GetType(typeName);

        if (foundType is not null)
            return foundType ?? throw new JsonException($"Could not find type '{typeName}'.");

        var assemblies = AppDomain
            .CurrentDomain
            .GetAssemblies()
            .ToList();

        foreach (var assembly in assemblies)
        {
            foundType = assembly.GetType(typeName);

            if (foundType is not null)
                break;
        }

        return foundType ?? throw new JsonException($"Could not find type '{typeName}'.");
    }

    public static void WriteType(Utf8JsonWriter writer, Type type)
    {
        writer.WritePropertyName("$type");

        var typeName = type.AssemblyQualifiedName ?? string.Empty;

        if (type == typeof(string))
            typeName = "string";
        else if (type == typeof(int))
            typeName = "int";
        else if (type == typeof(bool))
            typeName = "bool";
        else if (type == typeof(decimal))
            typeName = "decimal";
        else if (type == typeof(float))
            typeName = "float";
        else if (type == typeof(double))
            typeName = "double";
        else if (type == typeof(long))
            typeName = "long";
        else if (type == typeof(short))
            typeName = "short";
        else if (type == typeof(byte))
            typeName = "byte";
        else if (type == typeof(char))
            typeName = "char";

        if (!type.IsGenericType)
            typeName = typeName
                .Split(',')
                .First();
        else if (type.GetGenericTypeDefinition() == typeof(ImmutableArray<>) && !type.GenericTypeArguments[0].IsGenericType)
            typeName = $"immutablearray[{typeName.Split('[').Last().Split(']').First().Split(',').First()}]";

        writer.WriteStringValue(typeName);
    }
}
