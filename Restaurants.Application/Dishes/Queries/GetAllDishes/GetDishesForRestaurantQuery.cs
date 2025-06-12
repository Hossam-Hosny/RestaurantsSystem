using MediatR;
using Restaurants.Application.Dishes.DTOs;

namespace Restaurants.Application.Dishes.Queries.GetAllDishes;

public class GetDishesForRestaurantQuery(int restaurantId):IRequest<IEnumerable<DishDto>>
{
    public int RestaurantId { get; } = restaurantId;
}
