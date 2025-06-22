using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> _logger
    ,IRestaurantRepository _restaurantRepo
    , IMapper _mapper
    ,IrestaurantAuthorizationService restaurantAuthorizationService)
    : IRequestHandler<UpdateRestaurantCommand, bool>
{
    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await _restaurantRepo.GetRestaurantById(request.Id);
        if (restaurant is null)
            throw new NotFoundException("Restaurant",request.Id.ToString());


        _logger.LogInformation("Updateing Restaurant with id: {RestaurantId} with {@updatedRestaurant}",request.Id,request);

        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.update))
            throw new ForbidException();

        _mapper.Map(request, restaurant);
        await _restaurantRepo.SaveChanges();


        return true;
    }
}
