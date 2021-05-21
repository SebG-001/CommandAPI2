using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CommandAPI.Controllers;
using CommandAPI.Data;
using CommandAPI.Models;
using CommandAPI.Profiles;
using CommandAPI.Dtos;

namespace CommandAPI.Tests
{
    public class CommandsControllerTests : IDisposable
    {
        Mock<ICommandAPIRepo> mockRepo;
        CommandsProfile realProfile;
        MapperConfiguration configuration;
        IMapper mapper;

        public CommandsControllerTests()
        {
            mockRepo = new Mock<ICommandAPIRepo>();
            realProfile = new CommandsProfile();
            configuration = new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            mapper = new Mapper(configuration);
        }
        public void Dispose()
        {
            mockRepo = null;
            realProfile = null;
            configuration = null;
            mapper = null;
        }

        private List<Command>GetCommands(int num)
        {
            var lst = new List<Command>();
            if (num > 0)
            {
                lst.Add(new Command()
                {
                    Id = 0,
                    HowTo = "Original HowTo",
                    Platform = "Original Platform",
                    CommandLine = "Original CommandLine"
                });
            }
            return lst;
        }

        // -------
        //  Tests
        // -------

        [Fact]
        public void GetAllCommands_Returns200OK_WhenDBIsEmpty()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(0));
            var controller = new CommandsController(mockRepo.Object, mapper);
            //Act
            var result = controller.GetAllCommands();
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllCommands_ReturnsOneItem_WhenDBHasOneResource()
        {
            // Arrange
            mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
            var controller = new CommandsController(mockRepo.Object, mapper);
            
            var result = controller.GetAllCommands();    // Act

            // Arrange Assert
            var okResult = result.Result as OkObjectResult;            
            var commands = okResult.Value as List<CommandReadDto>;
            // Assert
            Assert.Single(commands);
        }

        [Fact]
        public void GetAllCommands_Returns200OK_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
            var controller = new CommandsController(mockRepo.Object, mapper);
            //Act
            var result = controller.GetAllCommands();
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllCommands_ReturnsCorrectType_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
            var controller = new CommandsController(mockRepo.Object, mapper);
            //Act
            var result = controller.GetAllCommands();
            //Assert
            Assert.IsType<ActionResult<IEnumerable<CommandReadDto>>>(result);
        }


    }
}
