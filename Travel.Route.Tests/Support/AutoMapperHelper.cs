using AutoMapper;
using Travel.Route.Domain.AutoMapper;

namespace Travel.Route.Tests.Support
{
    public static class AutoMapperHelper
    {
        public static IMapper GetMapper()
        {
            return GetMapperConfiguration().CreateMapper();
        }

        public static MapperConfiguration GetMapperConfiguration()
        {
            var config = new MapperConfiguration(mc =>
                                    mc.AddProfiles(new List<Profile>
                                    {
                                            new TravelRouteProfile()
                                    }));

            return config;
        }
    }
}
