using MediatR;

namespace Restaurants.Application.Users.Commands.UnAssignUserRole;

public class UnAssignUserRoleCommand : IRequest
{
    public string Email { get; set; } = default!;
    public string Role { get; set; } = default!;
}
