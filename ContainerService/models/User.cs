namespace ContainerService.models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Container> Containers { get; } = new List<Container>();


    }
}
