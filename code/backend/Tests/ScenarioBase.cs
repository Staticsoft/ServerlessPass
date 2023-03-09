using Microsoft.Extensions.DependencyInjection;

namespace Staticsoft.ServerlessPass.Tests;

public class ScenarioBase : TestBase
{
    protected override IServiceCollection Services => base.Services
        .AddSingleton<TestUser>();

    protected TestUser User
        => Service<TestUser>();
}