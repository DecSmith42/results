namespace DecSm.Results.UnitTests.TestUtils;

public static class VerifyResult
{
    public static bool Initialized { get; private set; }

    public static void Initialize()
    {
        if (Initialized)
            throw new("Already Initialized");

        Initialized = true;

        InnerVerifier.ThrowIfVerifyHasBeenRun();

        VerifierSettings.AddExtraSettings(settings => settings.Converters.Add(new ResultBaseConverter()));
    }
}
