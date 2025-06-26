using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantQueryHandler(ILogger<GetAllRestaurantQueryHandler>_logger
    ,IRestaurantRepository _restaurantsRepo
    ,IMapper _mapper)
    : IRequestHandler<GetAllRestaurantQuery, PageResult<RestaurantDto>>
{
    public async Task<PageResult<RestaurantDto>> Handle(GetAllRestaurantQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting All Restaurants [we are in Restaurants GetAll Query Handler ]");
        var (result,totalCount) = await _restaurantsRepo
            .GetAllMatchingAsync(request.SearchPhrase
            ,request.PageNumber
            ,request.PageSize
            ,request.SortBy
            ,request.SortDirection);

        var restaurantsDto = _mapper.Map<IEnumerable<RestaurantDto>>(result);

        var Finallresult = new PageResult<RestaurantDto>(restaurantsDto,totalCount,request.PageSize,request.PageNumber);

        return Finallresult;
    }
}
