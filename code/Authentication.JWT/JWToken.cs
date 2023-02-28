using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Staticsoft.SharpPass.Authentication.JWT;

public class JWToken : Token
{
    readonly JwtSecurityTokenHandler JWT = new();

    public Identity Parse(string accessToken)
        => new(Subject(accessToken));

    string Subject(string accessToken)
        => Claims(accessToken).First(claim => claim.Type == "sub").Value;

    IEnumerable<Claim> Claims(string accessToken)
        => JWT.ReadJwtToken(accessToken).Claims;
}
