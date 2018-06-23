namespace Cake.Markdownlint.NodeJs
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using Cake.Core.Diagnostics;
    using Core;
    using Core.IO;
    using Core.Tooling;

    /// <summary>
    /// A wrapper around the Markdownlint tool.
    /// </summary>
    internal class MarkdownlintNodeJsRunner : Tool<MarkdownlintNodeJsRunnerSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownlintNodeJsRunner" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The Cake environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="toolLocator">The tool locator.</param>
        public MarkdownlintNodeJsRunner(
            IFileSystem fileSystem,
            ICakeEnvironment environment,
            IProcessRunner processRunner,
            IToolLocator toolLocator)
            : base(fileSystem, environment, processRunner, toolLocator)
        {
        }

        /// <summary>
        /// Runs Markdownlint with specified settings.
        /// </summary>
        /// <param name="settings">Settings for running Markdownlint.</param>
        public void RunMarkdownlint(MarkdownlintNodeJsRunnerSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var args = new ProcessArgumentBuilder();
            settings.Evaluate(args);

            // Make sure directory of output file exists as otherwise markdownlint will silently fail.
            if (settings.OutputFile != null)
            {
                Directory.CreateDirectory(settings.OutputFile.GetDirectory().FullPath);
            }

            var processSettings = new ProcessSettings
            {
                Arguments = args.Render(),
                RedirectStandardError = true
            };

            this.Run(
                settings,
                null,
                processSettings,
                process =>
                {
                    process.GetStandardError();

                    var exitCode = process.GetExitCode();
                    if (exitCode != 0 && settings.ThrowOnIssue)
                    {
                        const string message = "Markdownlint returned with an error (exit code {0}).";
                        throw new CakeException(
                            string.Format(
                                CultureInfo.InvariantCulture,
                                message,
                                exitCode));
                    }
                });
        }

        /// <inheritdoc />
        protected override string GetToolName()
        {
            return "Markdownlint";
        }

        /// <inheritdoc />
        protected override IEnumerable<string> GetToolExecutableNames()
        {
            yield return "markdownlint.cmd";
            yield return "markdownlint";
        }

        /// <inheritdoc />
        protected override void ProcessExitCode(int exitCode)
        {
            // Don't throw an exit code.
            // We handle behavior ourselves in the post action.
            return;
        }
    }
}
