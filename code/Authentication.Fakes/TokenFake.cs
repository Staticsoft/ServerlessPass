namespace Staticsoft.SharpPass.Authentication.Fakes;

public class TokenFake : Token
{
    public Identity Parse(string accessToken)
        => new(accessToken);
}
