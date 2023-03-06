using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.AspNetCoreServer;
using Amazon.Lambda.AspNetCoreServer.Internal;
using Amazon.Lambda.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Staticsoft.SharpPass.Authentication.ASP;

namespace Staticsoft.SharpPass.Server.AWS;

public class LambdaEntryPoint : APIGatewayProxyFunction
{
    protected override void Init(IWebHostBuilder builder)
        => builder.UseStartup<AWSStartup>();

    protected override void Init(IHostBuilder builder) { }

    protected override void MarshallRequest(InvokeFeatures features, APIGatewayProxyRequest apiGatewayRequest, ILambdaContext lambdaContext)
    {
        SetAuthenticationContext(features, apiGatewayRequest);

        base.MarshallRequest(features, apiGatewayRequest, lambdaContext);
    }

    static void SetAuthenticationContext(InvokeFeatures features, APIGatewayProxyRequest apiGatewayRequest)
    {
        var claims = apiGatewayRequest.RequestContext.Authorizer?.Claims;
        if (claims != null) features.Set(new Claims(claims));
    }
}
