using Core.Bases;
using Core.Feature.Follow.Command.Models;
using Core.Feature.Follow.Query.Models;
using Core.Feature.Follow.Query.Results;
using Core.Wrappers;
using Data.Enums.Follow;
using Data.Enums.Notifacation;
using Data.Identity;
using Infrastructure.Hup;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Query;
using Services.RealTimeServices.NotificationsServices;
using Services.Services.FollowService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Follow.Command.Handler
{
    public class AddFollowHandler : ResponseHanlder,
        IRequestHandler<FollowRequestCommand, Response<string>>,
        IRequestHandler<AcceptFollowRequestCommand, Response<string>>,
        IRequestHandler<RejectFollowRequestCommand, Response<string>>,
        IRequestHandler<UnFollowCommand, Response<string>>,
        IRequestHandler<RemoveFollowerCommand,Response<string>>
    {
        #region Feild
        private readonly IHttpContextAccessor _http;
        private readonly UserManager<User> _userManager;
        private readonly IFollowServices _followServices;
        private readonly INotificationServices _notificationServices;
        private readonly IHubContext<ChatHup> _hub;
        #endregion
        #region Ctor 
        public AddFollowHandler
            (
            IHttpContextAccessor http,
            UserManager<User> userManager,
            IFollowServices followServices,
            INotificationServices notificationServices,
            IHubContext<ChatHup> hub)
        {
            _http = http;
            _userManager = userManager;
            _followServices = followServices;
            _notificationServices = notificationServices;
            _hub = hub;
        }
        #endregion

        public async  Task<Response<string>> Handle(FollowRequestCommand request, CancellationToken cancellationToken)
        {
            var senderid = GetCurrentUser();
            if (senderid == request.ReceiveId)
                return BadRequest<string>("Can't Follow YourSelf😊 ");
            var AlreadyFollowing=await _followServices.ISFollowing(senderid,request.ReceiveId);
            if (AlreadyFollowing)
                return BadRequest<string>("Already Following");
            if (await _followServices.ISPending(senderid, request.ReceiveId))
                return BadRequest<string>("Request Already Send!");
            await _followServices.AddFollowRequest(senderid, request.ReceiveId);
            //add notification and notify 
            await _notificationServices.CreateNotification(senderid, request.ReceiveId, NotifacationType.Follow);
            await _hub.Clients.User(request.ReceiveId.ToString())
                .SendAsync("ReceivedNotification",new 
                {
                    Message="New Follow Request ",
                    FromUserId=senderid,
                    Type=NotifacationType.Follow
                });

            return Success("Follow Request Send");
        }

        public async  Task<Response<string>> Handle(AcceptFollowRequestCommand request, CancellationToken cancellationToken)
        {
            var user  = GetCurrentUser();// 24 =>25

            var FollowRequest =await _followServices.GetFollowRequest(request.RequestID, user);
            if (FollowRequest == null)
                return BadRequest<string>("Request Not Found !");
            FollowRequest.Status = FollowStatus.Accepted;
            await _followServices.AddNewFollow(FollowRequest.ReceiveId, FollowRequest.SenderId);
            //add Notification and Notify 
            await _notificationServices.CreateNotification(user, FollowRequest.ReceiveId, NotifacationType.Follow);
            await _hub.Clients.User("ReceiveNotification").SendAsync(request.RequestID.ToString(), new
            {
                FromUserId = user,
                Message = "Your Follow Request was Accepted",
                Type = NotifacationType.Follow
            });

            return Success("Follow Request Accepted ");
        }

        public async Task<Response<string>> Handle(RejectFollowRequestCommand request, CancellationToken cancellationToken)
        {
            var user = GetCurrentUser();
            var FollowRequest = await _followServices.GetFollowRequest( request.RequestId,user);
            if (FollowRequest == null)
                return BadRequest<string>("Request Not Found!");
            FollowRequest.Status= FollowStatus.Rejected;
            await _followServices.UpdateFollowRequest(FollowRequest);
            return Success("Follow Request Rejected ");

        }

        public async Task<Response<string>> Handle(UnFollowCommand request, CancellationToken cancellationToken)
        {
            var user= GetCurrentUser();

            var result = await _followServices.UnFollow(user, request.ReceiveId);
            if (!result)
                return BadRequest<string>("You are not Follwoing This User!");
            return Success("UnFollow Successfuly");

        }

        public async Task<Response<string>> Handle(RemoveFollowerCommand request, CancellationToken cancellationToken)
        {
            var currentuser = GetCurrentUser();
            var follow = await _followServices.RemoveFollower(currentuser, request.FollowerId);
            if (!follow)
                return BadRequest<string>("Follower Not Found ");
            return Success("Follower Removed ");

        }

       

        private int GetCurrentUser()
        {
            ClaimsPrincipal claim = _http.HttpContext?.User;
            var user = int.Parse(_userManager.GetUserId(claim));
            return user;
        }

    }
}
