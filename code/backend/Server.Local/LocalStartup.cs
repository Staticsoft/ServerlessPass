using Staticsoft.PartitionedStorage.Abstractions;
using Staticsoft.PartitionedStorage.Memory;
using Staticsoft.ServerlessPass.Authentication;
using Staticsoft.ServerlessPass.Authentication.Fakes;
using Staticsoft.ServerlessPass.Users;
using Staticsoft.ServerlessPass.Users.Fakes;

namespace Staticsoft.ServerlessPass.Server.Local;

public class LocalStartup : Startup
{
    protected override IServiceCollection RegisterServices(IServiceCollection services) => base.RegisterServices(services)
        .AddSingleton<Partitions, MemoryPartitions>()
        .AddSingleton<User, MemoryUser>()
        .AddSingleton<MemoryUsers>()
        .AddScoped<Identity, AuthorizationHeaderIdentity>();
}