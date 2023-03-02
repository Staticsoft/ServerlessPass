using Staticsoft.PartitionedStorage.Abstractions;
using Staticsoft.SharpPass.Authentication;

namespace Staticsoft.SharpPass.Server;

public class UpdatePasswordEndpoint : ParametrizedHttpEndpoint<UpdatePasswordRequest, PasswordProfile>
{
    readonly Documents Storage;
    readonly Identity Identity;

    public UpdatePasswordEndpoint(Documents storage, Identity identity)
        => (Storage, Identity)
        = (storage, identity);

    public async Task<PasswordProfile> Execute(string passwordId, UpdatePasswordRequest request)
    {
        var existing = await Storage.Profiles.Get(Identity.UserId).Get(passwordId);
        var modifiedDate = DateTime.Now;
        var profile = new PasswordProfile()
        {
            Id = passwordId,
            Created = existing.Data.Created,
            Modified = $"{modifiedDate}",
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
        await Storage.Profiles.Get(Identity.UserId).Save(new Item<PasswordProfile>()
        {
            Data = profile,
            Id = passwordId,
            Version = existing.Version
        });
        return profile;
    }
}
