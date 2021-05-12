using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using CommandAPI.Data;
using CommandAPI.Models;

namespace CommandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandAPIRepo repo;
        public CommandsController(ICommandAPIRepo repository)
        {
            repo = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetAllCommands()
        {
            return Ok(repo.GetAllCommands());
        }

        [HttpGet("{id}")]        
        public ActionResult<Command> GetCommand(int id)
        {
            var result = repo.GetCommandById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
