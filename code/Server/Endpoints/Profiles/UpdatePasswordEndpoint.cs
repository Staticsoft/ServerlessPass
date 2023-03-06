using Staticsoft.PartitionedStorage.Abstractions;

namespace Staticsoft.SharpPass.Server;

public class UpdatePasswordEndpoint : ParametrizedHttpEndpoint<UpdatePasswordRequest, PasswordProfile>
{
    readonly UserDocuments User;

    public UpdatePasswordEndpoint(UserDocuments user)
        => User = user;

    public async Task<PasswordProfile> Execute(string passwordId, UpdatePasswordRequest request)
    {
        var existing = await User.Profiles.Get(passwordId);
        var modifiedDate = DateTime.UtcNow;
        var profile = new PasswordProfile()
        {
            id = passwordId,
            created = existing.Data.created,
            modified = $"{modifiedDate:O}",
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
            Id = passwordId,
            Version = existing.Version
        });
        return profile;
    }
}
