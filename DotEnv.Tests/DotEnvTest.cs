// <copyright file="DotEnvTest.cs" company="thiqah.sa">
// Copyright (c) thiqah.sa. All rights reserved.
// </copyright>

namespace DotEnv.Tests
{
    using DNDotEnv;
    using Xunit;

    public class DotEnvTest
    {
        [Fact]
        public void IsThrow_NOT_FOUNT_ERROR()
        {
            string? fullPath = this.GetSolutionPath() + Path.DirectorySeparatorChar+".env";
            Console.WriteLine(fullPath);
            Assert.True(File.Exists(fullPath));
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

        public string? GetSolutionPath()
        {
#nullable disable
            DirectoryInfo solution_dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent;
            return solution_dir?.FullName;
        }
    }
}