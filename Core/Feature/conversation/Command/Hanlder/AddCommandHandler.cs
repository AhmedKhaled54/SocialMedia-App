using AutoMapper;
using Core.Bases;
using Core.Feature.conversation.Command.Models;
using Core.Feature.conversation.Command.Results;
using Data.Enums.Notifacation;
using Data.Identity;
using Infrastructure.Hup;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Query;
using Services.RealTimeServices.MessageServices;
using Services.RealTimeServices.NotificationsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.conversation.Command.Hanlder
{
    public class AddCommandHandler : ResponseHanlder,
        IRequestHandler<DirectMessageCommand, Response<DirectMessageCommandResult>>,
        IRequestHandler<MarkReadMessageCommand, Response<bool>>,
        IRequestHandler<EditMessageCommand,Response<DirectMessageCommandResult>>,
        IRequestHandler<DeleteMessageCommand,Response<bool>>
    {

        #region Fields
        private readonly IPrivateMessageServices _messageServices;
        private readonly INotificationServices _notificationServices;
        private readonly IHubContext<ChatHup> _hub;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _http;
        #endregion
        #region Ctor
        public AddCommandHandler
            (
            IPrivateMessageServices messageServices,
            INotificationServices notificationServices,
            IHubContext<ChatHup> hub,
            IMapper mapper,
            UserManager<User> userManager,
            IHttpContextAccessor http)
        {
            _messageServices = messageServices;
            _notificationServices = notificationServices;
            _hub = hub;
            _mapper = mapper;
            _userManager = userManager;
            _http = http;
        }
        #endregion
        public async Task<Response<DirectMessageCommandResult>> Handle(DirectMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _messageServices.SendMessage(request.SnderId, request.RecivedId, request.Message);
            if (message == null)
                return BadRequest<DirectMessageCommandResult>("Incorrect Please Try Again !");
            //get convesationID : ==> TO Do 
            var ConversationId = _messageServices.GetConversation(request.SnderId, request.RecivedId);
            //send realtime chat 
            await _hub.Clients.Groups(ConversationId.ToString())//to do create connectionservicesManager 
                .SendAsync("ReceiveMessage", message);
            //add notification and notify
            await _notificationServices
                .CreateNotification(request.SnderId, request.RecivedId, NotifacationType.Message);
            await _hub.Clients.User(request.RecivedId.ToString()).
                SendAsync("ReceiveNotification",
                new
                {
                    Message = "New Message",
                    FromUserId = request.SnderId,
                    Type=NotifacationType.Message
                });

            var result = _mapper.Map<DirectMessageCommandResult>(message);
            return Success (result);

        }

        public async Task<Response<bool>> Handle(MarkReadMessageCommand request, CancellationToken cancellationToken)
        {
            var CurrentUser = GetCurrentUser();
            var result = await _messageServices.MarkMessageAsRead(request.SenderId,CurrentUser);
            await _hub.Clients.User(request.SenderId.ToString()).SendAsync("MessageRead", CurrentUser);
            return Success (result);
        }

        public async Task<Response<DirectMessageCommandResult>> Handle(EditMessageCommand request, CancellationToken cancellationToken)
        {
            var CurrentUser = GetCurrentUser();
            var message = await _messageServices.GetMessageById(request.MessageId);
            if (message.SendId != CurrentUser)
                return UnAuthorize<DirectMessageCommandResult>("Not Allawed Delete The Message ");
            
            var mapmessage = _mapper.Map(request,message);
            await _messageServices.UpdateMessage(mapmessage);
            var result =_mapper.Map<DirectMessageCommandResult>(message);
            
            
            //updated realtiem 
            var receiveid =message.SendId==CurrentUser?message.RecivedId:message.SendId;
            var ConversationId = _messageServices.GetConversationId(CurrentUser, receiveid);
            await _hub.Clients.Group(ConversationId.ToString()).SendAsync("MessageUpdated", new
            {
                MessageId = message.Id,
                NewBody = message.Body,
            });
            return Success (result);

        }

        public async Task<Response<bool>> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _messageServices.GetMessageById(request.MessageId);
            if (message == null)
                return NotFound<bool>("Not Found Message !");
           var CurrentUser= GetCurrentUser();
            //snder only deleted messsage 
            if (message.SendId != CurrentUser)
                return BadRequest<bool>("Not Allawed Removed The Message ");
            var receivedUser = message.SendId == CurrentUser ? message.RecivedId : CurrentUser;
            var ConversationId = _messageServices.GetConversationId(CurrentUser, receivedUser);
            await _messageServices.RemoveMessage(message);
            await _hub.Clients.Group(ConversationId).SendAsync("MessageDeleted", new
            {
                MessageId = request.MessageId
            });
            return Success(true);

        }

        private int GetCurrentUser()
        {
            ClaimsPrincipal claim =_http.HttpContext?.User;
            var user =int.Parse(_userManager.GetUserId(claim));
            return user;
        }
    }
}
