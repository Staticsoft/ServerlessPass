namespace Staticsoft.SharpPass.Users.Fakes;

public class MemoryUsers
{
    readonly HashSet<int> Users = new();

    public void Add(int userHash)
        => Users.Add(userHash);

    public bool Contains(int userHash)
        => Users.Contains(userHash);
}
