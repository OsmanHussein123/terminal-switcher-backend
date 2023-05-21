namespace SecurityService.services
{
    public class DetectionSystem
    {
        readonly List<string> checklist = new();

        public DetectionSystem() 
        {
            const string f = "LFI-WordList-Linux.txt";

            checklist = File.ReadAllLines(f).ToList();
        }

        public bool ControllCommand(string command)
        {
            bool contains = checklist.Contains(command);
            return contains;
        }
    }
}
