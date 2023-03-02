using Staticsoft.PartitionedStorage.Abstractions;
using Staticsoft.SharpPass.Authentication;

namespace Staticsoft.SharpPass.Server;

public class ListPasswordsEndpoint : HttpEndpoint<EmptyRequest, PasswordProfiles>
{
    readonly Documents Storage;
    readonly Identity Identity;

    public ListPasswordsEndpoint(Documents storage, Identity identity)
        => (Storage, Identity) = (storage, identity);

    public async Task<PasswordProfiles> Execute(EmptyRequest request)
    {
        var profiles = await Storage.Profiles.Get($"{nameof(PasswordProfile)}{Identity.UserId}").Scan();
        return new()
        {
            Results = profiles.Select(item => item.Data).ToArray()
        };
    }
}
