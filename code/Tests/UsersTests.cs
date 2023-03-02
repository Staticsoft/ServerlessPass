using Staticsoft.SharpPass.Users;
using Staticsoft.SharpPass.Users.Fakes;

namespace Staticsoft.SharpPass.Tests;

public class UsersTests : TestBase, IAsyncLifetime
{
    readonly string UserEmail;
    const string UserPassword = "TestP@ssword!123";

    MemoryUser Users
        => (MemoryUser)Get<User>();

    public UsersTests()
        => UserEmail = $"test-{Guid.NewGuid()}@email.com";

    [Fact]
    public async Task CannotAuthenticateUsingInvalidEmail()
    {
        await Assert.ThrowsAsync<LogInException>(() => API.Auth.Jwt.Create.Execute(new()
        {
            Email = "invalid@email.com",
            Password = UserPassword
        }));
    }

    [Fact]
    public async Task CannotAuthenticateUsingInvalidPassword()
    {
        await Assert.ThrowsAsync<LogInException>(() => API.Auth.Jwt.Create.Execute(new()
        {
            Email = UserEmail,
            Password = "InvalidPassword"
        }));
    }

    [Fact]
    public async Task CanAuthenticateUsingValidEmailAndPassword()
    {
        var response = await API.Auth.Jwt.Create.Execute(new()
        {
            Email = UserEmail,
            Password = UserPassword
        });
        Assert.NotEmpty(response.Access);
        Assert.NotEmpty(response.Refresh);
    }

    [Fact]
    public async Task CannotAuthenticateUsingInvalidRefreshToken()
    {
        await Assert.ThrowsAsync<LogInException>(() => API.Auth.Jwt.Refresh.Execute(new()
        {
            Refresh = "InvalidToken"
        }));
    }

    [Fact]
    public async Task CanAuthenticateUsingValidRefreshToken()
    {
        var initialResponse = await API.Auth.Jwt.Create.Execute(new()
        {
            Email = UserEmail,
            Password = UserPassword
        });
        var secondaryResponse = await API.Auth.Jwt.Refresh.Execute(new()
        {
            Refresh = initialResponse.Refresh
        });
        Assert.NotEmpty(secondaryResponse.Access);
        Assert.NotEmpty(secondaryResponse.Refresh);
    }

    public Task InitializeAsync()
        => Users.Create(UserEmail, UserPassword);

    public Task DisposeAsync()
        => Task.CompletedTask;
}
