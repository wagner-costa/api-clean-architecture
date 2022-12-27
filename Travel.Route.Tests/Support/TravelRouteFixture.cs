using Travel.Route.Domain.Entities;
using Travel.Route.Domain.Models;

namespace Travel.Route.Tests.Support
{
    public static class TravelRouteFixture
    {
        public static TravelRoute GetEntity()
        {
            var route = new TravelRoute()
            {
                Id = 10,
                Active = true,
                CreatedDate = DateTime.Now,
                Origin = "GUA",
                Destination = "CDG",
                Description = "GUA - BRC - SCL - ORL - CDG"
            };

            route.SetPrice(40);
            route.SetNumberOfStops(3);

            return route;
        }

        public static TravelRouteModel GetTravelRouteModel() => new TravelRouteModel();
    }
}
