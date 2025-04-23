namespace DecSm.Results.UnitTests.TestUtils;

public class ResultBaseConverter : WriteOnlyJsonConverter<ResultBase>
{
    public override void Write(VerifyJsonWriter writer, ResultBase resultBase)
    {
        if (resultBase is Result result)
        {
            WriteResult(writer, result);

            return;
        }

        var resultType = resultBase.GetType();
        var resultOf = typeof(Result<>);

        if (!resultType.IsGenericType || resultType.Name != resultOf.Name)
            return;

        var genericArgument = resultType
            .GetGenericArguments()[0];

        var method = typeof(ResultBaseConverter).GetMethod(nameof(WriteResultOf), BindingFlags.NonPublic | BindingFlags.Static)!;
        var genericMethod = method.MakeGenericMethod(genericArgument);
        genericMethod.Invoke(this, [writer, resultBase]);
    }

    private static void WriteResult(VerifyJsonWriter writer, Result result)
    {
        writer.WriteStartObject();
        writer.WriteMember(result, result.IsFailed, "IsFailed");
        writer.WriteMember(result, result.Reason, "Reason");
        writer.WriteEndObject();
    }

    private static void WriteResultOf<T>(VerifyJsonWriter writer, Result<T> result)
    {
        writer.WriteStartObject();
        writer.WriteMember(result, result.IsFailed, "IsFailed");
        writer.WriteMember(result, result.Reason, "Reason");
        writer.WriteMember(result, result.ValueOrDefault, "ValueOrDefault");
        writer.WriteEndObject();
    }
}
