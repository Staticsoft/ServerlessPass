namespace Staticsoft.SharpPass.Server;

public class ImportPasswordsEndpointProxy : HttpEndpoint<PasswordProfilesProxy, PasswordProfiles>
{
    readonly HttpEndpoint<PasswordProfiles, PasswordProfiles> Endpoint;

    public ImportPasswordsEndpointProxy(HttpEndpoint<PasswordProfiles, PasswordProfiles> endpoint)
        => Endpoint = endpoint;

    public Task<PasswordProfiles> Execute(PasswordProfilesProxy request)
        => Endpoint.Execute(request);
}
