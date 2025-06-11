using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetById;

public class GetByIdQueryHandler(ILogger<GetByIdQueryHandler> _logger,IMapper _mapper,IRestaurantRepository _restaurantsRepo) : IRequestHandler<GetByIdQuery, RestaurantDto?>
{
    public async Task<RestaurantDto?> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Getting Restaurant of {request.Id}");


        var result = await _restaurantsRepo.GetRestaurantById(request.Id);

        var restaurantDto = _mapper.Map<RestaurantDto?>(result);

        return restaurantDto;
    }
}
