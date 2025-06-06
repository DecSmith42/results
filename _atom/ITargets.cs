namespace Atom;

[PublicAPI]
internal interface ITargets : ISetupBuildInfo, IDotnetPackHelper, IDotnetTestHelper, INugetHelper, IGithubReleaseHelper
{
    const string ResultsProjectName = "DecSm.Results";
    const string ResultsTestProjectName = "DecSm.Results.UnitTests";

    [ParamDefinition("nuget-push-feed", "The Nuget feed to push to.", "https://api.nuget.org/v3/index.json")]
    string NugetFeed => GetParam(() => NugetFeed, "https://api.nuget.org/v3/index.json");

    [SecretDefinition("nuget-push-api-key", "The API key to use to push to Nuget.")]
    string? NugetApiKey => GetParam(() => NugetApiKey);

    Target PackResults =>
        d => d
            .DescribedAs("Builds the DecSm.Results project into a NuGet package")
            .ProducesArtifact(ResultsProjectName)
            .Executes(async cancellationToken => await DotnetPackProject(new(ResultsProjectName), cancellationToken));

    Target TestResults =>
        d => d
            .DescribedAs("Runs the DecSm.Results.UnitTests tests")
            .ProducesArtifact(ResultsTestProjectName)
            .Executes(async cancellationToken =>
            {
                var exitCode = 0;

                exitCode += await RunDotnetUnitTests(new(ResultsTestProjectName), cancellationToken);

                if (exitCode != 0)
                    throw new StepFailedException("One or more unit tests failed");
            });

    Target PushToNuget =>
        d => d
            .DescribedAs("Pushes the Atom projects to Nuget")
            .RequiresParam(nameof(NugetFeed))
            .RequiresParam(nameof(NugetApiKey))
            .ConsumesArtifact(nameof(PackResults), ResultsProjectName)
            .Executes(async cancellationToken =>
                await PushProject(ResultsProjectName, NugetFeed, NugetApiKey!, cancellationToken: cancellationToken));

    Target PushToRelease =>
        d => d
            .DescribedAs("Pushes the package to the release feed.")
            .RequiresParam(nameof(GithubToken))
            .ConsumesVariable(nameof(SetupBuildInfo), nameof(BuildVersion))
            .ConsumesArtifact(nameof(PackResults), ResultsProjectName)
            .Executes(async () =>
            {
                if (BuildVersion.IsPreRelease)
                {
                    Logger.LogInformation("Skipping release push for pre-release version");

                    return;
                }

                await UploadArtifactToRelease(ResultsProjectName, $"v{BuildVersion}");
            });
}
