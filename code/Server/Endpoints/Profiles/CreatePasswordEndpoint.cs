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
            Id = passwordId,
            Created = $"{createdDate:O}",
            Modified = $"{createdDate:O}",
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
            Id = passwordId
        });
        return profile;
    }
}
