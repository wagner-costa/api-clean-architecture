using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Travel.Route.Api.Models;
using Travel.Route.Common;
using Travel.Route.Domain.Contracts.Services;

namespace Travel.Route.Api.Filter
{
    public class AuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly IAccountService _accountService;
        private readonly IHostEnvironment _environment;
        private readonly ILogger<AuthorizationFilter> _logger;

        public AuthorizationFilter(IAccountService accountService, IHostEnvironment environment, ILogger<AuthorizationFilter> logger)
        {
            _accountService = accountService;
            _environment = environment;
            _logger = logger;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!_environment.IsDevelopmentOrLocal())
            {
                try
                {
                    // For endpoints without AllowAnonymous attribute
                    if (!context.ActionDescriptor.EndpointMetadata.Any(em => em.GetType() == typeof(AllowAnonymousAttribute)))
                    {
                        var token = context.HttpContext.Request.Headers["Authorization"];

                        var hasPermission = await _accountService.HasPermission(token, context.HttpContext.Request.Method, context.ActionDescriptor.AttributeRouteInfo.Template);

                        if (!hasPermission)
                        {
                            Unauthorize(context);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error authorizing.");

                    Unauthorize(context);
                }
            }
        }

        private void Unauthorize(AuthorizationFilterContext context)
        {
            context.Result = new JsonResult(new ErrorResponse("Unauthorized"));
            context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
        }
    }
}
