using Staticsoft.Contracts.Abstractions;
using Staticsoft.HttpCommunication.Abstractions;

namespace Staticsoft.ServerlessPass.Contracts;

public class Auth
{
    public Auth(Jwt jwt, HttpEndpoint<SignUpRequest, SignUpResponse> signUp)
        => (Jwt, SignUp)
        = (jwt, signUp);

    public Jwt Jwt { get; }

    [Endpoint(HttpMethod.Post, pattern: "Users")]
    [EndpointBehavior(statusCode: 201)]
    public HttpEndpoint<SignUpRequest, SignUpResponse> SignUp { get; }
}
