using Staticsoft.PartitionedStorage.Abstractions;

namespace Staticsoft.SharpPass.Server;

public class UpdatePasswordEndpoint : ParametrizedHttpEndpoint<UpdatePasswordRequest, PasswordProfile>
{
    readonly UserDocuments User;

    public UpdatePasswordEndpoint(UserDocuments user)
        => User = user;

    public async Task<PasswordProfile> Execute(string passwordId, UpdatePasswordRequest request)
    {
        var documents = await User.Profiles.Scan();
        var (document, index) = FindProfile(documents, passwordId);
        return await UpdateProfile(document, request, index);
    }

    static (Item<PasswordProfilesDocument>, int) FindProfile(Item<PasswordProfilesDocument>[] documents, string passwordId)
    {
        foreach (var document in documents)
        {
            var index = FindProfileIndex(document.Data, passwordId);
            if (index != -1) return (document, index);
        }

        throw new NotSupportedException();
    }

    static int FindProfileIndex(PasswordProfilesDocument document, string passwordId)
    {
        for (var i = 0; i < document.Profiles.Length; i++)
        {
            if (document.Profiles[i].id == passwordId)
            {
                return i;
            }
        }
        return -1;
    }

    async Task<PasswordProfile> UpdateProfile(Item<PasswordProfilesDocument> document, UpdatePasswordRequest request, int index)
    {
        var profile = GetUpdatedProfile(document, request, index);
        document.Data.Profiles[index] = profile;

        await User.Profiles.Save(new Item<PasswordProfilesDocument>()
        {
            Data = document.Data,
            Id = document.Id,
            Version = document.Version
        });

        return profile;
    }

    static PasswordProfile GetUpdatedProfile(Item<PasswordProfilesDocument> document, UpdatePasswordRequest request, int index) => new PasswordProfile()
    {
        id = document.Data.Profiles[index].id,
        created = document.Data.Profiles[index].created,
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