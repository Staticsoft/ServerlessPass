using Staticsoft.PartitionedStorage.Abstractions;

namespace Staticsoft.SharpPass.Server;

public class ListPasswordsEndpoint : HttpEndpoint<EmptyRequest, PasswordProfiles>
{
    readonly UserProfiles Profiles;

    public ListPasswordsEndpoint(UserProfiles profiles)
        => Profiles = profiles;

    public async Task<PasswordProfiles> Execute(EmptyRequest request)
    {
        var documents = await Profiles.Scan();
        var profiles = documents.SelectMany(document => document.Data.Profiles).ToArray();
        return new() { results = profiles };
    }
}
