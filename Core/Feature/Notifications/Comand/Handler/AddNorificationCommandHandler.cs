using Core.Bases;
using Core.Feature.Notifications.Comand.Models;
using Data.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Services.RealTimeServices.NotificationsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Notifications.Comand.Handler
{
    public class AddNorificationCommandHandler : ResponseHanlder,
        IRequestHandler<MarkAsReadNotificationCommand, Response<bool>>,
        IRequestHandler<MarkAsReadAllNotificationCommand, Response<bool>>,
        IRequestHandler<DeleteNotificationCommand, Response<string>>
    {
        #region Feild
        private readonly INotificationServices _notificationServices;
        private readonly IHttpContextAccessor _http;
        private readonly UserManager<User> _userManager;

        #endregion
        #region Ctor 

        public AddNorificationCommandHandler(INotificationServices notificationServices, IHttpContextAccessor http, UserManager<User> userManager)
        {
            _notificationServices = notificationServices;
            _http = http;
            _userManager = userManager;
        }
        #endregion

        public async Task<Response<bool>> Handle(MarkAsReadNotificationCommand request, CancellationToken cancellationToken)
        {
            var userid = GetCurrentUserId();
            var result = await _notificationServices.MarkAsRead(request.NotificationId,userid);
            return Success(result);
        }

        public async Task<Response<bool>> Handle(MarkAsReadAllNotificationCommand request, CancellationToken cancellationToken)
        {
            var userid = GetCurrentUserId();
            var result = await _notificationServices.MarkAllRead(userid);
            return Success(result);
        }

        public async Task<Response<string>> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            var userid = GetCurrentUserId();
            var notification = await _notificationServices.GetNotificationById(request.NotificationId);
            if (notification == null || notification.RecipientId != userid)
                return NotFound<string>("Notification Not Found ");
            var result = await _notificationServices.DeleteNotification(notification);
            if (!result)
                return BadRequest<string>("Failed To Delete Notification");
            return Success("Delete Notification Successfully");
        }

        private int GetCurrentUserId()
        {
            ClaimsPrincipal claim = _http.HttpContext?.User;
            var userid = int.Parse(_userManager.GetUserId(claim));
            return userid;

        }
    }
}
