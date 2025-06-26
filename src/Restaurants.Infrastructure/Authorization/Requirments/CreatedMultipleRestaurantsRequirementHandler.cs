using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorization.Requirments;

internal class CreatedMultipleRestaurantsRequirementHandler (IUserContext userContext
    ,IRestaurantRepository _repo)
    : AuthorizationHandler<CreatedMultipleRestaurantsRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipleRestaurantsRequirement requirement)
    {
        var user = userContext.GetCurrentUser();

        var restaurants = await _repo.GetAllAsync();
        var userRestaurantsCreated = restaurants.Count(r=>r.OwnerId == user!.Id);

        if (userRestaurantsCreated >= requirement.MinimumRestaurantsCreated)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }


    }
}
