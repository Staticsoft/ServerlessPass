using Staticsoft.Contracts.Abstractions;
using Staticsoft.HttpCommunication.Abstractions;

namespace Staticsoft.SharpPass.Contracts;

public class Administrative
{
    public Administrative(
        HttpEndpoint<EmptyRequest, StatusResponse> status
    )
        => Status = status;

    [Endpoint(HttpMethod.Post)]
    public HttpEndpoint<EmptyRequest, StatusResponse> Status { get; }
}
