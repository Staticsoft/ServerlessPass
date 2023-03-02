using Staticsoft.Contracts.Abstractions;
using Staticsoft.HttpCommunication.Abstractions;

namespace Staticsoft.SharpPass.Contracts;

public class Passwords
{
    public Passwords(
        ParametrizedHttpEndpoint<UpdatePasswordRequest, Password> update,
        ParametrizedHttpEndpoint<EmptyRequest, DeletePasswordResponse> delete
    )
        => (Update, Delete)
        = (update, delete);

    [Endpoint(HttpMethod.Put)]
    public ParametrizedHttpEndpoint<UpdatePasswordRequest, Password> Update { get; }

    [Endpoint(HttpMethod.Delete)]
    [EndpointBehavior(statusCode: 204)]
    public ParametrizedHttpEndpoint<EmptyRequest, DeletePasswordResponse> Delete { get; }
}
