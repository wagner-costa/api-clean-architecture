using FluentValidation;
using Travel.Route.Domain.Models;

namespace Travel.Route.Domain.Validators
{
    public class RouteModelValidator : AbstractValidator<TravelRouteModel>
    {
        public RouteModelValidator()
        {
            RuleFor(route => route.Origin)
                .NotNull()
                .WithMessage("Origin field is required.")
                .NotEmpty()
                .WithMessage("Origin field is required.");

            RuleFor(route => route.Destination)
                .NotNull()
                .WithMessage("Destination field is required.")
                .NotEmpty()
                .WithMessage("Destination field is required.");

            RuleFor(route => route.NumberOfStops)
                .GreaterThan(0)
                .WithMessage("NumberOfStops field must be greater than 0.");
        }
    }
}
