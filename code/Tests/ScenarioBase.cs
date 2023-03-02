using Microsoft.Extensions.DependencyInjection;
using Staticsoft.SharpPass.Users;
using Staticsoft.SharpPass.Users.Fakes;

namespace Staticsoft.SharpPass.Tests;

public class ScenarioBase : TestBase, IAsyncLifetime
{
    protected override IServiceCollection Services => base.Services
        .AddSingleton<TestUser>();

    MemoryUser Users
        => (MemoryUser)Get<User>();

    protected TestUser User
        => Service<TestUser>();

    public virtual Task InitializeAsync()
        => Users.Create(User.Email, User.Password);

    public virtual Task DisposeAsync()
        => Task.CompletedTask;
}
