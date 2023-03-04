using Staticsoft.SharpPass.Users;

namespace Staticsoft.SharpPass.Server;

public class SignUpEndpoint : HttpEndpoint<SignUpRequest, SignUpResponse>
{
    readonly User User;

    public SignUpEndpoint(User user)
        => User = user;

    public async Task<SignUpResponse> Execute(SignUpRequest request)
    {
        await User.Create(request.Email, request.Password);
        return new();
    }
}
