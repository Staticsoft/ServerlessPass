using Staticsoft.PartitionedStorage.Abstractions;
using Staticsoft.SharpPass.Authentication;

namespace Staticsoft.SharpPass.Server;

public class CreatePasswordEndpoint : HttpEndpoint<CreatePasswordRequest, PasswordProfile>
{
    readonly Documents Storage;
    readonly Identity Identity;

    public CreatePasswordEndpoint(Documents storage, Identity identity)
        => (Storage, Identity)
        = (storage, identity);

    public async Task<PasswordProfile> Execute(CreatePasswordRequest request)
    {
        var createdDate = DateTime.Now;
        var ticksTillMaxTime = DateTime.MaxValue.Ticks - createdDate.Ticks;
        var passwordId = $"{ToGuid(ticksTillMaxTime)}";
        var profile = new PasswordProfile()
        {
            Id = passwordId,
            Created = $"{createdDate}",
            Modified = $"{createdDate}",
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
            Id = passwordId
        });
        return profile;
    }

    static Guid ToGuid(long ticks)
        => new(0, 0, 0, BitConverter.GetBytes(ticks));
}
