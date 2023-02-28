namespace Staticsoft.SharpPass.Authentication;

public interface Token
{
    public Identity Parse(string accessToken);
}
