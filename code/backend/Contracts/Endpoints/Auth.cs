using Staticsoft.Contracts.Abstractions;
using Staticsoft.HttpCommunication.Abstractions;

namespace Staticsoft.ServerlessPass.Contracts;

public class Auth
{
    public Auth(Jwt jwt, Users users, HttpEndpoint<SignUpRequest, SignUpResponse> signUp)
        => (Jwt, Users, SignUp)
        = (jwt, users, signUp);

    public Jwt Jwt { get; }

    public Users Users { get; }

    [Endpoint(HttpMethod.Post, pattern: "Users")]
    [EndpointBehavior(statusCode: 201)]
    public HttpEndpoint<SignUpRequest, SignUpResponse> SignUp { get; }
}