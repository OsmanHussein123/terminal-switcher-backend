using ContainerService.models;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace ContainerService.services
{
    public class ContainersService
    {
        private DockerClient client;

        public ContainersService()
        {
            client = new DockerClientConfiguration().CreateClient();
        }

        public async Task<List<Container>> listContainer()
        {
            try
            {
                IList<ContainerListResponse> containers =
                await client.Containers.ListContainersAsync(new ContainersListParameters()
                {
                    Limit = 10,
                });
                List<Container> containersList = new List<Container>();
                containers.ToList().ForEach(container =>
                {
                    Container _container = new Container()
                    {
                        ContainerName = container.ID
                    };
                    containersList.Add(_container);
                });
                return containersList;
            }
            catch(Exception E)
            {
                String SS = E.ToString();
                return null;
            }
            
        }

        public async Task<string> CreateContainer(Container container)
        {
            await client.Containers.CreateContainerAsync(new CreateContainerParameters()
            {
                Image = container.Image,
                Name = container.ContainerName,
                HostConfig = new HostConfig()
                {
                    DNS = new[] { "8.8.8.8", "8.8.4.4" }
                }
            });


            return "ok";
        }
    }
}
