using Microsoft.AspNetCore.Diagnostics;
using SocialNetworkApi.Persistence.DataBase;

namespace SocialNetworkApi.RequestPipeline
{
    public static class WebApplicationExtensions
    {
        public static WebApplication InitializeDatabase(this WebApplication app)
        {
            var connectionString = app.Configuration[DbConstants.DefaultConnectionPath];
            if (connectionString != null)
            {
                DbInitializer.Initialize(connectionString);
            }

            return app;
        }

        public static WebApplication UseGlobalErrorHandling(this WebApplication app)
        {
            app.UseExceptionHandler("/error");

            app.Map("/error", (HttpContext httpContext) =>
            {
                var exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

                return Results.Problem();
            });

            return app;
        }
    }
 
}
