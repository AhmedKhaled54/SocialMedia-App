using Core.Feature.Notifications.Comand.Models;
using Core.Feature.Notifications.Query.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Tnef;

namespace SocialMedia_App.Controllers
{
    [Authorize(Roles = "User")]

    public class NotificationsController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllNotificationToUser([FromQuery] GetAllNotificationToUserQuery query)
        {
            var result = await _Mediator.Send(query);
            return NewResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUnReadNotificationToUser()
        {
            var query = new GetUnReadCountNotificationQuery();
            var result = await _Mediator.Send(query);
            return NewResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsRead([FromBody] MarkAsReadNotificationCommand cmd)
        {
            var result = await _Mediator.Send(cmd);
            return NewResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsReadAll()
        {
            var cmd = new MarkAsReadAllNotificationCommand();
            var result = await _Mediator.Send(cmd);
            return NewResult(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteNotification([FromQuery] int NotificationId)
        {
            var cmd = new DeleteNotificationCommand(NotificationId);
            var result = await _Mediator.Send(cmd);
            return NewResult(result);

        }
    }
}