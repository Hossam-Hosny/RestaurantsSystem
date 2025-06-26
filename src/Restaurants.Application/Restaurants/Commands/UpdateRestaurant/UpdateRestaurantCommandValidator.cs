using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandValidator:AbstractValidator<UpdateRestaurantCommand>
{
    public UpdateRestaurantCommandValidator()
    {
        RuleFor(c => c.Name)
            .Length(3, 50);
        RuleFor(c => c.Description)
            .NotEmpty()
            .WithMessage("Please add Description");

        RuleFor(r => r.HasDelivery)
            .NotEmpty()
            .WithMessage("Answer Plaeas has Delivery or Not ");
    }
}
