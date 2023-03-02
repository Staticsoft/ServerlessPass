using Amazon.DynamoDBv2;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Staticsoft.PartitionedStorage.Abstractions;
using Staticsoft.PartitionedStorage.AWS;
using Staticsoft.SharpPass.Authentication;
using Staticsoft.SharpPass.Authentication.ASP;
using Staticsoft.SharpPass.Users;
using Staticsoft.SharpPass.Users.Cognito;
using System;

namespace Staticsoft.SharpPass.Server.AWS;

public class AWSStartup : Startup
{
    protected override IApplicationBuilder ConfigureApp(IApplicationBuilder app, IWebHostEnvironment env) => base.ConfigureApp(app, env)
        .UseAuthorization()
        .UseCors(policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
        );

    protected override IServiceCollection RegisterServices(IServiceCollection services) => base.RegisterServices(services)
        .AddSingleton<Partitions, DynamoDBPartitions>()
        .AddSingleton(new DynamoDBPartitionedStorageOptions() { TableNamePrefix = nameof(SharpPass) })
        .AddSingleton<AmazonDynamoDBClient>()
        .AddSingleton<User, CognitoUser>()
        .AddSingleton(CognitoOptions())
        .AddSingleton<Identity, ClaimIdentity>();

    static CognitoOptions CognitoOptions()
        => new(
            userPoolId: Environment.GetEnvironmentVariable("CognitoUserPoolId"),
            clientId: Environment.GetEnvironmentVariable("CognitoClientAppId")
        );
}
