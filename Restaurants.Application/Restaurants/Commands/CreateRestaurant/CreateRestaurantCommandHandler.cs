using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using System.Reflection;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> _logger ,IMapper _mapper , IRestaurantRepository _restaurantsRepo) : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating Restaurant in Database {@Restaurant} ",request);

        Restaurant restaurant = _mapper.Map<Restaurant>(request);

        int id = await _restaurantsRepo.CreateAsync(restaurant);
        return id;
    }
}
