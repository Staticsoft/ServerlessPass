using Staticsoft.PartitionedStorage.Abstractions;
using Staticsoft.SharpPass.Authentication;

namespace Staticsoft.SharpPass.Server;

public class ListPasswordsEndpoint : HttpEndpoint<EmptyRequest, PasswordProfilesResponse>
{
    readonly Documents Storage;
    readonly Identity Identity;

    public ListPasswordsEndpoint(Documents storage, Identity identity)
        => (Storage, Identity) = (storage, identity);

    public async Task<PasswordProfilesResponse> Execute(EmptyRequest request)
    {
        var profiles = await Storage.Profiles.Get(Identity.UserId).Scan();
        return new()
        {
            Results = profiles.Select(item => item.Data).ToArray()
        };
    }
}
