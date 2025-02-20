using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Users.Command.UpdateUserDetails;
using Restaurant.Application.Users.Command.AssignUserRole;
using Restaurant.Application.Users.Command.UnassignUserRole;
using Restaurant.Domain.Contants;
using MediatR;

namespace Resturants.API.Controllers
{
    [ApiController]
    [Route("api/identity")]
    public class IdentityController(IMediator mediator) : ControllerBase
    {
        [HttpPatch("user")]
        [Authorize]
        public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }


        [HttpPost("userRole")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("userRole")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<IActionResult> UnassignUserRole(UnassignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }
    }
}
