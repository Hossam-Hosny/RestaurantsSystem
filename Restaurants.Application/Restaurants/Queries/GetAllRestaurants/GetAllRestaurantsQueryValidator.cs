using FluentValidation;
using Restaurants.Application.Restaurants.DTOs;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryValidator:AbstractValidator<GetAllRestaurantQuery>
{
    private int[] allowedPageSizes = [5, 10, 15, 20, 30];
    private string[] allowedSortByColumNames = [nameof(RestaurantDto.Name),nameof(RestaurantDto.Description),nameof(RestaurantDto.Category)];
    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(r => r.PageSize)
            .Must(value => allowedPageSizes.Contains(value))
            .WithMessage($"Page Size must be in [{string.Join(",",allowedPageSizes)}]");

        
        RuleFor(r => r.SortBy)
            .Must(value => allowedSortByColumNames.Contains(value))
            .When(q=>q.SortBy is not null)
            .WithMessage($"Sort By is optional, or must be in [{string.Join(",",allowedSortByColumNames)}]");


    }
}
