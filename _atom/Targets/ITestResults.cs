namespace Atom.Targets;

[TargetDefinition]
internal partial interface ITestResults : IDotnetTestHelper
{
    const string ResultsTestProjectName = "DecSm.Results.UnitTests";

    Target TestResults =>
        d => d
            .WithDescription("Runs the DecSm.Results.UnitTests tests")
            .ProducesArtifact(ResultsTestProjectName)
            .Executes(async () => await RunDotnetUnitTests(new(ResultsTestProjectName)));
}
