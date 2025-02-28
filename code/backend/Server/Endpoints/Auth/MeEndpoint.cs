using Staticsoft.ServerlessPass.Authentication;

namespace Staticsoft.ServerlessPass.Server;

public class MeEndpoint : HttpEndpoint<EmptyRequest, MeResponse>
{
    readonly Identity Identity;

    public MeEndpoint(Identity identity)
        => Identity = identity;

    public Task<MeResponse> Execute(EmptyRequest request)
        => Task.FromResult(new MeResponse()
        {
            email = Identity.Email,
            id = Identity.UserId
        });
}