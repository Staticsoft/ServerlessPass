using Amazon.Lambda.AspNetCoreServer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Staticsoft.SharpPass.Server.AWS;

public class LambdaEntryPoint : APIGatewayProxyFunction
{
    protected override void Init(IWebHostBuilder builder)
        => builder.UseStartup<AWSStartup>();

    protected override void Init(IHostBuilder builder) { }
}
