using MediatR;

namespace Restaurants.Application.Dishes.Commands.DeleteDishes;

public class DeleteDishesForRestaurantCommand(int restaurantId):IRequest
{
    public int restaurantId { get; } = restaurantId;
}
