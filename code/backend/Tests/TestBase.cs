using Microsoft.Extensions.DependencyInjection;
using Staticsoft.Contracts.ASP.Client;
using Staticsoft.HttpCommunication.Json;
using Staticsoft.Serialization.Net;
using Staticsoft.SharpPass.Contracts;
using Staticsoft.SharpPass.Server.Local;

namespace Staticsoft.SharpPass.Tests;

public class TestBase : TestBase<IntegrationServicesBase<LocalStartup>>
{
    readonly IServiceProvider Provider;

    public TestBase()
        => Provider = Services.BuildServiceProvider();

    protected virtual IServiceCollection Services
        => new ServiceCollection()
            .UseClientAPI<Schema>()
            .UseSystemJsonSerializer()
            .UseJsonHttpCommunication()
            .AddSingleton(Get<HttpClient>());

    protected Schema API
            => Service<Schema>();

    protected T Service<T>()
        where T : notnull
        => Provider.GetRequiredService<T>();
}
