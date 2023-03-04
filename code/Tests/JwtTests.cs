using Staticsoft.SharpPass.Users;

namespace Staticsoft.SharpPass.Tests;

public class JwtTests : UserScenarioBase
{
    [Fact]
    public async Task CannotAuthenticateUsingInvalidEmail()
    {
        await Assert.ThrowsAsync<LogInException>(() => API.Auth.Jwt.Create.Execute(new()
        {
            Email = "invalid@email.com",
            Password = User.Password
        }));
    }

    [Fact]
    public async Task CannotAuthenticateUsingInvalidPassword()
    {
        await Assert.ThrowsAsync<LogInException>(() => API.Auth.Jwt.Create.Execute(new()
        {
            Email = User.Email,
            Password = "InvalidPassword"
        }));
    }

    [Fact]
    public async Task CanAuthenticateUsingValidEmailAndPassword()
    {
        var response = await API.Auth.Jwt.Create.Execute(new()
        {
            Email = User.Email,
            Password = User.Password
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
            Email = User.Email,
            Password = User.Password
        });
        var secondaryResponse = await API.Auth.Jwt.Refresh.Execute(new()
        {
            Refresh = initialResponse.Refresh
        });
        Assert.NotEmpty(secondaryResponse.Access);
        Assert.NotEmpty(secondaryResponse.Refresh);
    }
}
