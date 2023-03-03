using Microsoft.Extensions.DependencyInjection;
using Staticsoft.SharpPass.Server;

namespace Staticsoft.SharpPass.Tests;

public class PasswordProfileIdGeneratorServices : UnitServicesBase
{
    protected override IServiceCollection Services => base.Services
        .AddSingleton<PasswordProfileIdGenerator>();
}


public class PasswordProfileIdGeneratorTests : TestBase<PasswordProfileIdGenerator, PasswordProfileIdGeneratorServices>
{
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
