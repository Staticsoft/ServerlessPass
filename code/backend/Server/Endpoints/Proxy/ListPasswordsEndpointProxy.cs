namespace Staticsoft.ServerlessPass.Server;

public class ListPasswordsEndpointProxy : HttpEndpoint<EmptyRequestProxy, PasswordProfiles>
{
    readonly HttpEndpoint<EmptyRequest, PasswordProfiles> Endpoint;

    public ListPasswordsEndpointProxy(HttpEndpoint<EmptyRequest, PasswordProfiles> endpoint)
        => Endpoint = endpoint;

    public Task<PasswordProfiles> Execute(EmptyRequestProxy request)
        => Endpoint.Execute(request);
}
