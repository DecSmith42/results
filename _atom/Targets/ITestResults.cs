namespace Atom.Targets;

[TargetDefinition]
internal partial interface ITestResults : IDotnetTestHelper
{
    const string ResultsTestProjectName = "DecSm.Results.UnitTests";

    Target TestResults =>
        d => d
            .WithDescription("Runs the DecSm.Results.UnitTests tests")
            .ProducesArtifact(ResultsTestProjectName)
            .Executes(async () =>
            {
                var exitCode = 0;

                exitCode += await RunDotnetUnitTests(new(ResultsTestProjectName));

                if (exitCode != 0)
                    throw new StepFailedException("One or more unit tests failed");
            });
}
