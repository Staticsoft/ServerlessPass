using System.Threading.Tasks;

namespace Staticsoft.SharpPass.Server;

public class CreatePasswordEndpoint : HttpEndpoint<CreatePasswordRequest, Password>
{
    public Task<Password> Execute(CreatePasswordRequest request)
    {
        throw new System.NotImplementedException();
    }
}
