using Staticsoft.PartitionedStorage.Abstractions;

namespace Staticsoft.SharpPass.Server;

public class ImportPasswordsEndpoint : HttpEndpoint<PasswordProfiles, PasswordProfiles>
{
    readonly UserProfiles Profiles;
    readonly PasswordProfilesIdGenerator Id;

    public ImportPasswordsEndpoint(UserProfiles profiles, PasswordProfilesIdGenerator id)
        => (Profiles, Id)
        = (profiles, id);

    public async Task<PasswordProfiles> Execute(PasswordProfiles request)
    {
        var existing = await Profiles.Scan();
        foreach (var document in existing)
        {
            await Profiles.Remove(document.Id);
        }
        var profiles = request.results.OrderByDescending(profile => profile.created).ToArray();
        for (var i = profiles.Length; i >= 0; i -= 500)
        {
            await Profiles.Save(new()
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
