using AutoMapper.Configuration.Annotations;
using Core.Feature.Posts.Command.Models;
using Core.Feature.Posts.Query.Models;
using Core.Feature.Posts.Query.Result;
using Core.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;

namespace SocialMedia_App.Controllers
{
    [Authorize(Roles ="User")]
    public class PostsController : BaseController
    {

        [HttpGet]
        public async Task<ActionResult<Pagination<GetpostsFeedQueryResult>>>GetPosts([FromQuery]GetPostsFeedQuery query)
        {
            var result = await _Mediator.Send(query);
            return NewResult(result);
        }

        [HttpGet]
        public async Task<ActionResult<GetpostsFeedQueryResult>> GetPostById([FromQuery] int PostId  )
        {
            var query = new GetPostByIdQuery(PostId);
            var result =await _Mediator.Send(query);
            return NewResult(result);

        }


        [HttpGet]
        public async Task<ActionResult<GetPostReactDetailQueryResult>> GetReactsPostDetails(int PostId)
        {
            var query =new GetDetailsReactPostQuery(PostId);
            var result =await _Mediator.Send(query);
            return NewResult(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewPost([FromForm] AddPostCommand cmd)
        {
            var result =await _Mediator.Send(cmd);
            return NewResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> ReactToPost([FromQuery] ReactToPostCommand cmd)
        {
            var result = await _Mediator.Send(cmd);
            return NewResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> EditPost([FromForm] EditPostCommand cmd)
        {
            var result =await _Mediator.Send(cmd);
            return NewResult(result);
        }


        [HttpDelete]
        public async Task<IActionResult>DeletePost(int PostId)
        {
            var cmd = new DeletePostCommand(PostId);
            var result = await _Mediator.Send(cmd);
            return NewResult(result);
        }
    }
}
