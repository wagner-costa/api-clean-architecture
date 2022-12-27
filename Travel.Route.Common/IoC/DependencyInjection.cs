using Microsoft.Extensions.DependencyInjection;
using Travel.Route.Data.Repositories;
using Travel.Route.Data.Seeds;
using Travel.Route.Domain.Contracts.Repositories;
using Travel.Route.Domain.Contracts.Services;
using Travel.Route.Domain.Services;

namespace Travel.Route.Common.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInjectorRegisterServices(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // Add services memory cache
            services.AddMemoryCache();

            // Repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnityOfWork, UnityOfWork>();

            // Add configure DI for application services
            services.AddScoped<ITravelRouteService, TravelRouteService>();
            services.AddScoped<IAccountService, AccountService>();

            // Seeds
            services.AddScoped<IDbInitializer, DbInitializer>();


            return services;
        }
    }
}