namespace Atom.Targets;

[TargetDefinition]
internal partial interface IPackResults : IDotnetPackHelper
{
    const string ResultsProjectName = "DecSm.Results";

    Target PackResults =>
        d => d
            .WithDescription("Builds the DecSm.Results project into a NuGet package")
            .ProducesArtifact(ResultsProjectName)
            .Executes(async () => await DotnetPackProject(new(ResultsProjectName)));
}
