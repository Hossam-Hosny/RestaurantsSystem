using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Authorization.Services;

public class restaurantAuthorizationService(ILogger<restaurantAuthorizationService> _logger
    , IUserContext userContext) : IrestaurantAuthorizationService
{
    public bool Authorize(Restaurant restaurant,ResourceOperation resourceOperation)
    {
        var user = userContext.GetCurrentUser();

        _logger.LogInformation("Authorizing user {email}, to {operation} for restaurant {restaurantName}"
            , user.Email, resourceOperation, restaurant.Name);

        if (resourceOperation is ResourceOperation.create || resourceOperation is ResourceOperation.read)
        {
            _logger.LogInformation("Create/read operation - successful authorization");
            return true;
        }

        if (resourceOperation is ResourceOperation.delete && user.IsInRole(UserRoles.Admin))
        {
            _logger.LogInformation("Admin user, delete operation - successful authorization");
            return true;
        }

        if ((resourceOperation is ResourceOperation.update || resourceOperation is ResourceOperation.delete)
            && user.Id == restaurant.OwnerId)
        {
            _logger.LogInformation("Restaurant owner - successful authorization ");
            return true;
        }

        return false;
    }

    
}
