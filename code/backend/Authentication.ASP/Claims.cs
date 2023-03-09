namespace Staticsoft.ServerlessPass.Authentication.ASP;

public class Claims
{
    readonly IDictionary<string, string> IdentityClaims;

    public Claims(IDictionary<string, string> identityClaims)
        => IdentityClaims = identityClaims;

    public string Get(string claimName)
        => IdentityClaims[claimName];
}
