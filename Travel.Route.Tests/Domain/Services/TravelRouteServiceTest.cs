using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Travel.Route.Domain.Contracts.Repositories;
using Travel.Route.Domain.Contracts.Services;
using Travel.Route.Domain.Entities;
using Travel.Route.Domain.Exceptions;
using Travel.Route.Domain.Services;
using Travel.Route.Tests.Support;

namespace Travel.Route.Tests.Domain.Services
{
    [TestClass]
    public class TravelRouteServiceTest
    {
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IRepository<TravelRoute>> _repositoryMock;
        private readonly Mock<IUnityOfWork> _unityOfWorkMock;
        private readonly ITravelRouteService _travelRouteService;

        public TravelRouteServiceTest()
        {
            _mapper = new Mock<IMapper>();
            _repositoryMock = new Mock<IRepository<TravelRoute>>();
            _unityOfWorkMock = new Mock<IUnityOfWork>();

            _travelRouteService = new TravelRouteService(
                _mapper.Object,
                _repositoryMock.Object,
                _unityOfWorkMock.Object);
        }

        [TestMethod]
        public async Task Add_WhenBeCalledWithIdGreaterThanZero_ThenShouldThrowsDomainException()
        {
            // Arrange
            var route = TravelRouteFixture.GetTravelRouteModel();
            route.Id = 10;

            // Action
            var result = await Assert.ThrowsExceptionAsync<DomainException>(async () => await _travelRouteService.AddAsync(route));

            // Assert
            Assert.IsTrue(result is DomainException);
        }
    }
}