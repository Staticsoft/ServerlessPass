namespace Staticsoft.SharpPass.Tests;

public class PasswordsTests : PasswordScenarioBase
{
    [Fact]
    public async Task ReturnsEmptyListOfProfilesIfThereAreNoProfiles()
    {
        var passwords = await API.ListPasswords.Execute(new());
        Assert.Empty(passwords.Results);
        Assert.Equal(0, passwords.Count);
    }
}
