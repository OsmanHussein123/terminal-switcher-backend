namespace ActivityService.service
{
    public interface ICommandService
    {
        Task<string> StopContainer(string containerName);
    }
}
