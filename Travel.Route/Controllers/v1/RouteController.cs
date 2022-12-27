using Microsoft.AspNetCore.Mvc;
using Travel.Route.Domain.Contracts.Services;
using Travel.Route.Domain.Models;

namespace Travel.Route.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/route")]
    public class RouteController : ControllerBase
    {
        private readonly ITravelRouteService _routeService;

        public RouteController(
            ITravelRouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TravelRouteQuery query)
        {
            return Ok(await _routeService.GetAsync(query));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var model = await _routeService.GetByIdAsync(id);

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TravelRouteModel model)
        {
            var modelCreated = await _routeService.AddAsync(model);

            return Created(nameof(GetById), modelCreated);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] TravelRouteModel model)
        {
            var modelUpdated = await _routeService.UpdateAsync(model);

            return Ok(modelUpdated);
        }

        [HttpDelete("{id:int}")]
        public async Task<NoContentResult> Delete(int id)
        {
            await _routeService.DeleteAsync(id);
                
            return NoContent();
        }
    }
}
