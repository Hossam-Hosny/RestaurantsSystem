
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetAllDishes;

public class GetDishesForRestaurantQueryHandler(ILogger<GetDishesForRestaurantQueryHandler>_logger
    ,IRestaurantRepository _restaurantRepository
    ,IMapper _mapper) : IRequestHandler<GetDishesForRestaurantQuery, IEnumerable<DishDto>>
{
    public async Task<IEnumerable<DishDto>> Handle(GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving dishes for restaurant with id:{restaurantId}", request.RestaurantId);
        var restaurant = await _restaurantRepository.GetRestaurantById(request.RestaurantId);

        if (restaurant is null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var result = _mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);
        return result;


    }
}
