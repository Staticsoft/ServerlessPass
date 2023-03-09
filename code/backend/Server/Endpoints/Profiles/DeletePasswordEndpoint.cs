using Staticsoft.PartitionedStorage.Abstractions;

namespace Staticsoft.ServerlessPass.Server;

public class DeletePasswordEndpoint : ParametrizedHttpEndpoint<EmptyRequest, DeletePasswordResponse>
{
    readonly ProfilesDocuments Documents;

    public DeletePasswordEndpoint(ProfilesDocuments documents)
        => Documents = documents;

    public async Task<DeletePasswordResponse> Execute(string profileId, EmptyRequest _)
    {
        var documents = await Documents.Scan();
        var (document, profileIndex) = documents.FindProfileDocument(profileId);
        await Documents.Save(new Item<PasswordProfilesDocument>()
        {
            Data = new PasswordProfilesDocument()
            {
                Profiles = RemoveProfile(document.Data.Profiles, profileIndex)
            },
            Id = document.Id,
            Version = document.Version
        });
        return new();
    }

    static PasswordProfile[] RemoveProfile(PasswordProfile[] profiles, int profileIndex)
    {
        var left = profiles[..profileIndex];
        var right = profiles[(profileIndex + 1)..];
        var updatedProfiles = new PasswordProfile[left.Length + right.Length];
        Array.Copy(left, updatedProfiles, left.Length);
        Array.Copy(right, 0, updatedProfiles, left.Length, right.Length);
        return updatedProfiles;
    }
}
