using Staticsoft.PartitionedStorage.Abstractions;

namespace Staticsoft.SharpPass.Server;

public class ImportPasswordsEndpoint : HttpEndpoint<PasswordProfiles, PasswordProfiles>
{
    readonly ProfilesDocuments Documents;
    readonly PasswordProfilesIdGenerator Id;

    public ImportPasswordsEndpoint(ProfilesDocuments documents, PasswordProfilesIdGenerator id)
        => (Documents, Id)
        = (documents, id);

    public async Task<PasswordProfiles> Execute(PasswordProfiles request)
    {
        var existing = await Documents.Scan();
        foreach (var document in existing)
        {
            await Documents.Remove(document.Id);
        }
        var profiles = request.results.OrderByDescending(profile => profile.created).ToArray();
        for (var i = profiles.Length; i >= 0; i -= MaxProfilesPerDocument)
        {
            await Documents.Save(new()
            {
                Data = new()
                {
                    Profiles = profiles[Math.Max(0, i - MaxProfilesPerDocument)..i]
                },
                Id = Id.Generate(DateTime.UtcNow)
            });
        }
        return request;
    }
}
