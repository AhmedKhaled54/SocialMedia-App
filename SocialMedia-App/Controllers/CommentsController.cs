using AutoMapper.Configuration.Annotations;
using Core.Feature.Comments.Command.Models;
using Core.Feature.Comments.Query.Models;
using Core.Feature.Comments.Query.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SocialMedia_App.Controllers
{

    public class CommentsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> AddComment([FromForm] AddCommentCommand cmd)
        {
            var result = await _Mediator.Send(cmd);
            return NewResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> ReactToComment([FromQuery] ReactToCommentCommand cmd)
        {
            var result = await _Mediator.Send(cmd);
            return NewResult(result);

        }


        [HttpGet]
        public async Task<ActionResult<GetReactCommentDetailsQueryResult>> GetCommentReactsDetails(int CommmentId)
        {
            var query = new GetReactCommentDetailsQuery(CommmentId);
            var result = await _Mediator.Send(query);
            return NewResult(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<GetCommentsQueryResult>>> GetCommentsDtails([FromQuery] GetCommentsQuery query)
        {
            var result = await _Mediator.Send(query);
            return NewResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> EditComment([FromForm] EditCommentCommand cmd)
        {
            var result = await _Mediator.Send(cmd);
            return NewResult(result);

        }

        [HttpDelete]
        public async Task<IActionResult>DeleteComment([FromQuery] DeleteCommentCommand cmd)
        {
            var result = await _Mediator.Send(cmd);
            return NewResult(result);
        }
    }
}
