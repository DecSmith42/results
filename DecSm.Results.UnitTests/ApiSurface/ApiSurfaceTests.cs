namespace DecSm.Results.UnitTests.ApiSurface;

internal sealed class ApiSurfaceTests
{
    private sealed record ApiDefinition
    {
        public required string Name { get; init; }

        public required ConstructorInfo[] Constructors { get; init; }

        public required PropertyInfo[] Properties { get; init; }

        public required MethodInfo[] Methods { get; init; }
    }

    [Test]
    public async Task Api_Surface_Does_Not_Change()
    {
        var contractsAssemblyName = typeof(IResult).Assembly.GetName();

        var contractTypes = Assembly
            .Load(contractsAssemblyName)
            .GetTypes()
            .Where(x => x is { IsPublic: true });

        var ignoredMembers = new[]
        {
            "Equals", "GetHashCode", "GetType", "ToString", "op_Equality", "op_Inequality", "<Clone>$", "Deconstruct",
        };

        var contractDefinitions = contractTypes
            .Select(x => new ApiDefinition
            {
                Name = FormatTypeText(x.Name),
                Constructors = x
                    .GetConstructors()
                    .Where(y => y.DeclaringType != typeof(object))
                    .ToArray(),
                Properties = x.GetProperties(),
                Methods = x.GetMethods(),
            })
            .Select(x => new ApiDefinition
            {
                Name = x.Name,
                Constructors = x.Constructors.ToArray(),
                Properties = x
                    .Properties
                    .Where(y => x.Methods.All(z => z
                                                       .Name
                                                       .Replace("get_", string.Empty)
                                                       .Replace("set_", string.Empty) !=
                                                   y.Name))
                    .ToArray(),
                Methods = x
                    .Methods
                    .Where(y => y.DeclaringType != typeof(object) && !ignoredMembers.Contains(y.Name))
                    .ToArray(),
            });

        var verify = await Verify(contractDefinitions);

        await TestContext.Out.WriteLineAsync(verify.Text);
    }

    [return: NotNullIfNotNull("typeText")]
    private static string? FormatTypeText(string? typeText)
    {
        if (typeText is null)
            return null;

        return typeText
            .Replace("`1[", "<")
            .Replace("`2[", "<")
            .Replace("`3[", "<")
            .Replace("`4[", "<")
            .Replace("`5[", "<")
            .Replace("]", ">")
            .Replace("`1", "<T>")
            .Replace("`2", "<T1, T2>")
            .Replace("`3", "<T1, T2, T3>")
            .Replace("`4", "<T1, T2, T3, T4>")
            .Replace("`5", "<T1, T2, T3, T4, T5>");
    }
}
