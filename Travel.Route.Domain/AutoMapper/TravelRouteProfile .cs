using AutoMapper;
using Travel.Route.Domain.Entities;
using Travel.Route.Domain.Models;

namespace Travel.Route.Domain.AutoMapper
{
    public class TravelRouteProfile : Profile
    {
        public TravelRouteProfile()
        {
            CreateMap<TravelRoute, TravelRouteModel>()
                .ReverseMap();
        }
    }
}
