using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Travel.Route.Domain.Exceptions;
using Travel.Route.Api.Models;

namespace Travel.Route.Api.Filter
{
    public class GlobalExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            var logDescription = $"{context.HttpContext.Request.Protocol} {context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";

            if (context.Exception is DomainException)
            {
                _logger.LogWarning(context.Exception, logDescription);

                context.Result = new JsonResult(new ErrorResponse(context.Exception.Message))
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            else if (context.Exception is DomainNotFoundException)
            {
                _logger.LogInformation(context.Exception, logDescription);

                context.Result = new JsonResult(new ErrorResponse(context.Exception.Message))
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            else
            {
                _logger.LogError(context.Exception, logDescription);

                context.Result = new JsonResult(new ErrorResponse("Unfortunately an error occurred while processing your request."))
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
