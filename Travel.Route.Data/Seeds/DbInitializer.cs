using Microsoft.Extensions.DependencyInjection;
using Travel.Route.Data.Context;
using Travel.Route.Domain.Entities;

namespace Travel.Route.Data.Seeds
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbInitializer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<AppDbContext>())
                {
                    if (!context.Routes.Any())
                    {
                        var route1 = new TravelRoute()
                        {
                            Active = true,
                            CreatedDate = DateTime.Now,
                            Origin = "GUA",
                            Destination = "CDG",
                            Description = "GUA - BRC - SCL - ORL - CDG"
                        };

                        route1.SetPrice(40);
                        route1.SetNumberOfStops(3);

                        context.Routes.AddAsync(route1);

                        var route2 = new TravelRoute()
                        {
                            Active = true,
                            CreatedDate = DateTime.Now,
                            Origin = "GUA",
                            Destination = "CDG",
                            Description = "GUA - ORL - CDG"
                        };

                        route2.SetPrice(61);
                        route2.SetNumberOfStops(1);

                        context.Routes.AddAsync(route2);

                        var route3 = new TravelRoute()
                        {
                            Active = true,
                            CreatedDate = DateTime.Now,
                            Origin = "GUA",
                            Destination = "CDG",
                            Description = "GUA - CDG"
                        };

                        route3.SetPrice(75);
                        route3.SetNumberOfStops(0);

                        context.Routes.AddAsync(route3);

                        var route4 = new TravelRoute()
                        {
                            Active = true,
                            CreatedDate = DateTime.Now,
                            Origin = "GUA",
                            Destination = "CDG",
                            Description = "GUA - SCL - ORL - CDG"
                        };

                        route4.SetPrice(45);
                        route4.SetNumberOfStops(2);

                        context.Routes.AddAsync(route4);

                        context.SaveChanges();
                    }
                }
            }
        }
    }
}