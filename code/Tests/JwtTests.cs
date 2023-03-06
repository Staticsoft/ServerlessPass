using Staticsoft.SharpPass.Users;

namespace Staticsoft.SharpPass.Tests;

public class JwtTests : UserScenarioBase
{
    [Fact]
    public async Task CannotAuthenticateUsingInvalidEmail()
    {
        await Assert.ThrowsAsync<LogInException>(() => API.Auth.Jwt.Create.Execute(new()
        {
            email = "invalid@email.com",
            password = User.Password
        }));
    }

    [Fact]
    public async Task CannotAuthenticateUsingInvalidPassword()
    {
        await Assert.ThrowsAsync<LogInException>(() => API.Auth.Jwt.Create.Execute(new()
        {
            email = User.Email,
            password = "InvalidPassword"
        }));
    }

    [Fact]
    public async Task CanAuthenticateUsingValidEmailAndPassword()
    {
        var response = await API.Auth.Jwt.Create.Execute(new()
        {
            email = User.Email,
            password = User.Password
        });
        Assert.NotEmpty(response.access);
        Assert.NotEmpty(response.refresh);
    }

    [Fact]
    public async Task CannotAuthenticateUsingInvalidRefreshToken()
    {
        await Assert.ThrowsAsync<LogInException>(() => API.Auth.Jwt.Refresh.Execute(new()
        {
            refresh = "InvalidToken"
        }));
    }

    [Fact]
    public async Task CanAuthenticateUsingValidRefreshToken()
    {
        var initialResponse = await API.Auth.Jwt.Create.Execute(new()
        {
            email = User.Email,
            password = User.Password
        });
        var secondaryResponse = await API.Auth.Jwt.Refresh.Execute(new()
        {
            refresh = initialResponse.refresh
        });
        Assert.NotEmpty(secondaryResponse.access);
        Assert.NotEmpty(secondaryResponse.refresh);
    }
}
