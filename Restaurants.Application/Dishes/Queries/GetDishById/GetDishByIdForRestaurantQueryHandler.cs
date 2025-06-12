using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDishById;

public class GetDishByIdForRestaurantQueryHandler(ILogger<GetDishByIdForRestaurantQueryHandler>_logger,
    IRestaurantRepository _restaurantRepository,
    IMapper _mapper) : IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
{
    public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("retriving from Restaurant of id : {restaurantId} dish of id :{dishId}", request.RestaurantId, request.DishId);

        var restaurant = await _restaurantRepository.GetRestaurantById(request.RestaurantId);
        if (restaurant is null) throw new NotFoundException(nameof(Restaurant),request.RestaurantId.ToString());

        var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId);
        if (dish is null) throw new NotFoundException(nameof(Dish), request.DishId.ToString());

        var result = _mapper.Map<DishDto>(dish);
        return result;






    }
}
