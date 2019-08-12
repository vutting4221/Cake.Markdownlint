namespace Cake.Markdownlint
{
    using System;
    using Cake.Core;
    using Cake.Core.Annotations;
    using Cake.Core.IO;
    using Cake.Markdownlint.NodeJs;

    /// <summary>
    /// Provides a wrapper around Markdownlint functionality within a Cake build script.
    /// </summary>
    [CakeAliasCategory("Markdownlint")]
    public static class MarkdownlintAliases
    {
        /// <summary>
        /// Runs Node.js markdownlint-cli for a specific file with default settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="file">Path to the file to lint.</param>
        /// <example>
        /// <code>
        /// <![CDATA[
        ///     RunMarkdownlintNodeJsForSingleFile(@"c:\foo.md");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("NodeJs")]
        [CakeNamespaceImport("Cake.Markdownlint.NodeJs")]
        public static void RunMarkdownlintNodeJsForSingleFile(this ICakeContext context, FilePath file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            context.RunMarkdownlintNodeJs(MarkdownlintNodeJsRunnerSettings.ForFile(file));
        }

        /// <summary>
        /// Runs Node.js markdownlint-cli for a directory with default settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="directory">Path to the directory containing the files to lint.</param>
        /// <example>
        /// <code>
        /// <![CDATA[
        ///     RunMarkdownlintNodeJsForDirectory(@"c:\foo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("NodeJs")]
        [CakeNamespaceImport("Cake.Markdownlint.NodeJs")]
        public static void RunMarkdownlintNodeJsForDirectory(this ICakeContext context, DirectoryPath directory)
        {
            if (directory == null)
            {
                throw new ArgumentNullException(nameof(directory));
            }

            context.RunMarkdownlintNodeJs(MarkdownlintNodeJsRunnerSettings.ForDirectory(directory));
        }

        /// <summary>
        /// Runs Node.js markdownlint-cli with specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for running Markdownlint.</param>
        /// <example>
        /// <para>Define working directory and output file:</para>
        /// <code>
        /// <![CDATA[
        ///     var settings =
        ///         MarkdownlintNodeJsRunnerSettings.ForDirectory(@"c:\myproject");
        ///     setting.OutputFile = @"c:\myprojects\logs\markdownlint.log";
        ///     RunMarkdownlintNodeJs(settings);
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("NodeJs")]
        [CakeNamespaceImport("Cake.Markdownlint.NodeJs")]
        public static void RunMarkdownlintNodeJs(this ICakeContext context, MarkdownlintNodeJsRunnerSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var runner =
                new MarkdownlintNodeJsRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.RunMarkdownlint(settings);
        }
    }
}
