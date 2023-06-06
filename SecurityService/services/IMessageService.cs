namespace SecurityService.services
{
    public interface IMessageService
    {
        bool EnqueueStop(string containerName);
    }
}
