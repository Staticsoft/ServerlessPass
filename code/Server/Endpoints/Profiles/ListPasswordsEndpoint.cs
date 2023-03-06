using Staticsoft.PartitionedStorage.Abstractions;

namespace Staticsoft.SharpPass.Server;

public class ListPasswordsEndpoint : HttpEndpoint<EmptyRequest, PasswordProfiles>
{
    readonly UserDocuments User;

    public ListPasswordsEndpoint(UserDocuments user)
        => User = user;

    public async Task<PasswordProfiles> Execute(EmptyRequest request)
    {
        var profiles = await User.Profiles.Scan();
        return new() { results = profiles.Select(item => item.Data).ToArray() };
    }
}
