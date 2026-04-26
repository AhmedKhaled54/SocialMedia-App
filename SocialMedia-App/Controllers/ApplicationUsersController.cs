using Core.Feature.ApplicationUser.Command.Models;
using Core.Feature.ApplicationUser.Query.Model;
using Core.Feature.ApplicationUser.Query.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SocialMedia_App.Controllers
{
    [Authorize(Roles ="admin")]
    public class ApplicationUsersController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetailsResult>>>GetAllUsers()
        {
            var query = new GetAllUserQuery();
            var result = await _Mediator.Send(query);
            return NewResult(result);
        }

        [HttpGet]
        public async Task<ActionResult<UserDetailsResult>> GetUserById( int UserId)
        {
            var query = new GetUserByIdQuery(UserId);
            var result =await _Mediator.Send(query);
            return NewResult(result);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveUser(int UserId)
        {
            var cmd = new RemoveUserCommand(UserId);
            var result =await _Mediator.Send(cmd);
            return NewResult(result);

        }
    }
}
