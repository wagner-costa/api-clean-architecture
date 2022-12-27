using AutoMapper;
using Travel.Route.Domain.Contracts.Repositories;
using Travel.Route.Domain.Contracts.Services;
using Travel.Route.Domain.Entities;
using Travel.Route.Domain.Exceptions;
using Travel.Route.Domain.Models;

namespace Travel.Route.Domain.Services
{
    public class TravelRouteService : ITravelRouteService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TravelRoute> _repository;
        private readonly IUnityOfWork _unityOfWork;

        public TravelRouteService(
            IMapper mapper,
            IRepository<TravelRoute> repository,
            IUnityOfWork unityOfWork)
        {
            _mapper = mapper;
            _repository = repository;
            _unityOfWork = unityOfWork;
        }
        public async Task<TravelRouteModel> AddAsync(TravelRouteModel model)
        {
            if (model.Id > 0)
            {
                throw new DomainException("The field id must not be greater than 0.");
            }

            return await AddOrUpdate(model);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetById(id);
            entity.Inactivate();
        }

        public async Task<List<TravelRouteModel>> GetAsync(TravelRouteQuery query)
        {
            var response = _mapper.Map<List<TravelRouteModel>>(await _repository.GetAsync(
                                        w => (w.Id == query.Id || query.Id == 0))).ToList();
            return response;
        }

        public async Task<TravelRouteModel> GetByIdAsync(int id)
        {
            return _mapper.Map<TravelRouteModel>(await GetById(id));
        }

        public async Task<TravelRouteModel> UpdateAsync(TravelRouteModel model)
        {
            if (model.Id == 0)
            {
                throw new DomainException("The field id must be greater than 0.");
            }

            return await AddOrUpdate(model);
        }

        private async Task<TravelRouteModel> AddOrUpdate(TravelRouteModel model)
        {
            var entity = (model.Id > 0) ?
                await GetById(model.Id) :
                await _repository.AddAsync(new TravelRoute());

            _mapper.Map(model, entity);

            await _unityOfWork.CommitAsync();

            model.Id = entity.Id;

            return model;
        }

        private async Task<TravelRoute> GetById(int id)
        {
            var entity = await _repository.FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                throw new DomainNotFoundException($"Route with id {id} not found.");
            }

            if (!entity.Active)
            {
                throw new DomainException($"{nameof(TravelRoute)} is inactive.");
            }

            return entity;
        }
    }
}
