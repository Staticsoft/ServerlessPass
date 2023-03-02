using Microsoft.Extensions.DependencyInjection;
using Staticsoft.HttpCommunication.Abstractions;

namespace Staticsoft.SharpPass.Tests;

public class PasswordScenarioBase : ScenarioBase, IAsyncLifetime
{
    protected override IServiceCollection Services => base.Services
        .DecorateSingleton<HttpRequestExecutor, AuthenticatedHttpRequestExecutor>();

    AuthenticatedHttpRequestExecutor Executor
        => (AuthenticatedHttpRequestExecutor)Service<HttpRequestExecutor>();

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        var response = await API.Auth.Jwt.Create.Execute(new()
        {
            Email = User.Email,
            Password = User.Password
        });
        Executor.Token = response.Access;
    }
}
