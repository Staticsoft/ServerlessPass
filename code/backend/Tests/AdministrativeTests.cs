using Staticsoft.Contracts.Abstractions;

namespace Staticsoft.ServerlessPass.Tests;

public class AdministrativeTests : TestBase
{
    [Fact]
    public async Task StatusCompletesWithoutErrors()
    {
        await API.Administrative.Status.Execute();
    }
}
