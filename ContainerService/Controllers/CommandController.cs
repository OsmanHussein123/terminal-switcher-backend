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

        public CommandController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/<CommandController>/5
        [HttpGet("cmd")]
        public async Task<string> get(string command)
        {
            return await new command().DockerExec(command);
        }

        // GET api/<CommandController>/5
        [HttpGet("containers")]
        public async Task<IActionResult> getContainer(string command)
        {
            return Ok(await _context.Users.Include(e => e.Containers).ToListAsync());
        }
    }
}
