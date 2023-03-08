using Staticsoft.Contracts.Abstractions;
using Staticsoft.HttpCommunication.Abstractions;

namespace Staticsoft.SharpPass.Contracts;

public class Passwords
{
    public Passwords(
        HttpEndpoint<EmptyRequest, PasswordProfiles> list,
        HttpEndpoint<CreatePasswordRequest, PasswordProfile> create,
        HttpEndpoint<PasswordProfiles, PasswordProfiles> import,
        ParametrizedHttpEndpoint<UpdatePasswordRequest, PasswordProfile> update,
        ParametrizedHttpEndpoint<EmptyRequest, DeletePasswordResponse> delete,
        ParametrizedHttpEndpoint<UpdatePasswordRequestProxy, PasswordProfile> updateProxy,
        ParametrizedHttpEndpoint<EmptyRequestProxy, DeletePasswordResponse> deleteProxy
    )
        => (List, Create, Import, Update, Delete, UpdateProxy, DeleteProxy)
        = (list, create, import, update, delete, updateProxy, deleteProxy);

    [Endpoint(HttpMethod.Post)]
    public HttpEndpoint<EmptyRequest, PasswordProfiles> List { get; }

    [Endpoint(HttpMethod.Post)]
    [EndpointBehavior(statusCode: 201)]
    public HttpEndpoint<CreatePasswordRequest, PasswordProfile> Create { get; }

    [Endpoint(HttpMethod.Post)]
    public HttpEndpoint<PasswordProfiles, PasswordProfiles> Import { get; }

    [Endpoint(HttpMethod.Post, pattern: "update/{parameter}")]
    public ParametrizedHttpEndpoint<UpdatePasswordRequest, PasswordProfile> Update { get; }

    [Endpoint(HttpMethod.Post, pattern: "delete/{parameter}")]
    [EndpointBehavior(statusCode: 204)]
    public ParametrizedHttpEndpoint<EmptyRequest, DeletePasswordResponse> Delete { get; }

    [Endpoint(HttpMethod.Put)]
    public ParametrizedHttpEndpoint<UpdatePasswordRequestProxy, PasswordProfile> UpdateProxy { get; }

    [Endpoint(HttpMethod.Delete)]
    [EndpointBehavior(statusCode: 204)]
    public ParametrizedHttpEndpoint<EmptyRequestProxy, DeletePasswordResponse> DeleteProxy { get; }
}