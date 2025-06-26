using MediatR;
using Restaurants.Application.Restaurants.DTOs;

namespace Restaurants.Application.Restaurants.Queries.GetById;

public class GetByIdQuery(int id):IRequest<RestaurantDto?>
{
    public int Id { get; } = id;
}
