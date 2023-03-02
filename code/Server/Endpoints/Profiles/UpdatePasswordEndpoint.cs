using System.Threading.Tasks;

namespace Staticsoft.SharpPass.Server;

public class UpdatePasswordEndpoint : ParametrizedHttpEndpoint<UpdatePasswordRequest, PasswordProfile>
{
    public Task<PasswordProfile> Execute(string passwordId, UpdatePasswordRequest request)
    {
        throw new System.NotImplementedException();
    }
}
