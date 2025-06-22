using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Interfaces
{
    public interface IrestaurantAuthorizationService
    {
        bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation);
    }
}