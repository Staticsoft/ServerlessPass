using Staticsoft.Contracts.Abstractions;
using Staticsoft.ServerlessPass.Contracts;

namespace Staticsoft.ServerlessPass.Tests;

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
            digits = true,
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
        Assert.True(profile.digits);
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
            digits = true,
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
            digits = true,
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
        Assert.True(profile.digits);
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
            digits = true,
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
    public async Task ImportOfEmptyListOfProfilesRemovesAllProfiles()
    {
        await API.Passwords.Create.Execute(new()
        {
            site = "domain.com",
            login = "test@mail.com",
            uppercase = true,
            lowercase = true,
            digits = true,
            symbols = true,
            length = 16,
            counter = 1,
            version = 2
        });
        await API.Passwords.Import.Execute(new() { results = Array.Empty<PasswordProfile>() });
        var imported = await API.Passwords.List.Execute();
        Assert.Empty(imported.results);
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
            digits = true,
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
    public async Task ReturnsProfilesInReverseChronologicalOrder()
    {
        await API.Passwords.Create.Execute(new()
        {
            site = "earlier.domain.com"
        });
        await API.Passwords.Create.Execute(new()
        {
            site = "later.domain.com"
        });
        var profiles = await API.Passwords.List.Execute();
        Assert.Equal(
            new[] { "later.domain.com", "earlier.domain.com" },
            profiles.results.Select(profile => profile.site)
        );
    }

    [Fact]
    public async Task ImportsProfilesInReverseChronologicalOrder()
    {
        await API.Passwords.Create.Execute(new()
        {
            site = "earlier.domain.com"
        });
        await API.Passwords.Create.Execute(new()
        {
            site = "later.domain.com"
        });
        var profiles = await API.Passwords.List.Execute();
        await API.Passwords.Import.Execute(profiles);
        var imported = await API.Passwords.List.Execute();
        Assert.Equal(profiles.results, imported.results);
    }

    [Fact]
    public async Task CanCreate1001Profile()
    {
        await CreateProfiles(1001);
        var profiles = await API.Passwords.List.Execute();
        Assert.Equal(1001, profiles.count);
    }

    [Fact]
    public async Task CanImport1001Profile()
    {
        await CreateProfiles(1001);
        var profiles = await API.Passwords.List.Execute();
        await API.Passwords.Import.Execute(profiles);
        var imported = await API.Passwords.List.Execute();
        Assert.Equal(profiles.results, imported.results);
    }

    async Task CreateProfiles(int profilesCount)
    {
        foreach (var loginId in Enumerable.Range(0, profilesCount))
        {
            await API.Passwords.Create.Execute(new()
            {
                site = "domain.com",
                login = $"test{loginId}@mail.com"
            });
        }
    }
}
