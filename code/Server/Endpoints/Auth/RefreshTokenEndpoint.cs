namespace Staticsoft.SharpPass.Server;

public class RefreshTokenEndpoint : HttpEndpoint<RefreshJwtRequest, JwtResponse>
{
    readonly Users.User User;

    public RefreshTokenEndpoint(Users.User user)
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
