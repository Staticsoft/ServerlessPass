namespace Staticsoft.SharpPass.Server;

public class DeletePasswordEndpointProxy : ParametrizedHttpEndpoint<EmptyRequestProxy, DeletePasswordResponse>
{
    readonly ParametrizedHttpEndpoint<EmptyRequest, DeletePasswordResponse> Endpoint;

    public DeletePasswordEndpointProxy(ParametrizedHttpEndpoint<EmptyRequest, DeletePasswordResponse> endpoint)
        => Endpoint = endpoint;

    public Task<DeletePasswordResponse> Execute(string parameter, EmptyRequestProxy request)
        => Endpoint.Execute(parameter, request);
}
