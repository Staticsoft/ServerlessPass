using Staticsoft.HttpCommunication.Abstractions;

namespace Staticsoft.SharpPass.Tests;

public class AuthenticatedHttpRequestExecutor : HttpRequestExecutor
{
    public string Token = string.Empty;

    readonly HttpRequestExecutor Executor;

    public AuthenticatedHttpRequestExecutor(HttpRequestExecutor executor)
        => Executor = executor;

    public Task<HttpResponse> Execute(HttpRequest request)
        => Token == string.Empty
        ? Executor.Execute(request)
        : Executor.Execute(request.WithHeader("Authorization", $"JWT {Token}"));
}
