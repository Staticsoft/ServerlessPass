using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Staticsoft.SharpPass.Server.AWS;

public class AWSStartup : Startup
{
    protected override void ConfigureApp(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseCors(policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
        );
    }
}
