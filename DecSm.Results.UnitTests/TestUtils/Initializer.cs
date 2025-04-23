namespace DecSm.Results.UnitTests.TestUtils;

public static class Initializer
{
    [ModuleInitializer]
    public static void Init()
    {
        DiffTools.UseOrder(DiffTool.Rider, DiffTool.VisualStudioCode, DiffTool.VisualStudio, DiffTool.BeyondCompare);
        VerifyResult.Initialize();
    }
}
