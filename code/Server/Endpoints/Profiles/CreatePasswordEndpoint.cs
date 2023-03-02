using System.Threading.Tasks;

namespace Staticsoft.SharpPass.Server;

public class CreatePasswordEndpoint : HttpEndpoint<CreatePasswordRequest, PasswordProfile>
{
    public Task<PasswordProfile> Execute(CreatePasswordRequest request)
    {
        throw new System.NotImplementedException();
    }
}
