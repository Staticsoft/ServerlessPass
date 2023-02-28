namespace Staticsoft.SharpPass.Users;

public interface User
{
    Task Create(string username, string password);
    Task<AuthenticatedUser> LogIn(string username, string password);
    Task<AuthenticatedUser> LogIn(string refreshToken);
}
