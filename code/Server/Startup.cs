using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Staticsoft.SharpPass.Server;

public abstract class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddCors();
        RegisterServices(services);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        ConfigureApp(app, env);

        app.UseRouting();

        app.UseEndpoints(ConfigureEndpoints);
    }

    protected virtual void ConfigureApp(IApplicationBuilder app, IWebHostEnvironment env) { }
    protected virtual IServiceCollection RegisterServices(IServiceCollection services)
        => services;

    void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
        => endpoints.MapControllers();
}
