using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantQueryHandler(ILogger<GetAllRestaurantQueryHandler>_logger,IRestaurantRepository _restaurantsRepo,IMapper _mapper) : IRequestHandler<GetAllRestaurantQuery, IEnumerable<RestaurantDto>>
{
    public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting All Restaurants [we are in Restaurants Service]");
        var result = await _restaurantsRepo.GetAllAsync();

        var restaurantsDto = _mapper.Map<IEnumerable<RestaurantDto>>(result);



        return restaurantsDto!;
    }
}
