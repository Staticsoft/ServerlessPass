using Microsoft.Extensions.DependencyInjection;
using Staticsoft.PartitionedStorage.Abstractions;
using Staticsoft.PartitionedStorage.Memory;
using Staticsoft.SharpPass.Authentication;
using Staticsoft.SharpPass.Authentication.Fakes;
using Staticsoft.SharpPass.Users;
using Staticsoft.SharpPass.Users.Fakes;

namespace Staticsoft.SharpPass.Server.Local;

public class LocalStartup : Startup
{
    protected override IServiceCollection RegisterServices(IServiceCollection services) => base.RegisterServices(services)
        .AddSingleton<Partitions, MemoryPartitions>()
        .AddSingleton<User, MemoryUser>()
        .AddSingleton<MemoryUsers>()
        .AddScoped<Identity, AuthorizationHeaderIdentity>();
}