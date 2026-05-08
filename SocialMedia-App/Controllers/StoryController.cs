using Core.Feature.Story.Command.Models;
using Core.Feature.Story.Query.Models;
using Core.Feature.Story.Query.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SocialMedia_App.Controllers
{

    public class StoryController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> AddStory([FromForm] AddStoryCommand cmd)
        {
            var result = await _Mediator.Send(cmd);
            return NewResult(result);

        }


        [HttpGet]
        public async Task<ActionResult<GetUserStoriesQueryResult>> GetUserStories([FromQuery] GetUserStoriesQuery query)
        {
            var result = await _Mediator.Send(query);
            return NewResult(result);
        }

        [HttpGet]
        public async Task<ActionResult<GetUserStoriesQueryResult>> GetFollowingStories([FromQuery] GetFollowingStoryQuery query)
        {
            var result = await _Mediator.Send(query);
            return NewResult(result);
        }

        [HttpGet]
        public async Task<ActionResult<GetUserStoriesQueryResult>> GetStoryById([FromQuery] GetStoryByIdQuery query)
        {
            var result = await _Mediator.Send(query);
            return NewResult(result);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteStory([FromQuery] DeleteStoryCommand cmd)
        {
            var result = await _Mediator.Send(cmd);
            return NewResult(result);
        }
    }
}
