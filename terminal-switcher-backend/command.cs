using Docker.DotNet;
using Docker.DotNet.Models;

namespace terminal_switcher_backend
{
    public class command
    {
        private DockerClient client;

        public command() 
        {
            client = new DockerClientConfiguration().CreateClient();
            
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

            var execCreateResponse = await client.Exec.ExecCreateContainerAsync("ubuntu", execCreateParameters, default(CancellationToken));

            using (var stdOutAndErrStream = await client.Exec.StartAndAttachContainerExecAsync(execCreateResponse.ID, false, default(CancellationToken)))
            {
                var (stdout, stderr) = await stdOutAndErrStream.ReadOutputToEndAsync(default(CancellationToken));
                return stdout.ToString();
            }
        }


        public string sendCommand(string command) 
        {
            if(command == "id")
            {
                return "osman";
            }

            if(command == "ls")
            {
                return "list of files";
            }

            return "unkown command";
        }
    }
}
