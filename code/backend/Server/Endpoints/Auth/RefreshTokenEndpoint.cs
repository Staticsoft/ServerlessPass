using Staticsoft.SharpPass.Users;

namespace Staticsoft.SharpPass.Server;

public class RefreshTokenEndpoint : HttpEndpoint<RefreshJwtRequest, JwtResponse>
{
    readonly User User;

    public RefreshTokenEndpoint(User user)
        => User = user;

    public async Task<JwtResponse> Execute(RefreshJwtRequest request)
    {
        var user = await User.LogIn(request.refresh);
        return new()
        {
            access = user.AccessToken,
            refresh = user.RefreshToken
        };
    }
}
