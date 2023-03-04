namespace Staticsoft.SharpPass.Tests;

public class UserScenarioBase : ScenarioBase, IAsyncLifetime
{
    public virtual Task InitializeAsync()
        => API.Auth.SignUp.Execute(new()
        {
            Email = User.Email,
            Password = User.Password
        });

    public virtual Task DisposeAsync()
        => Task.CompletedTask;
}
