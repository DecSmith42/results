namespace Atom;

[BuildDefinition]
[GenerateEntryPoint]
internal partial class Build : DefaultBuildDefinition, IGithubWorkflows, IGitVersion, ITargets
{
    public override IReadOnlyList<IWorkflowOption> GlobalWorkflowOptions =>
    [
        UseGitVersionForBuildId.Enabled, new SetupDotnetStep("9.0.x"),
    ];

    public override IReadOnlyList<WorkflowDefinition> Workflows =>
    [
        new("Validate")
        {
            Triggers = [GitPullRequestTrigger.IntoMain, ManualTrigger.Empty],
            StepDefinitions = [Targets.SetupBuildInfo, Targets.PackResults.WithSuppressedArtifactPublishing, Targets.TestResults],
            WorkflowTypes = [Github.WorkflowType],
        },
        new("Build")
        {
            Triggers = [GitPushTrigger.ToMain, GithubReleaseTrigger.OnReleased, ManualTrigger.Empty],
            StepDefinitions =
            [
                Targets.SetupBuildInfo,
                Targets.PackResults,
                Targets.TestResults,
                Targets.PushToNuget.WithOptions(WorkflowSecretInjection.Create(Params.NugetApiKey)),
                Targets.PushToRelease.WithGithubTokenInjection(),
            ],
            WorkflowTypes = [Github.WorkflowType],
        },
        Github.DependabotDefaultWorkflow(),
    ];
}
