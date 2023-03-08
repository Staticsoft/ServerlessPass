using Amazon.CognitoIdentityProvider;
using Amazon.DynamoDBv2;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Staticsoft.PartitionedStorage.Abstractions;
using Staticsoft.PartitionedStorage.AWS;
using Staticsoft.SharpPass.Authentication;
using Staticsoft.SharpPass.Authentication.ASP;
using Staticsoft.SharpPass.Services;
using Staticsoft.SharpPass.Users;
using Staticsoft.SharpPass.Users.Cognito;
using System;

namespace Staticsoft.SharpPass.Server.AWS;

public class AWSStartup : Startup
{
    protected override IApplicationBuilder ConfigureApp(IApplicationBuilder app, IWebHostEnvironment env)
        => base.ConfigureApp(app, env);

    protected override IServiceCollection RegisterServices(IServiceCollection services) => base.RegisterServices(services)
        .AddCors()
        .AddSingleton<Partitions, DynamoDBPartitions>()
        .AddSingleton(DynamoDbOptions())
        .AddSingleton<AmazonDynamoDBClient>()
        .AddScoped<CognitoUser>()
        .AddScoped<User>(provider => provider.GetRequiredService<CognitoUser>())
        .AddScoped<ServiceStatus>(provider => provider.GetRequiredService<CognitoUser>())
        .AddSingleton(CognitoOptions())
        .AddSingleton<AmazonCognitoIdentityProviderClient>()
        .AddScoped<Identity, ClaimIdentity>();

    static DynamoDBPartitionedStorageOptions DynamoDbOptions()
        => new() { TableNamePrefix = Configuration("DynamoDbTableNamePrefix") };

    static CognitoOptions CognitoOptions()
        => new(
            userPoolId: Configuration("CognitoUserPoolId"),
            clientId: Configuration("CognitoClientAppId")
        );

    static string Configuration(string name)
        => Environment.GetEnvironmentVariable(name) ?? throw new NullReferenceException($"Environment varialbe {name} is not set");
}
