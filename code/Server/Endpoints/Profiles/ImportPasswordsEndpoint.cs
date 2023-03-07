using Staticsoft.PartitionedStorage.Abstractions;

namespace Staticsoft.SharpPass.Server;

public class ImportPasswordsEndpoint : HttpEndpoint<PasswordProfiles, PasswordProfiles>
{
    readonly UserDocuments User;
    readonly PasswordProfilesIdGenerator Id;

    public ImportPasswordsEndpoint(UserDocuments user, PasswordProfilesIdGenerator id)
        => (User, Id)
        = (user, id);

    public async Task<PasswordProfiles> Execute(PasswordProfiles request)
    {
        var existing = await User.Profiles.Scan();
        foreach (var document in existing)
        {
            await User.Profiles.Remove(document.Id);
        }
        var profiles = request.results.OrderByDescending(profile => profile.created).ToArray();
        for (var i = profiles.Length; i >= 0; i -= 500)
        {
            await User.Profiles.Save(new()
            {
                Data = new()
                {
                    Profiles = profiles[Math.Max(0, i - 500)..i]
                },
                Id = Id.Generate(DateTime.UtcNow)
            });
        }
        return request;
    }
}
