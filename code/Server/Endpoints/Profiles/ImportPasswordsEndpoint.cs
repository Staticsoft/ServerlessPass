using Staticsoft.PartitionedStorage.Abstractions;
using System.Globalization;

namespace Staticsoft.SharpPass.Server;

public class ImportPasswordsEndpoint : HttpEndpoint<PasswordProfiles, PasswordProfiles>
{
    readonly UserDocuments User;
    readonly PasswordProfileIdGenerator Id;

    static readonly ParallelOptions Parallelism = new() { MaxDegreeOfParallelism = 10 };

    public ImportPasswordsEndpoint(UserDocuments user, PasswordProfileIdGenerator id)
        => (User, Id)
        = (user, id);

    public async Task<PasswordProfiles> Execute(PasswordProfiles request)
    {
        var profiles = await User.Profiles.Scan();
        await Parallel.ForEachAsync(profiles, Parallelism, DeleteProfile);

        var imported = request.Results.Select(ToImportedProfile).ToArray();
        await Parallel.ForEachAsync(imported, Parallelism, ImportProfile);

        return new PasswordProfiles() { Results = imported };
    }

    async ValueTask DeleteProfile(Item<PasswordProfile> profile, CancellationToken _)
        => await User.Profiles.Remove(profile.Id);

    PasswordProfile ToImportedProfile(PasswordProfile profile)
        => ToImportedProfile(profile, DateTime.ParseExact(profile.Created, "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind));

    PasswordProfile ToImportedProfile(PasswordProfile profile, DateTime createdDate)
        => profile with { Id = Id.Generate(createdDate) };

    async ValueTask ImportProfile(PasswordProfile profile, CancellationToken _)
        => await User.Profiles.Save(new Item<PasswordProfile>
        {
            Data = profile,
            Id = profile.Id
        });
}
