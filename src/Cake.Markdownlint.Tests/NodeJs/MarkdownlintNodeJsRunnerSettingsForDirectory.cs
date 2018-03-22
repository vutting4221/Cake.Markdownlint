namespace Cake.Markdownlint.Tests.NodeJs
{
    using Cake.Core.IO;
    using Cake.Markdownlint.NodeJs;

    /// <summary>
    /// Test class for <see cref="MarkdownlintNodeJsRunnerSettings"/> for linting a directory.
    /// </summary>
    public class MarkdownlintNodeJsRunnerSettingsForDirectory : MarkdownlintNodeJsRunnerSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownlintNodeJsRunnerSettingsForDirectory"/> class.
        /// </summary>
        public MarkdownlintNodeJsRunnerSettingsForDirectory()
            : base(new DirectoryPath(@"c:\directory-to-lint"))
        {
        }
    }
}
