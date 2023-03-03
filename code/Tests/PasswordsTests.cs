using Staticsoft.Contracts.Abstractions;

namespace Staticsoft.SharpPass.Tests;

public class PasswordsTests : PasswordScenarioBase
{
    [Fact]
    public async Task ReturnsEmptyListOfProfilesIfThereAreNoProfiles()
    {
        var profiles = await API.ListPasswords.Execute();
        Assert.Empty(profiles.Results);
        Assert.Equal(0, profiles.Count);
    }

    [Fact]
    public async Task ReturnsSavedProfileAfterProfileIsCreated()
    {
        var profile = await API.CreatePassword.Execute(new()
        {
            Site = "domain.com",
            Login = "test@mail.com",
            Uppercase = true,
            Lowercase = true,
            Numbers = true,
            Symbols = true,
            Length = 16,
            Counter = 1,
            Version = 2
        });
        Assert.NotEmpty(profile.Id);
        Assert.NotEmpty(profile.Created);
        Assert.NotEmpty(profile.Modified);
        Assert.Equal("domain.com", profile.Site);
        Assert.Equal("test@mail.com", profile.Login);
        Assert.True(profile.Uppercase);
        Assert.True(profile.Lowercase);
        Assert.True(profile.Numbers);
        Assert.True(profile.Symbols);
        Assert.Equal(16, profile.Length);
        Assert.Equal(1, profile.Counter);
        Assert.Equal(2, profile.Version);
    }

    [Fact]
    public async Task ReturnsSingleProfileAfterProfileIsCreated()
    {
        var profile = await API.CreatePassword.Execute(new()
        {
            Site = "domain.com",
            Login = "test@mail.com",
            Uppercase = true,
            Lowercase = true,
            Numbers = true,
            Symbols = true,
            Length = 16,
            Counter = 1,
            Version = 2
        });
        var profiles = await API.ListPasswords.Execute();
        Assert.Equal(1, profiles.Count);
        Assert.Equal(profile, profiles.Results.Single());
    }

    [Fact]
    public async Task ReturnsModifiedProfileAfterProfileIsModified()
    {
        var created = await API.CreatePassword.Execute(new());
        var profile = await API.Passwords.Update.Execute(created.Id, new()
        {
            Site = "domain.com",
            Login = "test@mail.com",
            Uppercase = true,
            Lowercase = true,
            Numbers = true,
            Symbols = true,
            Length = 16,
            Counter = 1,
            Version = 2
        });
        Assert.NotEmpty(profile.Id);
        Assert.NotEmpty(profile.Created);
        Assert.NotEmpty(profile.Modified);
        Assert.Equal("domain.com", profile.Site);
        Assert.Equal("test@mail.com", profile.Login);
        Assert.True(profile.Uppercase);
        Assert.True(profile.Lowercase);
        Assert.True(profile.Numbers);
        Assert.True(profile.Symbols);
        Assert.Equal(16, profile.Length);
        Assert.Equal(1, profile.Counter);
        Assert.Equal(2, profile.Version);
    }

    [Fact]
    public async Task ReturnsSingleProfileAfterProfileIsCreatedAndModified()
    {
        var created = await API.CreatePassword.Execute(new());
        var profile = await API.Passwords.Update.Execute(created.Id, new()
        {
            Site = "domain.com",
            Login = "test@mail.com",
            Uppercase = true,
            Lowercase = true,
            Numbers = true,
            Symbols = true,
            Length = 16,
            Counter = 1,
            Version = 2
        });
        var profiles = await API.ListPasswords.Execute();
        Assert.Equal(1, profiles.Count);
        Assert.Equal(profile, profiles.Results.Single());
    }

    [Fact]
    public async Task ReturnsEmptyListOfProfilesAfterProfileIsCreatedAndDeleted()
    {
        var profile = await API.CreatePassword.Execute(new());
        await API.Passwords.Delete.Execute(profile.Id);
        var profiles = await API.ListPasswords.Execute();
        Assert.Equal(0, profiles.Count);
        Assert.Empty(profiles.Results);
    }

    [Fact]
    public async Task ImportOfExistingProfilesHasNoEffect()
    {
        await API.CreatePassword.Execute(new()
        {
            Site = "domain.com",
            Login = "test@mail.com",
            Uppercase = true,
            Lowercase = true,
            Numbers = true,
            Symbols = true,
            Length = 16,
            Counter = 1,
            Version = 2
        });
        var profiles = await API.ListPasswords.Execute();
        await API.ImportPasswords.Execute(profiles);
        var imported = await API.ListPasswords.Execute();
        Assert.Equal(profiles.Results, imported.Results);
    }

    [Fact]
    public async Task ImportOfExistingProfileWithDifferentCreatedDateUpdatesProfileId()
    {
        var profile = await API.CreatePassword.Execute(new()
        {
            Site = "domain.com",
            Login = "test@mail.com",
            Uppercase = true,
            Lowercase = true,
            Numbers = true,
            Symbols = true,
            Length = 16,
            Counter = 1,
            Version = 2
        });
        var updated = profile with { Created = $"{DateTime.UtcNow:O}" };
        await API.ImportPasswords.Execute(new() { Results = new[] { updated } });
        var imported = await API.ListPasswords.Execute();
        Assert.NotEqual(profile.Id, imported.Results.Single().Id);
    }
}
