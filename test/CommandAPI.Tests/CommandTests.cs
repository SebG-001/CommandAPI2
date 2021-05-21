using System;
using Xunit;
using CommandAPI.Models;

namespace CommandAPI.Tests
{
    public class CommandTests : IDisposable
    {
        private Command testCommand;
        public CommandTests()
        {
            testCommand = new Command()
            {
                HowTo = "Original HowTo",
                Platform = "Original Platform",
                CommandLine = "Original CommandLine"
            };
        }
        public void Dispose()
        {
            testCommand = null;
        }

        [Fact]
        public void CanChangeHowTo()
        {
            testCommand.HowTo = "Assigned How To";
            Assert.Equal("Assigned How To", testCommand.HowTo);
        }
        [Fact]
        public void CanChangePlatform()
        {
            testCommand.Platform = "Assigned platform";
            Assert.Equal("Assigned platform", testCommand.Platform);
        }
        [Fact]
        public void CanChangeCommandLine()
        {
            testCommand.CommandLine = "Assigned CLI property";
            Assert.Equal("Assigned CLI property", testCommand.CommandLine);
        }
    }
}
