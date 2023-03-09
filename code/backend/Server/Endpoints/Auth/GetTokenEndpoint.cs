using Staticsoft.ServerlessPass.Users;

namespace Staticsoft.ServerlessPass.Server;

public class GetTokenEndpoint : HttpEndpoint<CreateJwtRequest, JwtResponse>
{
    readonly User User;

    public GetTokenEndpoint(User user)
        => User = user;

    public async Task<JwtResponse> Execute(CreateJwtRequest request)
    {
        var user = await User.LogIn(request.email, request.password);
        return new()
        {
            access = user.AccessToken,
            refresh = user.RefreshToken
        };
    }
}
