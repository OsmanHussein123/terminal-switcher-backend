using ContainerService.data;
using ContainerService.models;
using ContainerService.services;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContainerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContainerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        

        public ContainerController(ApplicationDbContext context)
        {
            _context = context;
            
        }

        // GET api/<CommandController>/5
        [HttpGet]
        public async Task<IActionResult> getContainer()
        {
            return Ok(await new CommandService().listContainer());
        }

        [HttpGet("start")]
        public async Task<IActionResult> startContainer(string id)
        {
            User user = _context.Users.Include(x => x.Containers).First(x => x.Id == int.Parse(id));
            if (user != null) 
            {
                user.Containers.ForEach(async container =>
                {
                    await new CommandService().StartContainer(container);
                });
            }
            return Ok(user.Containers);
        }
    }
}
