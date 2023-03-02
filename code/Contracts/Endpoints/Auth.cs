namespace Staticsoft.SharpPass.Contracts;

public class Auth
{
    public Auth(Jwt jwt)
        => Jwt = jwt;

    public Jwt Jwt { get; }
}
