using Staticsoft.Contracts.Abstractions;
using Staticsoft.HttpCommunication.Abstractions;

namespace Staticsoft.SharpPass.Contracts;

public class Schema
{
    public Schema(
        Auth auth,
        Passwords passwords,
        HttpEndpoint<EmptyRequest, Password[]> listPasswords,
        HttpEndpoint<CreatePasswordRequest, Password> createPassword
    )
        => (Auth, Passwords, ListPasswords, CreatePassword)
        = (auth, passwords, listPasswords, createPassword);

    public Auth Auth { get; }
    public Passwords Passwords { get; }

    [Endpoint(HttpMethod.Get, pattern: nameof(Passwords))]
    public HttpEndpoint<EmptyRequest, Password[]> ListPasswords { get; }

    [Endpoint(HttpMethod.Post, pattern: nameof(Passwords))]
    public HttpEndpoint<CreatePasswordRequest, Password> CreatePassword { get; }
}
