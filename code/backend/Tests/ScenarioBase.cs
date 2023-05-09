using Microsoft.Extensions.DependencyInjection;

namespace Staticsoft.ServerlessPass.Tests;

public class ScenarioBase : TestBase
{
    protected override IServiceCollection ClientServices(IServiceCollection services) => base.ClientServices(services)
        .AddSingleton<TestUser>();

    protected TestUser User
        => Client<TestUser>();
}