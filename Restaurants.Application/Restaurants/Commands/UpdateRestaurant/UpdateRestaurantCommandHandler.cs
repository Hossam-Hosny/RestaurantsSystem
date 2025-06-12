using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> _logger,IRestaurantRepository _restaurantRepo, IMapper _mapper) : IRequestHandler<UpdateRestaurantCommand, bool>
{
    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await _restaurantRepo.GetRestaurantById(request.Id);
        if (restaurant is null)
            throw new NotFoundException($"Restaurant with {request.Id} does not exist");


        _logger.LogInformation("Updateing Restaurant with id: {RestaurantId} with {@updatedRestaurant}",request.Id,request);

        _mapper.Map(request, restaurant);
        await _restaurantRepo.SaveChanges();


        return true;
    }
}
