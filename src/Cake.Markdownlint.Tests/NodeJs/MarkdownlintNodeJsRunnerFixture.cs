namespace Cake.Markdownlint.Tests.NodeJs
{
    using Cake.Markdownlint.NodeJs;
    using Testing.Fixtures;

    internal class MarkdownlintNodeJsRunnerFixture : ToolFixture<MarkdownlintNodeJsRunnerSettingsForDirectory>
    {
        public MarkdownlintNodeJsRunnerFixture()
            : base("markdownlint.cmd")
        {
        }

        protected override void RunTool()
        {
            var tool = new MarkdownlintNodeJsRunner(this.FileSystem, this.Environment, this.ProcessRunner, this.Tools);
            tool.RunMarkdownlint(this.Settings);
        }
    }
}