using Staticsoft.PartitionedStorage.Abstractions;

namespace Staticsoft.ServerlessPass.Server;

public class PasswordProfileRepository
{
    readonly ProfilesDocuments Documents;

    public PasswordProfileRepository(ProfilesDocuments documents)
        => Documents = documents;

    public async Task<PasswordProfile[]> Scan()
    {
        var documents = await Documents.Scan();
        return CombineDocumentsProfiles(documents);
    }

    static PasswordProfile[] CombineDocumentsProfiles(Item<PasswordProfilesDocument>[] documents)
    {
        var profilesCount = documents.Sum(document => document.Data.Profiles.Length);
        var profiles = new PasswordProfile[profilesCount];
        var copied = 0;
        foreach (var document in documents)
        {
            Array.Copy(document.Data.Profiles, 0, profiles, copied, document.Data.Profiles.Length);
            copied += document.Data.Profiles.Length;
        }
        return profiles;
    }
}
