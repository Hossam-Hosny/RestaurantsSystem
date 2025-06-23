using MediatR;
using Restaurants.Application.Restaurants.DTOs;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantQuery:IRequest<IEnumerable<RestaurantDto>>
{

    public string? SearchPhrase { get; set; }

}
