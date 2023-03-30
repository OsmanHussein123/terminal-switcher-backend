namespace LoginService.services
{
    public interface ITokenBuilder
    {
        string BuildToken(string username);
    }
}
