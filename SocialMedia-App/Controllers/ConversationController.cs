using Core.Feature.conversation.Command.Models;
using Core.Feature.conversation.Query.Models;
using Core.Feature.conversation.Query.Results;
using Core.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SocialMedia_App.Controllers
{
    [Authorize(Roles = "User")]


    public class ConversationController : BaseController
    {

        [HttpGet]
        public async Task<ActionResult<Pagination<GetConvesationQueryResult>>> GetAllConversationWithPagination([FromQuery] GetConversationQuery query)
        {
            var result = await _Mediator.Send(query);
            return NewResult(result);
        }
        
        [HttpGet]
        public async Task<IActionResult>GetAllChats ([FromQuery] GetAllChatsQuery query)
        {
            var resutl =await _Mediator.Send(query);
            return NewResult(resutl);
        }

        [HttpPost]
        public async Task<IActionResult>SendMessage (DirectMessageCommand cmd)
        {
            var result =await _Mediator.Send(cmd);
            return NewResult(result);
        }

        [HttpPost]
        public async Task<IActionResult>MarkReadMessage(MarkReadMessageCommand cmd)
        {
            var result = await _Mediator.Send(cmd);
            return NewResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUnReadMessage(int SenderId )
        {
            var query = new GetUnreadMessageQuery(SenderId);
            var result = await _Mediator.Send(query);
            return NewResult(result);
        }


        [HttpPut]
        public async Task<IActionResult>UpdateMessage( EditMessageCommand cmd)
        {
            var resutl = await _Mediator.Send(cmd); 
            return NewResult(resutl);
        }


        [HttpDelete]
        public async Task<IActionResult>DeleteMessage(DeleteMessageCommand cmd)
        {

            var result = await _Mediator.Send(cmd);
            return NewResult(result);
        }
        
            
        }
    }

