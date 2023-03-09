using Staticsoft.ServerlessPass.Services;

namespace Staticsoft.ServerlessPass.Server;

public class StatusEndpoint : HttpEndpoint<EmptyRequest, StatusResponse>
{
    readonly IEnumerable<ServiceStatus> Services;

    public StatusEndpoint(IEnumerable<ServiceStatus> services)
        => Services = services;

    public async Task<StatusResponse> Execute(EmptyRequest request)
    {
        await Task.WhenAll(Services.Select(service => service.Check()));
        return new();
    }
}
