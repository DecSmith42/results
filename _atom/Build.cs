namespace Atom;

[BuildDefinition]
[GenerateEntryPoint]
internal partial class Build : DefaultBuildDefinition, IGithubWorkflows, IGitVersion, IPackResults, ITestResults, IPushToNuget
{
    public override IReadOnlyList<IWorkflowOption> DefaultWorkflowOptions =>
    [
        UseGitVersionForBuildId.Enabled, new SetupDotnetStep("9.0.x"),
    ];

    public override IReadOnlyList<WorkflowDefinition> Workflows =>
    [
        new("Validate")
        {
            Triggers = [GitPullRequestTrigger.IntoMain, ManualTrigger.Empty],
            StepDefinitions = [Commands.SetupBuildInfo, Commands.PackResults.WithSuppressedArtifactPublishing, Commands.TestResults],
            WorkflowTypes = [Github.WorkflowType],
        },
        new("Build")
        {
            Triggers = [GitPushTrigger.ToMain, GithubReleaseTrigger.OnReleased, ManualTrigger.Empty],
            StepDefinitions =
            [
                Commands.SetupBuildInfo,
                Commands.PackResults,
                Commands.TestResults,
                Commands.PushToNuget.WithAddedOptions(WorkflowSecretInjection.Create(Params.NugetApiKey)),
            ],
            WorkflowTypes = [Github.WorkflowType],
        },
    ];
}
