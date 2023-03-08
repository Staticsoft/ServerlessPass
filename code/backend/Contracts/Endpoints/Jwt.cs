using Staticsoft.Contracts.Abstractions;
using Staticsoft.HttpCommunication.Abstractions;

namespace Staticsoft.SharpPass.Contracts;

public class Jwt
{
    public Jwt(
        HttpEndpoint<CreateJwtRequest, JwtResponse> create,
        HttpEndpoint<RefreshJwtRequest, JwtResponse> refresh
    )
        => (Create, Refresh)
        = (create, refresh);

    [Endpoint(HttpMethod.Post)]
    public HttpEndpoint<CreateJwtRequest, JwtResponse> Create { get; }

    [Endpoint(HttpMethod.Post)]
    public HttpEndpoint<RefreshJwtRequest, JwtResponse> Refresh { get; }
}
