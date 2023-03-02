using Staticsoft.Contracts.ASP.Server;
using Staticsoft.HttpCommunication.Json;
using Staticsoft.Serialization.Net;
using System.Reflection;

namespace Staticsoft.SharpPass.Server;

public abstract class Startup
{
    public void ConfigureServices(IServiceCollection services)
        => RegisterServices(services);

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        => ConfigureApp(app, env);

    protected virtual IApplicationBuilder ConfigureApp(IApplicationBuilder app, IWebHostEnvironment env) => app
        .UseRouting()
        .UseServerAPIRouting<Schema>();

    protected virtual IServiceCollection RegisterServices(IServiceCollection services) => services
        .UseServerAPI<Schema>(Assembly.GetExecutingAssembly())
        .AddHttpContextAccessor()
        .AddCors()
        .UseSystemJsonSerializer()
        .UseJsonHttpCommunication()
        .AddSingleton<Documents>();
}
