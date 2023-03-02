using Staticsoft.SharpPass.Users;
using System.Threading.Tasks;

namespace Staticsoft.SharpPass.Server;

public class RefreshTokenEndpoint : HttpEndpoint<RefreshJwtRequest, JwtResponse>
{
    readonly User User;

    public RefreshTokenEndpoint(User user)
        => User = user;

    public async Task<JwtResponse> Execute(RefreshJwtRequest request)
    {
        var user = await User.LogIn(request.Refresh);
        return new()
        {
            Access = user.AccessToken,
            Refresh = user.RefreshToken
        };
    }
}
