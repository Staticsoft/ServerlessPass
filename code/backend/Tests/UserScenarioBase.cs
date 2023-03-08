namespace Staticsoft.SharpPass.Tests;

public class UserScenarioBase : ScenarioBase, IAsyncLifetime
{
    public virtual Task InitializeAsync()
        => API.Auth.SignUp.Execute(new()
        {
            email = User.Email,
            password = User.Password
        });

    public virtual Task DisposeAsync()
        => Task.CompletedTask;
}
