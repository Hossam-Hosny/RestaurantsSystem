using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

internal class RestaurantsService(
    IRestaurantRepository _restaurantsRepo,
    ILogger<RestaurantsService> _logger,
    IMapper _mapper
    ) : IRestaurantsService
{
    public async Task<int> CreateRestaurant(RegisterRestaurantDto model)
    {
        _logger.LogInformation("Creating Restaurant in Database ");

        Restaurant restaurant = _mapper.Map<Restaurant>(model);

        int id = await _restaurantsRepo.CreateAsync(restaurant);
        return id;
    }

    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
    {
        _logger.LogInformation("Getting All Restaurants [we are in Restaurants Service]");
        var result = await _restaurantsRepo.GetAllAsync();

        var restaurantsDto = _mapper.Map<IEnumerable<RestaurantDto>>(result);

       

        return restaurantsDto!;
    }

    public async Task<RestaurantDto?> GetById(int id)
    {
        _logger.LogInformation($"Getting Restaurant of {id}");

        
        var result =await _restaurantsRepo.GetRestaurantById(id);

        var restaurantDto = _mapper.Map<RestaurantDto?>(result);

        return restaurantDto;


    }
}
