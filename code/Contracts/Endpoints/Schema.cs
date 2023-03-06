namespace Staticsoft.SharpPass.Contracts;

public class Schema
{
    public Schema(Auth auth, Passwords passwords)
        => (Auth, Passwords)
        = (auth, passwords);

    public Auth Auth { get; }
    public Passwords Passwords { get; }
}
