using Staticsoft.PartitionedStorage.Abstractions;

namespace Staticsoft.SharpPass.Server;

public class CreatePasswordEndpoint : HttpEndpoint<CreatePasswordRequest, PasswordProfile>
{
    readonly UserProfiles Profiles;
    readonly PasswordProfilesIdGenerator Id;

    public CreatePasswordEndpoint(UserProfiles profiles, PasswordProfilesIdGenerator id)
        => (Profiles, Id)
        = (profiles, id);

    public async Task<PasswordProfile> Execute(CreatePasswordRequest request)
    {
        var profile = ToNewProfile(request);
        var documents = await Profiles.Scan(new ScanOptions { MaxItems = 1 });
        var document = documents.FirstOrDefault();
        if (document != null && document.Data.Profiles.Length < 500)
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
        return Profiles.Save(new()
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
        => Profiles.Save(new()
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
            numbers = request.numbers,
            symbols = request.symbols,
            length = request.length,
            counter = request.counter,
            version = request.version
        };
        return profile;
    }
}
