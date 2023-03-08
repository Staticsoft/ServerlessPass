using Staticsoft.PartitionedStorage.Abstractions;

namespace Staticsoft.SharpPass.Server;

public class UpdatePasswordEndpoint : ParametrizedHttpEndpoint<UpdatePasswordRequest, PasswordProfile>
{
    readonly ProfilesDocuments Documents;

    public UpdatePasswordEndpoint(ProfilesDocuments documents)
        => Documents = documents;

    public async Task<PasswordProfile> Execute(string profileId, UpdatePasswordRequest request)
    {
        var documents = await Documents.Scan();
        var (document, profileIndex) = documents.FindProfileDocument(profileId);
        return await UpdateProfile(document, request, profileIndex);
    }

    async Task<PasswordProfile> UpdateProfile(Item<PasswordProfilesDocument> document, UpdatePasswordRequest request, int profileIndex)
    {
        var profile = GetUpdatedProfile(document, request, profileIndex);
        document.Data.Profiles[profileIndex] = profile;

        await Documents.Save(new Item<PasswordProfilesDocument>()
        {
            Data = document.Data,
            Id = document.Id,
            Version = document.Version
        });

        return profile;
    }

    static PasswordProfile GetUpdatedProfile(Item<PasswordProfilesDocument> document, UpdatePasswordRequest request, int profileIndex) => new()
    {
        id = document.Data.Profiles[profileIndex].id,
        created = document.Data.Profiles[profileIndex].created,
        modified = $"{DateTime.UtcNow:O}",
        site = request.site,
        login = request.login,
        uppercase = request.uppercase,
        lowercase = request.lowercase,
        numbers = request.numbers,
        symbols = request.symbols,
        length = request.length,
        counter = request.counter,
        version = request.version
    };
}