using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.DTOs;

namespace Restaurant.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RestaurantsController
    (IRestaurantsService _restaurantsService
    ) : ControllerBase
{

    [HttpGet("get-all-restaurants")]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await _restaurantsService.GetAllRestaurants();
        return Ok(restaurants);
    }

    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> GetRestaurant([FromRoute] int id)
    {
       var restaurant = await  _restaurantsService.GetById(id);

        if (restaurant is null) { return NotFound(); }
        return Ok(restaurant);
    }


    [HttpPost]
    public async Task<IActionResult> CreateRestaurant([FromBody] RegisterRestaurantDto dto)
    {
        int id =await _restaurantsService.CreateRestaurant(dto);
        return CreatedAtAction("GetRestaurant" ,new {id},null);

    }


}
