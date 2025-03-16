namespace Staticsoft.ServerlessPass.Server;

public class ListPasswordsEndpoint : HttpEndpoint<EmptyRequest, PasswordProfiles>
{
    readonly PasswordProfileRepository Profiles;
    readonly IHttpContextAccessor Accessor;

    public ListPasswordsEndpoint(PasswordProfileRepository profiles, IHttpContextAccessor accessor)
        => (Profiles, Accessor) = (profiles, accessor);

    HttpContext Context
        => Accessor.HttpContext ?? throw new InvalidOperationException($"{nameof(Accessor.HttpContext)} is null");

    public async Task<PasswordProfiles> Execute(EmptyRequest request)
    {
        var profiles = await Profiles.Scan();
        if (!Context.Request.Query.TryGetValue("search", out var values))
        {
            return new() { results = profiles };
        }

        var site = values.Single()!;
        var matchingProfiles = profiles
            .Where(profile => IsSameSite(profile, site))
            .OrderBy(profile => LengthDifference(profile, site))
            .ToArray();
        return new() { results = matchingProfiles };
    }

    static bool IsSameSite(PasswordProfile profile, string site)
        => site.EndsWith(profile.site);

    static int LengthDifference(PasswordProfile profile, string site)
        => Math.Abs(profile.site.Length - site.Length);
}
