using Microsoft.Extensions.DependencyInjection;
using Staticsoft.ServerlessPass.Server;

namespace Staticsoft.ServerlessPass.Tests;

public class PasswordProfilesIdGeneratorTests : TestBase<PasswordProfilesIdGenerator>
{
    protected override IServiceCollection Services => base.Services
        .AddSingleton<PasswordProfilesIdGenerator>();

    [Fact]
    public void GeneratesIdsInReverseOrder()
    {
        var passwordTime = DateTime.Now;
        var primeTime = 37870000033;
        var passwordsIdsToGenerate = 100000;

        var dates = new List<DateTime>(passwordsIdsToGenerate);
        for (var i = 0; i < passwordsIdsToGenerate; i++)
        {
            passwordTime = passwordTime.AddTicks(primeTime);
            dates.Add(passwordTime);
        }

        var passwordIds = dates.Select(SUT.Generate).ToArray();
        var reversedOrderPasswordIds = passwordIds.Sort().Reverse().ToArray();

        Assert.Equal(passwordIds, reversedOrderPasswordIds);
    }
}

public static class LinqExtensions
{
    public static IEnumerable<T> Sort<T>(this IEnumerable<T> collection)
        => collection.OrderBy(item => item);
}
