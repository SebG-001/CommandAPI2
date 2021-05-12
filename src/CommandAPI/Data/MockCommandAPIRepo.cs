﻿using CommandAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAPI.Data
{
    public class MockCommandAPIRepo : ICommandAPIRepo
    {
        public void CreateCommand(Command cmd)
        {
            throw new NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return new List<Command> 
            {
                new Command { 
                    Id=0, HowTo="How to generate a migration",
                    CommandLine="dotnet ef migrations add <Name of Migration>",
                    Platform=".Net Core EF" },
                new Command{
                    Id=1, HowTo="Run Migrations",
                    CommandLine="dotnet ef database update",
                    Platform=".Net Core EF"},
                new Command{
                    Id=2, HowTo="List active migrations",
                    CommandLine="dotnet ef migrations list",
                    Platform=".Net Core EF"}
            };
        }

        public Command GetCommandById(int id)
        {
            if (id == 0) return new Command
            {
                Id = 0,
                HowTo = "How to generate a migration",
                CommandLine = "dotnet ef migrations add <Name of Migration>",
                Platform = ".Net Core EF"
            };
            else if (id == 1) return new Command
            {
                Id = 1,
                HowTo = "Run Migrations",
                CommandLine = "dotnet ef database update",
                Platform = ".Net Core EF"
            };
            else if (id == 2) return new Command
            {
                Id = 2,
                HowTo = "List active migrations",
                CommandLine = "dotnet ef migrations list",
                Platform = ".Net Core EF"
            };
            else return null;
        }
        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new NotImplementedException();
        }
    }
}
