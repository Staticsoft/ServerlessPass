namespace Staticsoft.SharpPass.Users.Fakes;

public class MemoryUser : User
{
    readonly MemoryUsers Users;

    public MemoryUser(MemoryUsers users)
        => Users = users;

    public Task Create(string username, string password)
    {
        Users.Add(Code(username, password));
        return Task.CompletedTask;
    }

    public Task<AuthenticatedUser> LogIn(string username, string password)
    {
        if (!Users.Contains(Code(username, password))) throw new LogInException();

        return Task.FromResult(new AuthenticatedUser(username, Secret(username, password)));
    }

    public Task<AuthenticatedUser> LogIn(string refreshToken)
    {
        if (!Users.Contains(Code(refreshToken))) throw new LogInException();

        return Task.FromResult(new AuthenticatedUser(UserName(refreshToken), refreshToken));
    }

    static string UserName(string refreshToken)
        => refreshToken[..refreshToken.IndexOf(':')];

    static int Code(string username, string password)
        => Code(Secret(username, password));

    static string Secret(string username, string password)
        => $"{username}:{password}";

    static int Code(string refreshToken)
        => refreshToken.GetHashCode();
}
