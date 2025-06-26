using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using System.Reflection;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> _logger 
    ,IMapper _mapper 
    , IRestaurantRepository _restaurantsRepo
    ,IUserContext userContext) 
    : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        _logger.LogInformation("{email} [{id}] is Creating Restaurant in Database {@Restaurant} "
            ,currentUser.Email,currentUser.Id,request);

        Restaurant restaurant = _mapper.Map<Restaurant>(request);
        restaurant.OwnerId = currentUser.Id;


        int id = await _restaurantsRepo.CreateAsync(restaurant);
        return id;
    }
}
