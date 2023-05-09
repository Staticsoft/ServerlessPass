using Microsoft.Extensions.DependencyInjection;
using Staticsoft.Contracts.ASP.Client;
using Staticsoft.HttpCommunication.Json;
using Staticsoft.Serialization.Net;
using Staticsoft.ServerlessPass.Contracts;
using Staticsoft.ServerlessPass.Server.Local;
using Staticsoft.Testing.Integration;

namespace Staticsoft.ServerlessPass.Tests;

public class TestBase : IntegrationTestBase<LocalStartup>
{
    protected override IServiceCollection ClientServices(IServiceCollection services) => base.ClientServices(services)
        .UseClientAPI<Schema>()
        .UseSystemJsonSerializer()
        .UseJsonHttpCommunication();

    protected Schema API
        => Client<Schema>();
}
