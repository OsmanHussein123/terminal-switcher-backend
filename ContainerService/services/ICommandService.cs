using ContainerService.models;

namespace ContainerService.services
{
    public interface ICommandService
    {
        IQueryable<Command> GetAll();
        Task<Command> Add(Command command);
  
    }
}
