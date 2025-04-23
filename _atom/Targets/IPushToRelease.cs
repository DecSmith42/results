﻿namespace Atom.Targets;

[TargetDefinition]
internal partial interface IPushToRelease : IGithubReleaseHelper
{
    Target PushToRelease =>
        d => d
            .WithDescription("Pushes the package to the release feed.")
            .RequiresParam(Params.GithubToken)
            .ConsumesArtifact(Commands.PackResults, IPackResults.ResultsProjectName)
            .Executes(async () =>
            {
                if (Version.IsPreRelease)
                {
                    Logger.LogInformation("Skipping release push for pre-release version");

                    return;
                }

                await UploadArtifactToRelease(IPackResults.ResultsProjectName);
            });
}
