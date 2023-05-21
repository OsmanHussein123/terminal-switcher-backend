namespace ContainerService.services
{
    public interface IMessageService
    {
        bool EnqueueCommand(string containerName,string command);
    }
}
