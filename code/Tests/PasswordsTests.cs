using Staticsoft.Contracts.Abstractions;

namespace Staticsoft.SharpPass.Tests;

public class PasswordsTests : PasswordScenarioBase
{
    [Fact]
    public async Task ReturnsEmptyListOfProfilesIfThereAreNoProfiles()
    {
        var profiles = await API.Passwords.List.Execute();
        Assert.Empty(profiles.results);
        Assert.Equal(0, profiles.count);
    }

    [Fact]
    public async Task ReturnsSavedProfileAfterProfileIsCreated()
    {
        var profile = await API.Passwords.Create.Execute(new()
        {
            site = "domain.com",
            login = "test@mail.com",
            uppercase = true,
            lowercase = true,
            numbers = true,
            symbols = true,
            length = 16,
            counter = 1,
            version = 2
        });
        Assert.NotEmpty(profile.id);
        Assert.NotEmpty(profile.created);
        Assert.NotEmpty(profile.modified);
        Assert.Equal("domain.com", profile.site);
        Assert.Equal("test@mail.com", profile.login);
        Assert.True(profile.uppercase);
        Assert.True(profile.lowercase);
        Assert.True(profile.numbers);
        Assert.True(profile.symbols);
        Assert.Equal(16, profile.length);
        Assert.Equal(1, profile.counter);
        Assert.Equal(2, profile.version);
    }

    [Fact]
    public async Task ReturnsSingleProfileAfterProfileIsCreated()
    {
        var profile = await API.Passwords.Create.Execute(new()
        {
            site = "domain.com",
            login = "test@mail.com",
            uppercase = true,
            lowercase = true,
            numbers = true,
            symbols = true,
            length = 16,
            counter = 1,
            version = 2
        });
        var profiles = await API.Passwords.List.Execute();
        Assert.Equal(1, profiles.count);
        Assert.Equal(profile, profiles.results.Single());
    }

    [Fact]
    public async Task ReturnsModifiedProfileAfterProfileIsModified()
    {
        var created = await API.Passwords.Create.Execute(new());
        var profile = await API.Passwords.Update.Execute(created.id, new()
        {
            site = "domain.com",
            login = "test@mail.com",
            uppercase = true,
            lowercase = true,
            numbers = true,
            symbols = true,
            length = 16,
            counter = 1,
            version = 2
        });
        Assert.NotEmpty(profile.id);
        Assert.NotEmpty(profile.created);
        Assert.NotEmpty(profile.modified);
        Assert.Equal("domain.com", profile.site);
        Assert.Equal("test@mail.com", profile.login);
        Assert.True(profile.uppercase);
        Assert.True(profile.lowercase);
        Assert.True(profile.numbers);
        Assert.True(profile.symbols);
        Assert.Equal(16, profile.length);
        Assert.Equal(1, profile.counter);
        Assert.Equal(2, profile.version);
    }

    [Fact]
    public async Task ReturnsSingleProfileAfterProfileIsCreatedAndModified()
    {
        var created = await API.Passwords.Create.Execute(new());
        var profile = await API.Passwords.Update.Execute(created.id, new()
        {
            site = "domain.com",
            login = "test@mail.com",
            uppercase = true,
            lowercase = true,
            numbers = true,
            symbols = true,
            length = 16,
            counter = 1,
            version = 2
        });
        var profiles = await API.Passwords.List.Execute();
        Assert.Equal(1, profiles.count);
        Assert.Equal(profile, profiles.results.Single());
    }

    [Fact]
    public async Task ReturnsEmptyListOfProfilesAfterProfileIsCreatedAndDeleted()
    {
        var profile = await API.Passwords.Create.Execute(new());
        await API.Passwords.Delete.Execute(profile.id);
        var profiles = await API.Passwords.List.Execute();
        Assert.Equal(0, profiles.count);
        Assert.Empty(profiles.results);
    }

    [Fact]
    public async Task ImportOfExistingProfilesHasNoEffect()
    {
        await API.Passwords.Create.Execute(new()
        {
            site = "domain.com",
            login = "test@mail.com",
            uppercase = true,
            lowercase = true,
            numbers = true,
            symbols = true,
            length = 16,
            counter = 1,
            version = 2
        });
        var profiles = await API.Passwords.List.Execute();
        await API.Passwords.Import.Execute(profiles);
        var imported = await API.Passwords.List.Execute();
        Assert.Equal(profiles.results, imported.results);
    }

    [Fact]
    public async Task ImportOfExistingProfileWithDifferentCreatedDateUpdatesProfileId()
    {
        var profile = await API.Passwords.Create.Execute(new()
        {
            site = "domain.com",
            login = "test@mail.com",
            uppercase = true,
            lowercase = true,
            numbers = true,
            symbols = true,
            length = 16,
            counter = 1,
            version = 2
        });
        var updated = profile with { created = $"{DateTime.UtcNow:O}" };
        await API.Passwords.Import.Execute(new() { results = new[] { updated } });
        var imported = await API.Passwords.List.Execute();
        Assert.NotEqual(profile.id, imported.results.Single().id);
    }
}
