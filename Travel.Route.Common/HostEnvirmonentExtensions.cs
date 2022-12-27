using Microsoft.Extensions.Hosting;

namespace Travel.Route.Common
{
    public static class HostEnvirmonentExtensions
    {
        public static bool IsDevelopmentOrLocal(this IHostEnvironment environment)
        {
            return environment.IsDevelopment() || environment.IsEnvironment("Local");
        }
    }
}
