using Travel.Route.Domain.Models;

namespace Travel.Route.Domain.Contracts.Services
{
    public interface ITravelRouteService
    {
        Task<TravelRouteModel> GetByIdAsync(int id);

        Task<List<TravelRouteModel>> GetAsync(TravelRouteQuery query);

        Task<TravelRouteModel> AddAsync(TravelRouteModel model);

        Task DeleteAsync(int id);

        Task<TravelRouteModel> UpdateAsync(TravelRouteModel model);
    }
}
