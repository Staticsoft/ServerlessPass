using Staticsoft.Contracts.Abstractions;

namespace Staticsoft.SharpPass.Tests;

public class AdministrativeTests : TestBase
{
    [Fact]
    public async Task StatusCompletesWithoutErrors()
    {
        await API.Administrative.Status.Execute();
    }
}
