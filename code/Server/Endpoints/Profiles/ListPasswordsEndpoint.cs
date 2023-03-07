using Staticsoft.PartitionedStorage.Abstractions;

namespace Staticsoft.SharpPass.Server;

public class ListPasswordsEndpoint : HttpEndpoint<EmptyRequest, PasswordProfiles>
{
    readonly ProfilesDocuments Documents;

    public ListPasswordsEndpoint(ProfilesDocuments documents)
        => Documents = documents;

    public async Task<PasswordProfiles> Execute(EmptyRequest request)
    {
        var documents = await Documents.Scan();
        var profiles = CombineDocumentsProfiles(documents);
        return new() { results = profiles };
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
