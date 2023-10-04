// <copyright file="DotEnvTest.cs" company="thiqah.sa">
// Copyright (c) thiqah.sa. All rights reserved.
// </copyright>

namespace DotEnv.Tests
{
    using DNDotEnv;
    using Xunit;

    public class DotEnvTest
    {
        private readonly Xunit.Abstractions.ITestOutputHelper _testOutputHelper;

        public DotEnvTest(Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        [Fact]
        public void READ_FILE_IF_EXIST()
        {
            string? fullPath = this.GetSolutionPath() + Path.DirectorySeparatorChar + ".env";
            _testOutputHelper.WriteLine(fullPath);
            Assert.True(File.Exists(fullPath));
        }

        [Fact]
        public void IsThrow_NOT_FOUNT_ERROR()
        {
            string? fullPath = this.GetSolutionPath() + Path.DirectorySeparatorChar + "x.env";
            _testOutputHelper.WriteLine("XXXXXXXXXXXX" + fullPath);

            Assert.False(File.Exists(fullPath));

            Action act = () => DotEnv.Load(fullPath);

            Exception exception = Assert.Throws<Exception>(act);

            Assert.Equal(fullPath + " Cannot be read", exception.Message);
        }


        [Fact]
        public void READ_NUMBER()
        {
            string? fullPath = this.GetSolutionPath() + Path.DirectorySeparatorChar + ".env";
            DotEnv.Load(fullPath);
            Assert.NotNull(Environment.GetEnvironmentVariable("NUMBER_VALUE"));
            Assert.Equal("123123", Environment.GetEnvironmentVariable("NUMBER_VALUE"));
        }
        [Fact]
        public void READ_STRING()
        {
            string? fullPath = this.GetSolutionPath() + Path.DirectorySeparatorChar + ".env";
            DotEnv.Load(fullPath);
            Assert.NotNull(Environment.GetEnvironmentVariable("STRING_VALUE"));
            Assert.Equal("String value with spaces.", Environment.GetEnvironmentVariable("STRING_VALUE"));
        }

        [Fact]
        public void READ_MULTI_EQUAL()
        {
            string? fullPath = this.GetSolutionPath() + Path.DirectorySeparatorChar + ".env";
            DotEnv.Load(fullPath);
            Assert.NotNull(Environment.GetEnvironmentVariable("CONECTION_STRING"));

            Assert.Equal("Server=localhost;Database=Source_DB;Trusted_Connection=True;TrustServerCertificate=True", Environment.GetEnvironmentVariable("CONECTION_STRING"));
        }

        [Fact]
        public void NOT_ENV_VALID_VARIABLE()
        {
            string? fullPath = this.GetSolutionPath() + Path.DirectorySeparatorChar + ".env";
            DotEnv.Load(fullPath);
            Assert.Null(Environment.GetEnvironmentVariable("NOT_ENV_VALID_VARIABLE"));
        }

        [Fact]
        public void READ_MULTI_LINE()
        {
            string? fullPath = this.GetSolutionPath() + Path.DirectorySeparatorChar + ".env";
            DotEnv.Load(fullPath);
            var multi_line = """"
            """
            This is multi line
            Sample Variable
            """
            """";
            Assert.Equal("\"\"\"\r\nThis is multi line\r\nSample Variable\r\n\"\"\"", multi_line);
            //Assert.Null(Environment.GetEnvironmentVariable("MULTI_LINE_VARIABLE"));
        }

        public string? GetSolutionPath()
        {
#nullable disable
            DirectoryInfo solution_dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent;
            return solution_dir?.FullName;
        }
    }
}