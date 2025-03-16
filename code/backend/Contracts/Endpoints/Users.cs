using Staticsoft.Contracts.Abstractions;
using Staticsoft.HttpCommunication.Abstractions;

namespace Staticsoft.ServerlessPass.Contracts;

public class Users
{
    public Users(HttpEndpoint<EmptyRequest, MeResponse> me)
        => Me = me;

    [Endpoint(HttpMethod.Get)]
    public HttpEndpoint<EmptyRequest, MeResponse> Me { get; }
}