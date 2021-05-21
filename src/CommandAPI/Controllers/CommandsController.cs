using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using CommandAPI.Data;
using CommandAPI.Models;
using CommandAPI.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;


namespace CommandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandAPIRepo repo;
        private readonly IMapper mapper;
        public CommandsController(ICommandAPIRepo repository, IMapper mapper)
        {
            repo = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            return Ok(mapper.Map<IEnumerable<CommandReadDto>>(repo.GetAllCommands()));
        }

        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var result = repo.GetCommandById(id);
            if (result == null)
                return NotFound();
            return Ok(mapper.Map<CommandReadDto>(result));
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto cmdCreateDto)
        {
            var cmd = mapper.Map<Command>(cmdCreateDto);
            repo.CreateCommand(cmd);
            repo.SaveChanges();  // This also updates the context and the objects passed to it including cmd.
            var cmdReadDto = mapper.Map<CommandReadDto>(cmd);
            return CreatedAtRoute(nameof(GetCommandById), new { Id = cmdReadDto.Id }, cmdReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto cmdUpdateDto)
        {
            var cmd = repo.GetCommandById(id);
            if (cmd == null)
                return NotFound();
            // cmd is a reference to the object stored in the context
            mapper.Map(cmdUpdateDto, cmd);  // This updates the context since it updates cmd
            repo.UpdateCommand(cmd);
            repo.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var cmd = repo.GetCommandById(id);
            if (cmd == null)
                return NotFound();
            var cmdToPatch = mapper.Map<CommandUpdateDto>(cmd);
            patchDoc.ApplyTo(cmdToPatch, ModelState);
            if (!TryValidateModel(cmdToPatch))
                return ValidationProblem(ModelState);
            mapper.Map(cmdToPatch, cmd);
            repo.UpdateCommand(cmd);
            repo.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var cmd = repo.GetCommandById(id);
            if (cmd == null)
                return NotFound();
            repo.DeleteCommand(cmd);
            repo.SaveChanges();
            return NoContent();
        }
    }
}
