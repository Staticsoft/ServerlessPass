using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.AspNetCoreServer;
using Amazon.Lambda.AspNetCoreServer.Internal;
using Amazon.Lambda.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Hosting;
using Staticsoft.ServerlessPass.Authentication.ASP;

namespace Staticsoft.ServerlessPass.Server.AWS;

public class LambdaEntryPoint : APIGatewayProxyFunction
{
    protected override void Init(IWebHostBuilder builder)
        => builder.UseStartup<AWSStartup>();

    protected override void Init(IHostBuilder builder) { }

    protected override void MarshallRequest(InvokeFeatures features, APIGatewayProxyRequest request, ILambdaContext lambdaContext)
    {
        SetAuthenticationContext(features, request);
        HandleCloudWatchRequest(request);

        base.MarshallRequest(features, request, lambdaContext);
    }

    static void SetAuthenticationContext(InvokeFeatures features, APIGatewayProxyRequest apiGatewayRequest)
    {
        var claims = apiGatewayRequest.RequestContext.Authorizer?.Claims;
        if (claims != null)
        {
            features.Set(new Claims(claims));
        }
    }

    static void HandleCloudWatchRequest(APIGatewayProxyRequest request)
    {
        if (request.HttpMethod == null)
        {
            request.Path = "/";
            request.Resource = "/";
            request.HttpMethod = "POST";
        }
    }

    protected override void PostMarshallRequestFeature(IHttpRequestFeature request, APIGatewayProxyRequest lambdaRequest, ILambdaContext lambdaContext)
    {
        HandleCloudWatchRequest(request, lambdaRequest);
        base.PostMarshallRequestFeature(request, lambdaRequest, lambdaContext);
    }

    static void HandleCloudWatchRequest(IHttpRequestFeature request, APIGatewayProxyRequest lambdaRequest)
    {
        if (lambdaRequest.RequestContext.EventType == "CloudWatchTrigger")
        {
            request.Path = lambdaRequest.RequestContext.ResourcePath;
        }
    }
}
