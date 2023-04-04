using ContainerService.data;
using ContainerService.models;
using ContainerService.services;
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
        private ContainersService _containersService;

        public ContainerController(ApplicationDbContext context)
        {
            _context = context;
            _containersService = new ContainersService();
        }

        // GET api/<CommandController>/5
        [HttpGet]
        public async Task<IActionResult> getContainer()
        {
            List<Container> test = await new command().listContainer();

            List<Container> containers = await _context.Containers.ToListAsync();
            containers.ForEach(async container =>
            {
                await new command().CreateContainer(container);
            });

            return Ok(await new command().listContainer());
        }
    }
}
