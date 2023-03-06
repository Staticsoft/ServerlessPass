using Staticsoft.PartitionedStorage.Abstractions;

namespace Staticsoft.SharpPass.Server;

public class CreatePasswordEndpoint : HttpEndpoint<CreatePasswordRequest, PasswordProfile>
{
    readonly UserDocuments User;
    readonly PasswordProfileIdGenerator Id;

    public CreatePasswordEndpoint(UserDocuments user, PasswordProfileIdGenerator id)
        => (User, Id)
        = (user, id);

    public async Task<PasswordProfile> Execute(CreatePasswordRequest request)
    {
        var createdDate = DateTime.UtcNow;
        var passwordId = Id.Generate(createdDate);
        var profile = new PasswordProfile()
        {
            id = passwordId,
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
        await User.Profiles.Save(new Item<PasswordProfile>()
        {
            Data = profile,
            Id = passwordId
        });
        return profile;
    }
}
