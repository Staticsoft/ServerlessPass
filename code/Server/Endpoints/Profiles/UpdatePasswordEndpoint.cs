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
            Id = passwordId,
            Created = existing.Data.Created,
            Modified = $"{modifiedDate:O}",
            Site = request.Site,
            Login = request.Login,
            Uppercase = request.Uppercase,
            Lowercase = request.Lowercase,
            Numbers = request.Numbers,
            Symbols = request.Symbols,
            Length = request.Length,
            Counter = request.Counter,
            Version = request.Version
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
