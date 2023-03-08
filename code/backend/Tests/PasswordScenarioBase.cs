using Microsoft.Extensions.DependencyInjection;
using Staticsoft.HttpCommunication.Abstractions;

namespace Staticsoft.SharpPass.Tests;

public class PasswordScenarioBase : UserScenarioBase, IAsyncLifetime
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
            email = User.Email,
            password = User.Password
        });
        Executor.Token = response.access;
    }
}
