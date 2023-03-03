namespace Staticsoft.SharpPass.Server;

public class GetTokenEndpoint : HttpEndpoint<CreateJwtRequest, JwtResponse>
{
    readonly Users.User User;

    public GetTokenEndpoint(Users.User user)
        => User = user;

    public async Task<JwtResponse> Execute(CreateJwtRequest request)
    {
        var user = await User.LogIn(request.Email, request.Password);
        return new()
        {
            Access = user.AccessToken,
            Refresh = user.RefreshToken
        };
    }
}
