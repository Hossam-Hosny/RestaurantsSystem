using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler>_logger
    ,IRestaurantRepository _restaurantRepo
    ,IrestaurantAuthorizationService restaurantAuthorizationService)
    : IRequestHandler<DeleteRestaurantCommand,bool>
{
    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await _restaurantRepo.GetRestaurantById(request.Id);
        if (restaurant is null)
            throw new NotFoundException("Restaurant",request.Id.ToString());

        _logger.LogInformation("Deleting Restaurant in DataBase {RestaurantId}",request.Id);

        if (!restaurantAuthorizationService.Authorize(restaurant,ResourceOperation.delete))
            throw new ForbidException();

        await _restaurantRepo.DeleteAsync(restaurant);
        return true;

    }
}
