using Docker.DotNet;
using Docker.DotNet.Models;
using ContainerService.models;

namespace ContainerService.services
{
    public class command
    {
        private DockerClient client;

        public command()
        {
            client = new DockerClientConfiguration(new Uri("tcp://host.docker.internal:2375")).CreateClient();

        }

        public async Task<string> DockerExec(string command)
        {
            List<string> commands = command.Split(' ').ToList();

            var execCreateParameters = new ContainerExecCreateParameters
            {
                Cmd = commands,
                AttachStdout = true,
                AttachStderr = true,
            };

            var execCreateResponse = await client.Exec.ExecCreateContainerAsync("ubuntu", execCreateParameters, default);

            using (var stdOutAndErrStream = await client.Exec.StartAndAttachContainerExecAsync(execCreateResponse.ID, false, default))
            {
                var (stdout, stderr) = await stdOutAndErrStream.ReadOutputToEndAsync(default);
                return stdout.ToString();
            }
        }
        public async Task<List<Container>> listContainer()
        {
            try
            {
                IList<ContainerListResponse> containers =
                await client.Containers.ListContainersAsync(new ContainersListParameters());
                List<Container> containersList = new List<Container>();
                containers.ToList().ForEach(container =>
                {
                    Container _container = new Container()
                    {
                        ContainerName = container.Names.First(),
                        Image=container.Image
                    };
                    containersList.Add(_container);
                });
                return containersList;
            }
            catch (Exception E)
            {
                String SS = E.ToString();
                return null;
            }

        }

        public async Task<string> CreateContainer(Container container)
        {
            try
            {
                await client.Containers.CreateContainerAsync(new CreateContainerParameters()
                {
                    Image = container.Image,
                    Name = container.ContainerName
                });


                return "ok";
            }
            catch(Exception E) 
            {
                String SS = E.ToString();
                return null;
            }
            
        }

        public async Task<string> StartContainer(Container container)
        {
            try
            {
                var x = await client.Containers.InspectContainerAsync(container.ContainerName);


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
