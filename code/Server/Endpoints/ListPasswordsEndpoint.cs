using System.Threading.Tasks;

namespace Staticsoft.SharpPass.Server;

public class ListPasswordsEndpoint : HttpEndpoint<EmptyRequest, Password[]>
{
    public Task<Password[]> Execute(EmptyRequest request)
    {
        throw new System.NotImplementedException();
    }
}
