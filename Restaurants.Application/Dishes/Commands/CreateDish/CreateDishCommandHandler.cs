using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler>_logger
    ,IRestaurantRepository _restaurantRepository
    ,IDishRepository _dishRepository
    ,IMapper _mapper
    ,IrestaurantAuthorizationService restaurantAuthorizationService) 
    : IRequestHandler<CreateDishCommand,int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new dish:{@DishRequest}", request);
        var restaurant = await _restaurantRepository.GetRestaurantById(request.RestaurantId);
        if (restaurant == null) throw new NotFoundException(nameof(restaurant), request.RestaurantId.ToString());

        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.update))
            throw new ForbidException();

        var dish = _mapper.Map<Dish>(request);


        var dishId = await _dishRepository.CreateAsync(dish);
        return dishId;


    }
}
