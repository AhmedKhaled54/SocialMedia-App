using Core.Feature.Follow.Command.Models;
using Core.Feature.Follow.Query.Models;
using Core.Feature.Follow.Query.Results;
using Core.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SocialMedia_App.Controllers
{
    public class FollowController : BaseController
    {

        [HttpGet]
        public async Task<ActionResult<Pagination<GetFollowersQueryResult>>>GetFollowers([FromQuery] GetAllFollowerQuery query )
        {
            var result = await _Mediator.Send(query);
            return NewResult(result);
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<GetFollowersQueryResult>>> GetFollowing([FromQuery] GetAllFollowingQuery query)
        {
            var result =await _Mediator.Send(query);
            return NewResult(result);
        }

        [HttpPost]
        public async Task<IActionResult>FollowToUser(FollowRequestCommand cmd)
        {
            var result =await _Mediator.Send(cmd);
            return NewResult(result);
        }


        [HttpPost]
        public async Task<IActionResult>AcceptFollow(AcceptFollowRequestCommand cmd)
        {
            var result = await _Mediator.Send(cmd);
            return NewResult(result);
        }

        [HttpPost]
        public async Task<IActionResult>RejectFollow(RejectFollowRequestCommand cmd)
        {
            var result = await _Mediator.Send(cmd);
            return NewResult(result);
        }


        [HttpDelete]
        public async Task<IActionResult>UnFollow(UnFollowCommand cmd)
        {
            var result = await _Mediator.Send(cmd);
            return NewResult(result);
        }

        [HttpDelete]
        public async  Task<IActionResult>RemoveFollower(RemoveFollowerCommand cmd)
        {
            var result = await _Mediator.Send(cmd);
            return NewResult(result); 
        }


    }
}
