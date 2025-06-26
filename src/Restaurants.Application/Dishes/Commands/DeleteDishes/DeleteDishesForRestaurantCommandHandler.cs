using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteDishes;

public class DeleteDishesForRestaurantCommandHandler(ILogger<DeleteDishesForRestaurantCommandHandler> _logger,
    IRestaurantRepository _restaurantRepository
   , IDishRepository _dishRepository
    ,IrestaurantAuthorizationService restaurantAuthorizationService) 
    : IRequestHandler<DeleteDishesForRestaurantCommand>
{
    public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogWarning("Deleting dishes from restaurant of id:{restaurantId}",request.restaurantId);

        var restaurant = await _restaurantRepository.GetRestaurantById(request.restaurantId);
        if (restaurant is null) throw new NotFoundException(nameof(Restaurant),request.restaurantId.ToString());

        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.delete))
            throw new ForbidException();

        await _dishRepository.DeleteAsync(restaurant.Dishes);






    }
}
