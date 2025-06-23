using FluentValidation;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryValidator:AbstractValidator<GetAllRestaurantQuery>
{
    private int[] allowedPageSizes = [5, 10, 15, 20, 30];
    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(r => r.PageSize)
            .Must(value => allowedPageSizes.Contains(value))
            .WithMessage($"Page Size must be in [{string.Join(",",allowedPageSizes)}]");


    }
}
