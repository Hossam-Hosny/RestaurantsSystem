using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetById;
using Restaurants.Domain.Constants;
using Restaurants.Infrastructure.Authorization;

namespace Restaurant.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RestaurantsController
    (IMediator _mediator
    ) : ControllerBase
{

    [HttpGet("get-all-restaurants")]
    //[Authorize(Policy =PolicyNames.AtLeast20)]
    //[Authorize(Policy =PolicyNames.CreatedAtleast2Restaurants)]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll([FromQuery]GetAllRestaurantQuery query)
    {
        var restaurants = await _mediator.Send(query);
        return Ok(restaurants);
    }

    [HttpGet("[action]/{id}")]
    [Authorize(Policy = PolicyNames.HasNationality)]
    public async Task<ActionResult<RestaurantDto?>> GetRestaurant([FromRoute] int id)
    {
        var restaurant = await _mediator.Send(new GetByIdQuery(id));

        if (restaurant is null) { return NotFound(); }
        return Ok(restaurant);
    }


    [HttpPost]
    [Authorize(Roles =UserRoles.Owner)]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand command)
    {

        int id = await _mediator.Send(command);
        return CreatedAtAction("GetRestaurant" ,new {id},null);

    }




    [HttpDelete("[action]/{id}")]
    public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
    {
        var isDeleted = await _mediator.Send(new DeleteRestaurantCommand(id));

        if (isDeleted) return NoContent();

        return NotFound();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, UpdateRestaurantCommand command)
    {
        command.Id = id;
        var isUpdated = await _mediator.Send(command);

        if (isUpdated) return NoContent();

        return NotFound();
    }




}
