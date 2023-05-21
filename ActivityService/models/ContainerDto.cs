using ActivityService.services;
using System.Threading;
using System.Timers;

namespace ContainerService.models
{
    public class ContainerDto
    {
        public string ContainerName { get; set; }

        public string command { get; set; }

        public System.Timers.Timer timer;

        public ContainerDto() 
        {
            timer = new System.Timers.Timer();
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timer.Interval = 6000;
        }

        private async void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            await new CommandService().StopContainer(ContainerName);
        }
    }
}
