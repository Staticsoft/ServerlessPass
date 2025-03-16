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
        .AddScoped<PasswordProfileRepository>()
        .AddSingleton<PasswordProfilesIdGenerator>();

    static ProfilesDocuments AddUserProfiles(IServiceProvider services)
        => services.GetRequiredService<Partitions>().GetFactory<PasswordProfilesDocument>().Get(services.GetRequiredService<Identity>().UserId);

    static Task Cors(HttpContext context, Func<Task> next)
    {
        if (!context.Request.Headers.Origin.Any()) return next();

        var origin = context.Request.Headers.Origin.Single();
        if (AllowedOrigins.Contains(origin))
        {
            context.Response.Headers["Access-Control-Allow-Origin"] = origin;
        }
        context.Response.Headers["Access-Control-Allow-Headers"] = "content-type, accept, origin, authorization";
        context.Response.Headers["Access-Control-Allow-Methods"] = "GET, POST, PUT, DELETE, OPTIONS";

        if (context.Request.Method != "OPTIONS") return next();

        context.Response.StatusCode = 200;
        return context.Response.CompleteAsync();
    }

    static string[] AllowedOrigins
        => Configuration("CrossOriginDomains").Split(',');

    static string Configuration(string name)
         => Environment.GetEnvironmentVariable(name) ?? throw new NullReferenceException($"Environment varialbe {name} is not set");
}
