namespace Staticsoft.SharpPass.Contracts;

public class Schema
{
    public Schema(Auth auth, Passwords passwords, Administrative administrative)
        => (Auth, Passwords, Administrative)
        = (auth, passwords, administrative);

    public Auth Auth { get; }
    public Passwords Passwords { get; }
    public Administrative Administrative { get; }
}
