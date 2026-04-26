using Core.Feature.Authorization.Command.Models;
using Core.Feature.Authorization.Query.Models;
using Core.Feature.Authorization.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SocialMedia_App.Controllers
{
    [Authorize(Roles ="admin")]
    public class AuthorizationController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDeatailsResult>>>GetAllRoles()
        {
            var query = new GetAllRolesQuery();
            var result =await _Mediator.Send(query);
            return NewResult(result);
        }

        [HttpPost]
        public async Task<IActionResult>CreateRole (AddNewRoleCommand cmd)
        {
            var result =await _Mediator.Send(cmd);
            return NewResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleToUser(AssignUserToRoleCommand cmd)
        {
            var result = await _Mediator.Send(cmd);
            return NewResult(result);
        }


        [HttpPut]
        public async Task<IActionResult>EditRole([FromForm] EditRoleCommand cmd)
        {
            var result =await _Mediator.Send(cmd);  
            return NewResult(result);
        }


        [HttpDelete]
        public async Task<IActionResult>RemoveRole (int RoleId )
        {
            var cmd = new DeleteRoleCommand(RoleId);
            var result=await _Mediator.Send(cmd);
            return NewResult(result);
        }
    }
}
