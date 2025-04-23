BenchmarkRunner.Run<Comparisons>(DefaultConfig
    .Instance
    .WithOptions(ConfigOptions.DisableOptimizationsValidator)
    .AddExporter(MarkdownExporter.Default));
