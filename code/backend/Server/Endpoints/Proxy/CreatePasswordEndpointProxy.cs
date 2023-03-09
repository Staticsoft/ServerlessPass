namespace Staticsoft.ServerlessPass.Server;

public class CreatePasswordEndpointProxy : HttpEndpoint<CreatePasswordRequestProxy, PasswordProfile>
{
    readonly HttpEndpoint<CreatePasswordRequest, PasswordProfile> Endpoint;

    public CreatePasswordEndpointProxy(HttpEndpoint<CreatePasswordRequest, PasswordProfile> endpoint)
        => Endpoint = endpoint;

    public Task<PasswordProfile> Execute(CreatePasswordRequestProxy request)
        => Endpoint.Execute(request);
}
