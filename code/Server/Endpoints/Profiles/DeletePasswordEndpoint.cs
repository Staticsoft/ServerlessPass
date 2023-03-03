namespace Staticsoft.SharpPass.Server;

public class DeletePasswordEndpoint : ParametrizedHttpEndpoint<EmptyRequest, DeletePasswordResponse>
{
    readonly UserDocuments User;

    public DeletePasswordEndpoint(UserDocuments user)
        => User = user;

    public async Task<DeletePasswordResponse> Execute(string passwordId, EmptyRequest request)
    {
        await User.Profiles.Remove(passwordId);
        return new();
    }
}
