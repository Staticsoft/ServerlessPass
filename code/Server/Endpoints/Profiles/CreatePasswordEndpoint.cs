using Staticsoft.PartitionedStorage.Abstractions;
using Staticsoft.SharpPass.Authentication;

namespace Staticsoft.SharpPass.Server;

public class CreatePasswordEndpoint : HttpEndpoint<CreatePasswordRequest, PasswordProfile>
{
    readonly Documents Storage;
    readonly Identity Identity;
    readonly PasswordProfileIdGenerator Id;

    public CreatePasswordEndpoint(Documents storage, Identity identity, PasswordProfileIdGenerator id)
        => (Storage, Identity, Id)
        = (storage, identity, id);

    public async Task<PasswordProfile> Execute(CreatePasswordRequest request)
    {
        var createdDate = DateTime.Now;
        var passwordId = Id.Generate(createdDate);
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
}

public class PasswordProfileIdGenerator
{
    public string Generate(DateTime date)
        => Generate(DateTime.MaxValue.Ticks - date.Ticks);

    static string Generate(long ticksTillMaxTime)
        => $"{ToGuid(ticksTillMaxTime)}";

    static Guid ToGuid(long ticks)
        => ToGuid($"{ticks:X16}");

    static Guid ToGuid(string ticks)
        => new($"00000000-0000-0000-{ticks[0..4]}-{ticks[4..16]}");
}
