using Staticsoft.Contracts.Abstractions;
using Staticsoft.HttpCommunication.Abstractions;

namespace Staticsoft.ServerlessPass.Contracts;

public class Schema
{
    public Schema(
        Auth auth,
        Passwords passwords,
        HttpEndpoint<EmptyRequestProxy, PasswordProfiles> listProxy,
        HttpEndpoint<CreatePasswordRequestProxy, PasswordProfile> createProxy,
        HttpEndpoint<PasswordProfilesProxy, PasswordProfiles> importProxy
    )
        => (Auth, Passwords, ListProxy, CreateProxy, ImportProxy)
        = (auth, passwords, listProxy, createProxy, importProxy);

    public Auth Auth { get; }
    public Passwords Passwords { get; }

    [Endpoint(HttpMethod.Get, pattern: "passwords")]
    public HttpEndpoint<EmptyRequestProxy, PasswordProfiles> ListProxy { get; }

    [Endpoint(HttpMethod.Post, pattern: "passwords")]
    public HttpEndpoint<CreatePasswordRequestProxy, PasswordProfile> CreateProxy { get; }

    [Endpoint(HttpMethod.Put, pattern: "passwords")]
    public HttpEndpoint<PasswordProfilesProxy, PasswordProfiles> ImportProxy { get; }
}
