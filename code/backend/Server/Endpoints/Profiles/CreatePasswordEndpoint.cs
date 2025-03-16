using Staticsoft.PartitionedStorage.Abstractions;

namespace Staticsoft.ServerlessPass.Server;

public class CreatePasswordEndpoint : HttpEndpoint<CreatePasswordRequest, PasswordProfile>
{
    readonly ProfilesDocuments Documents;
    readonly PasswordProfilesIdGenerator Id;

    public CreatePasswordEndpoint(ProfilesDocuments documents, PasswordProfilesIdGenerator id)
        => (Documents, Id)
        = (documents, id);

    public async Task<PasswordProfile> Execute(CreatePasswordRequest request)
    {
        var profile = ToNewProfile(request);
        var documents = await Documents.Scan(new ScanOptions { MaxItems = 1 });
        var document = documents.FirstOrDefault();
        if (document != null && document.Data.Profiles.Length < MaxProfilesPerDocument)
        {
            await CreateProfile(document, profile);
        }
        else
        {
            await CreateProfile(profile);
        }
        return profile;
    }

    Task CreateProfile(Item<PasswordProfilesDocument> document, PasswordProfile profile)
    {
        var updatedProfiles = new PasswordProfile[document.Data.Profiles.Length + 1];
        updatedProfiles[0] = profile;
        Array.Copy(document.Data.Profiles, 0, updatedProfiles, 1, document.Data.Profiles.Length);
        return Documents.Save(new()
        {
            Data = new()
            {
                Profiles = updatedProfiles,
            },
            Id = document.Id,
            Version = document.Version
        });
    }

    Task CreateProfile(PasswordProfile profile)
        => Documents.Save(new()
        {
            Data = new()
            {
                Profiles = new[] { profile }
            },
            Id = Id.Generate(DateTime.UtcNow)
        });

    static PasswordProfile ToNewProfile(CreatePasswordRequest request)
    {
        var createdDate = DateTime.UtcNow;
        var profile = new PasswordProfile()
        {
            id = $"{Guid.NewGuid()}",
            created = $"{createdDate:O}",
            modified = $"{createdDate:O}",
            site = request.site,
            login = request.login,
            uppercase = request.uppercase,
            lowercase = request.lowercase,
            digits = request.digits,
            symbols = request.symbols,
            length = request.length,
            counter = request.counter,
            version = request.version
        };
        return profile;
    }
}
