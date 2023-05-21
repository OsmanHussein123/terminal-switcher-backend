using ActivityService.service;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace ActivityService.services
{
    public class CommandService: ICommandService
    {
        private DockerClient client;

        public CommandService()
        {
            client = new DockerClientConfiguration(new Uri("tcp://host.docker.internal:2375")).CreateClient();

        }

        public async Task<string> StopContainer(string containerName)
        {
            try
            {
                
                await client.Containers.StopContainerAsync(containerName, new ContainerStopParameters());
                return "ok";
                
            }
            catch (Exception E)
            {
                String SS = E.ToString();
                return null;
            }

        }


    }
}
