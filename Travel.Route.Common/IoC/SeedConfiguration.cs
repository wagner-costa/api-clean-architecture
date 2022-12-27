using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Travel.Route.Data.Seeds;

namespace Travel.Route.Common.IoC
{
    public static class SeedConfiguration
    {
        public static void SeedingInvoke(this IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetService<IDbInitializer>();
                dbInitializer.SeedData();
            }
        }
    }
}
