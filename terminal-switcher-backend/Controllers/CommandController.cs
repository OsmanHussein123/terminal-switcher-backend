using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace terminal_switcher_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        
        // GET api/<CommandController>/5
        [HttpGet("cmd")]
        public async Task<string> get(string command)
        {
            return await new command().DockerExec(command);
        }
    }
}
