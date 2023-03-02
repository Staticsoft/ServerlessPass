using Staticsoft.SharpPass.Authentication;

namespace Staticsoft.SharpPass.Server;

public class DeletePasswordEndpoint : ParametrizedHttpEndpoint<EmptyRequest, DeletePasswordResponse>
{
    readonly Documents Storage;
    readonly Identity Identity;

    public DeletePasswordEndpoint(Documents storage, Identity identity)
        => (Storage, Identity)
        = (storage, identity);

    public async Task<DeletePasswordResponse> Execute(string passwordId, EmptyRequest request)
    {
        await Storage.Profiles.Get(Identity.UserId).Remove(passwordId);
        return new();
    }
}
