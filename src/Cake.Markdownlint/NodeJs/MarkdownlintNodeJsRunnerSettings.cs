namespace Cake.Markdownlint.NodeJs
{
    using System;
    using Core;
    using Core.IO;
    using Core.Tooling;

    /// <summary>
    /// Settings for <see cref="MarkdownlintNodeJsRunner"/> .
    /// </summary>
    public class MarkdownlintNodeJsRunnerSettings : ToolSettings
    {
        private FilePath file;
        private DirectoryPath directory;

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownlintNodeJsRunnerSettings"/> class.
        /// </summary>
        /// <param name="file">Path to the file to lint.</param>
        protected MarkdownlintNodeJsRunnerSettings(FilePath file)
        {
            this.file = file ?? throw new ArgumentNullException(nameof(file));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownlintNodeJsRunnerSettings"/> class.
        /// </summary>
        /// <param name="directory">Path to the directory containing the files to lint.</param>
        protected MarkdownlintNodeJsRunnerSettings(DirectoryPath directory)
        {
            this.directory = directory ?? throw new ArgumentNullException(nameof(directory));
        }

        /// <summary>
        /// Gets or sets the path to the config file.
        /// </summary>
        public FilePath ConfigFile { get; set; }

        /// <summary>
        /// Gets or sets the path to the output file.
        /// </summary>
        public FilePath OutputFile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether an exception should be thrown if an issues was
        /// detected.
        /// </summary>
        public bool ThrowOnIssue { get; set; } = true;

        /// <summary>
        /// Returns a new instance of the <see cref="MarkdownlintNodeJsRunnerSettings"/> class for linting a single file.
        /// </summary>
        /// <param name="file">Path to the file to lint.</param>
        /// <returns>Instance of the <see cref="MarkdownlintNodeJsRunnerSettings"/> class.</returns>
        public static MarkdownlintNodeJsRunnerSettings ForFile(FilePath file)
        {
            return new MarkdownlintNodeJsRunnerSettings(file);
        }

        /// <summary>
        /// Returns a new instance of the <see cref="MarkdownlintNodeJsRunnerSettings"/> class for linting a directory with files.
        /// </summary>
        /// <param name="directory">Path to the directory containing the files to lint.</param>
        /// <returns>Instance of the <see cref="MarkdownlintNodeJsRunnerSettings"/> class.</returns>
        public static MarkdownlintNodeJsRunnerSettings ForDirectory(DirectoryPath directory)
        {
            return new MarkdownlintNodeJsRunnerSettings(directory);
        }

        /// <summary>
        /// Evaluates the settings and writes them into <paramref name="args"/>.
        /// </summary>
        /// <param name="args">Argument builder to which the settings should be added.</param>
        internal void Evaluate(ProcessArgumentBuilder args)
        {
            if (this.ConfigFile != null)
            {
                args.AppendSwitchQuoted("-c", this.ConfigFile.FullPath);
            }

            if (this.OutputFile != null)
            {
                args.AppendSwitchQuoted("-o", this.OutputFile.FullPath);
            }

            if (this.file != null)
            {
                args.AppendQuoted(this.file.FullPath);
            }

            if (this.directory != null)
            {
                args.AppendQuoted(this.directory.FullPath);
            }
        }
    }
}
