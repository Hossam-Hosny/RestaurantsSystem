using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteDishes;

public class DeleteDishesForRestaurantCommandHandler(ILogger<DeleteDishesForRestaurantCommandHandler> _logger,
    IRestaurantRepository _restaurantRepository,
    IDishRepository _dishRepository) : IRequestHandler<DeleteDishesForRestaurantCommand>
{
    public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogWarning("Deleting dishes from restaurant of id:{restaurantId}",request.restaurantId);

        var restaurant = await _restaurantRepository.GetRestaurantById(request.restaurantId);
        if (restaurant is null) throw new NotFoundException(nameof(Restaurant),request.restaurantId.ToString());

        await _dishRepository.DeleteAsync(restaurant.Dishes);






    }
}
