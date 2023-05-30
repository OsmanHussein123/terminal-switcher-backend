using ContainerService.data;
using ContainerService.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace terminal_switcher_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly CommandContext _commandContext;
        public CommandController(ApplicationDbContext context, CommandContext commandContext)
        {
            _context = context;
            _commandContext = commandContext;
        }

        // GET api/<CommandController>
        [HttpGet("cmd")]
        public async Task<string> get(string command,string containerName)
        {
            await _commandContext.CreateAsync(new ContainerService.models.Command() { Action = command });
            return await new CommandService().DockerExec(command,containerName);
        }

        // GET api/<CommandController>
        [HttpGet("containers")]
        public async Task<IActionResult> getContainer()
        {

            return Ok(await _context.Users.Include(e => e.Containers).ToListAsync());
        }

        // GET api/<CommandController>
        [HttpGet("commands")]
        public async Task<IActionResult> getCommands()
        {
            return Ok(await _commandContext.GetAsync());
        }

    }
}
