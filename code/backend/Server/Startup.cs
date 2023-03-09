using Staticsoft.Contracts.ASP.Server;
using Staticsoft.PartitionedStorage.Abstractions;
using Staticsoft.Serialization.Net;
using Staticsoft.ServerlessPass.Authentication;
using Staticsoft.ServerlessPass.Services;
using System.Reflection;

namespace Staticsoft.ServerlessPass.Server;

public abstract class Startup
{
    const string CorsPolicy = nameof(CorsPolicy);

    public void ConfigureServices(IServiceCollection services)
        => RegisterServices(services);

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        => ConfigureApp(app, env);

    protected virtual IApplicationBuilder ConfigureApp(IApplicationBuilder app, IWebHostEnvironment env) => app
        .Use(Cors)
        .UseRouting()
        .UseServerAPIRouting<Schema>();

    protected virtual IServiceCollection RegisterServices(IServiceCollection services) => services
        .UseServerAPI<Schema>(Assembly.GetExecutingAssembly())
        .AddHttpContextAccessor()
        .UseSystemJsonSerializer()
        .AddSingleton<ItemSerializer, JsonItemSerializer>()
        .AddScoped<ServiceStatus, StorageStatus>()
        .AddScoped(AddUserProfiles)
        .AddSingleton<PasswordProfilesIdGenerator>();

    static ProfilesDocuments AddUserProfiles(IServiceProvider services)
        => services.GetRequiredService<Partitions>().GetFactory<PasswordProfilesDocument>().Get(services.GetRequiredService<Identity>().UserId);

    static Task Cors(HttpContext context, Func<Task> next)
    {
        context.Response.Headers["Access-Control-Allow-Origin"] = "chrome-extension://lcmbpoclaodbgkbjafnkbbinogcbnjih";
        context.Response.Headers["Access-Control-Allow-Headers"] = "x-requested-with, content-type, accept, origin, authorization, x-csrftoken, user-agent, accept-encoding, cache-control";
        context.Response.Headers["Access-Control-Allow-Methods"] = "GET, POST, PUT, DELETE, OPTIONS";

        if (context.Request.Method != "OPTIONS") return next();

        context.Response.StatusCode = 200;
        return context.Response.CompleteAsync();
    }
}
