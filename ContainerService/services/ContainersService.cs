using Docker.DotNet;
using Docker.DotNet.Models;
using ContainerService.models;

namespace ContainerService.services
{
    public class ContainersService
    {
        private DockerClient client;

        public ContainersService()
        {
            client = new DockerClientConfiguration(new Uri("tcp://host.docker.internal:2375")).CreateClient();

        }

        public async Task<string> DockerExec(string command,string containerName)
        {
            try
            {
                new MessageService().EnqueueCommand(containerName, command);
            List<string> commands = command.Split(' ').ToList();
            String task = await StartContainer(new Container() { ContainerName = containerName });
            if(task != null) 
            {
                var execCreateParameters = new ContainerExecCreateParameters
                {
                    Cmd = commands,
                    AttachStdout = true,
                    AttachStderr = true,
                };

                var execCreateResponse = await client.Exec.ExecCreateContainerAsync(containerName, execCreateParameters, default);

                using (var stdOutAndErrStream = await client.Exec.StartAndAttachContainerExecAsync(execCreateResponse.ID, false, default))
                {
                    var (stdout, stderr) = await stdOutAndErrStream.ReadOutputToEndAsync(default);
                    return stdout.ToString();
                }
            }
            return "";

            }catch (Exception ex)
            {
                string e = ex.ToString();
                return null;
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
                    Name = container.ContainerName,
                    Tty=true,
                    AttachStdin=true,
                    AttachStdout=true,
                    AttachStderr=true,
                    
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
                
                await client.Containers.StartContainerAsync(container.ContainerName, new ContainerStartParameters());
                return "ok";
                
            }
            catch (Exception E)
            {
                String SS = E.ToString();
                return null;
            }

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
