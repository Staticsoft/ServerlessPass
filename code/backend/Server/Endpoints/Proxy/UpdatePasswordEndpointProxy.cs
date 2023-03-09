namespace Staticsoft.ServerlessPass.Server;

public class UpdatePasswordEndpointProxy : ParametrizedHttpEndpoint<UpdatePasswordRequestProxy, PasswordProfile>
{
    readonly ParametrizedHttpEndpoint<UpdatePasswordRequest, PasswordProfile> Endpoint;

    public UpdatePasswordEndpointProxy(ParametrizedHttpEndpoint<UpdatePasswordRequest, PasswordProfile> endpoint)
        => Endpoint = endpoint;

    public Task<PasswordProfile> Execute(string parameter, UpdatePasswordRequestProxy request)
        => Endpoint.Execute(parameter, request);
}
