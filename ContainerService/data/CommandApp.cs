using ContainerService.models;
using ContainerService.services;
using Docker.DotNet.Models;

namespace ContainerService.data
{
    public class CommandApp
    {
        private readonly ICommandService _service;

        public CommandApp(ICommandService service)
        {
            _service = service;
        }
        public List<Command> GetAll()
        {
            return _service.GetAll().ToList();
        }

        public async Task<Command> Add(Command command)
        {
            return await this._service.Add(command);
        }

    }
}
