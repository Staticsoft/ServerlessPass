using System.Threading.Tasks;

namespace Staticsoft.SharpPass.Server;

public class UpdatePasswordEndpoint : ParametrizedHttpEndpoint<UpdatePasswordRequest, Password>
{
    public Task<Password> Execute(string passwordId, UpdatePasswordRequest request)
    {
        throw new System.NotImplementedException();
    }
}
