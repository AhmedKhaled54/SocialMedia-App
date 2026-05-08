using AutoMapper.Execution;
using Core.Bases;
using Core.Feature.Comments.Command.Models;
using Data.Entity.Comments;
using Data.Enums.Notifacation;
using Data.Identity;
using Infrastructure.Hup;
using MediatR;
using Microsoft.AspNetCore.Components.Forms.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Query;
using Services.FilesServices;
using Services.RealTimeServices.NotificationsServices;
using Services.Services.CommentsServices;
using Services.Services.RactsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Comments.Command.Handler
{
    public class AddCommentCommandHandler : ResponseHanlder,
        IRequestHandler<AddCommentCommand, Response<string>>,
        IRequestHandler<ReactToCommentCommand, Response<string>>,
        IRequestHandler<EditCommentCommand, Response<string>>,
        IRequestHandler<DeleteCommentCommand, Response<string>>
    {
        #region Feild
        private readonly ICommentServices _commentServices;
        private readonly INotificationServices _notificationServices;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _http;
        private readonly IHubContext<ChatHup> _hub;
        private readonly IReactServices _reactServices;
        private readonly IFileServices _fileServices;
        #endregion
        #region Ctor 

        public AddCommentCommandHandler(ICommentServices commentServices, INotificationServices notificationServices, UserManager<User> userManager, IHttpContextAccessor http, IHubContext<ChatHup> hubContext, IReactServices reactServices, IFileServices fileServices)
        {
            _commentServices = commentServices;
            _notificationServices = notificationServices;
            _userManager = userManager;
            _http = http;
            _hub = hubContext;
            _reactServices = reactServices;
            _fileServices = fileServices;
        }
        #endregion
        public async Task<Response<string>> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            var userid = GetCurrentUserId();
            List<CommentMedia>? medias = null;
            if (request.media != null && request.media.Any())
                medias = await _commentServices.CommentmedaiProcess(request.media);
            var comment = await _commentServices.AddComment(userid, request.PostId, request.content, medias);

            await _notificationServices.CreateNotification(userid, comment.UserId, NotifacationType.Comment);
            await _hub.Clients.User(comment.UserId.ToString()).SendAsync("ReceiveNotification",
                new
                {
                    FromUserId = userid,
                    Message = $"{comment.User.UserName} Comment To Your Post",
                    Type = NotifacationType.Comment,
                    CommentId = comment.Id
                });

            return Success("Added Comment ");
        }

        public async Task<Response<string>> Handle(ReactToCommentCommand request, CancellationToken cancellationToken)
        {

            var userid = GetCurrentUserId();
            if (userid == 0)
                return UnAuthorize<string>("Incorrect Please Try Again !");
            var result = await _reactServices.CommentReactAsync(request.CommentId, userid, request.React);
            if (!result.notify || result.ownerid == userid)
                return Success("Done");

            await _notificationServices.CreateNotification(userid, result.ownerid, NotifacationType.Like);
            await _hub.Clients.User(result.ownerid.ToString()).SendAsync("ReceiveNotication", new
            {
                FromUserId = userid,
                Message = $"{request.React} To Your Comment ",
                Type = NotifacationType.Like
            });
            return Success("Reaction Added Successfuly ");

        }

        public async Task<Response<string>> Handle(EditCommentCommand request, CancellationToken cancellationToken)
        {
            var userid = GetCurrentUserId();
            List<CommentMedia>? CommentMedias = null;

            var comment = await _commentServices.GetCommentById(request.CommentId);
            if (comment == null)
                return NotFound<string>("Comment Not Found !");
            if (comment.UserId != userid)
                return BadRequest<string>("You Are Not Allowed To Edit This Comment !");
            if (request.MediaRemoved != null && request.MediaRemoved.Any())
            {
                var media = comment.Media.Where(c => request.MediaRemoved.Contains(c.Id)).ToList();
                await _commentServices.DeleteCommentMedia(media);
                foreach (var item in media)
                {
                    await _fileServices.RemoveImage(item.Url);
                }

                comment.Media = comment.Media.Where(c => !request.MediaRemoved.Contains(c.Id)).ToList();// not deletd 
            }

            if (request.Files != null && request.Files.Any())
            {
                CommentMedias = await _commentServices.CommentmedaiProcess(request.Files);
                foreach (var item in CommentMedias)
                {
                    comment.Media.Add(item);
                }
            }

            comment.Content = request.Content ?? comment.Content;
            await _commentServices.UpdateComment(comment);
            return Success("Comment Updated Successfuly ");

        }

        public async Task<Response<string>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            //delete comment and commment media and comment reacts
            var userid = GetCurrentUserId();
            var comment =await _commentServices.GetCommentById(request.CommentId);
            if (comment == null)
                return NotFound<string>("Comment Not Found !");
            if (comment.UserId != userid)
                return UnAuthorize<string>("You Are Not Allowed To Delete This Comment !");
            if (comment.Media!=null)
            {
                foreach (var item in comment.Media)
                {
                    await _fileServices.RemoveImage(item.Url);
                }
            }
            await _commentServices.DeleteComment(comment);
            return Success("Comment Deleted Successfully !");
        }

        private int GetCurrentUserId()
        {
            ClaimsPrincipal claim = _http.HttpContext?.User;
            var userid = int.Parse(_userManager.GetUserId(claim));
            return userid;
        }
    }
}
