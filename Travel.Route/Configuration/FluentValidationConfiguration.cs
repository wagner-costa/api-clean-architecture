using Microsoft.AspNetCore.Mvc;
using Travel.Route.Api.Models;

namespace Travel.Route.Api.Configuration
{

    public static class FluentValidationConfiguration
    {
        public static void ConfigureResultErrors(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage)).ToList();

                    return new BadRequestObjectResult(new ErrorResponse(errors));
                };
            });
        }
    }
}
