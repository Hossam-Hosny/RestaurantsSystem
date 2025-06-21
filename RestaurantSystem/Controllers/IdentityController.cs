using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Users.Commands.AssignUserRole;
using Restaurants.Application.Users.Commands.UnAssignUserRole;
using Restaurants.Application.Users.Commands.UpdateUserDetails;
using Restaurants.Domain.Constants;

namespace Restaurant.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdentityController(IMediator _mediator):ControllerBase
{

    [HttpPatch("user")]
    [Authorize]
    public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand command)
    {
        await _mediator.Send(command);
        return NoContent();

    }


    [HttpPost("[action]")]
    [Authorize(Roles =UserRoles.Admin)]
    public async Task <IActionResult> AssignUserRole(AssignUserRoleCommand command)
    {
      await  _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("[action]")]
    [Authorize(Roles =UserRoles.Admin)]
    public async Task <IActionResult> UnassignUserRole(UnAssignUserRoleCommand command )
    {
        await _mediator.Send(command);
        return NoContent();
    }


}
