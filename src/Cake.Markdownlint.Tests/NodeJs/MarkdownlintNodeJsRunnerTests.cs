namespace Cake.Markdownlint.Tests.NodeJs
{
    using Shouldly;
    using Xunit;

    public sealed class MarkdownlintNodeJsRunnerTests
    {
        public sealed class TheRunMarkdownlintMethod
        {
            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given
                var fixture = new MarkdownlintNodeJsRunnerFixture
                {
                    Settings = null,
                };

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                result.IsArgumentNullException("settings");
            }

            [Fact]
            public void No_Settings_Specified_Should_Execute_Command_Without_Arguments()
            {
                var fixture = new MarkdownlintNodeJsRunnerFixture();

                var result = fixture.Run();

                result.Args.ShouldBe("\"c:/directory-to-lint\"");
            }

            [Fact]
            public void ConfigFile_Should_Be_Added_If_Settings_Are_Passed()
            {
                var fixture = new MarkdownlintNodeJsRunnerFixture();
                fixture.Settings.ConfigFile = @"c:\foo.log";

                var result = fixture.Run();

                result.Args.ShouldBe("-c \"c:/foo.log\" \"c:/directory-to-lint\"");
            }

            [Fact]
            public void OutputFile_Should_Be_Added_If_Settings_Are_Passed()
            {
                var fixture = new MarkdownlintNodeJsRunnerFixture();
                fixture.Settings.OutputFile = @"c:\foo.log";

                var result = fixture.Run();

                result.Args.ShouldBe("-o \"c:/foo.log\" \"c:/directory-to-lint\"");
            }
        }
    }
}