using Microsoft.Extensions.DependencyInjection;
using Staticsoft.HttpCommunication.Abstractions;

namespace Staticsoft.ServerlessPass.Tests;

public class PasswordScenarioBase : UserScenarioBase, IAsyncLifetime
{
    protected override IServiceCollection ClientServices(IServiceCollection services) => base.ClientServices(services)
        .Decorate<HttpRequestExecutor, AuthenticatedHttpRequestExecutor>();

    AuthenticatedHttpRequestExecutor Executor
        => (AuthenticatedHttpRequestExecutor)Client<HttpRequestExecutor>();

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
