namespace Staticsoft.ServerlessPass.Server;

public class GetPasswordEndpoint : ParametrizedHttpEndpoint<EmptyRequest, PasswordProfile>
{
    readonly PasswordProfileRepository Profiles;

    public GetPasswordEndpoint(PasswordProfileRepository profiles)
        => Profiles = profiles;

    public async Task<PasswordProfile> Execute(string parameter, EmptyRequest request)
    {
        var profiles = await Profiles.Scan();
        return profiles.Single(profile => profile.id == parameter);
    }
}