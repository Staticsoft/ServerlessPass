using Staticsoft.PartitionedStorage.Abstractions;

namespace Staticsoft.SharpPass.Server;

public class ListPasswordsEndpoint : HttpEndpoint<EmptyRequest, PasswordProfiles>
{
    readonly UserDocuments User;

    public ListPasswordsEndpoint(UserDocuments user)
        => User = user;

    public async Task<PasswordProfiles> Execute(EmptyRequest request)
    {
        var documents = await User.Profiles.Scan();
        var profiles = documents.SelectMany(document => document.Data.Profiles).ToArray();
        return new() { results = profiles };
    }
}
