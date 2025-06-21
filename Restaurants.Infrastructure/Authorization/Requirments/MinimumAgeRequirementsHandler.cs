using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;

namespace Restaurants.Infrastructure.Authorization.Requirments;

public class MinimumAgeRequirementsHandler(ILogger<MinimumAgeRequirementsHandler> _logger
    ,IUserContext userContext)
    : AuthorizationHandler<MinimumAgeRequirements>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirements requirement)
    {
        var currentUser = userContext.GetCurrentUser();


        _logger.LogInformation("User: {email}, date of birth {dob} - Handling MinimumAgeRequirements "
            ,currentUser.Email,currentUser.DateOfBirth);

        if (currentUser.DateOfBirth is null)
        {
            _logger.LogWarning("User date of birth is null");
            context.Fail();
            return Task.CompletedTask;
        }

        if (currentUser.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
        {
            _logger.LogInformation("Authorization succeeded");
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();

        }


        return Task.CompletedTask;

    }
}
