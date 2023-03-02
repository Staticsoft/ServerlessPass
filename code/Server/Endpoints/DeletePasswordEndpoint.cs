using System.Threading.Tasks;

namespace Staticsoft.SharpPass.Server;

public class DeletePasswordEndpoint : ParametrizedHttpEndpoint<EmptyRequest, DeletePasswordResponse>
{
    public Task<DeletePasswordResponse> Execute(string passwordId, EmptyRequest request)
    {
        throw new System.NotImplementedException();
    }
}
