using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

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
}
