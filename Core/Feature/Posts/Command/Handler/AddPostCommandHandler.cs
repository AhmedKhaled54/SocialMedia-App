using AutoMapper;
using Core.Bases;
using Core.Feature.Posts.Command.Models;
using Data.Entity.Posts;
using Data.Enums.Notifacation;
using Data.Identity;
using Infrastructure.Hup;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Services.FilesServices;
using Services.RealTimeServices.NotificationsServices;
using Services.Services.CachServices;
using Services.Services.PostsServices;
using Services.Services.RactsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Posts.Command.Handler
{
    public class AddPostCommandHandler : ResponseHanlder,
        IRequestHandler<AddPostCommand, Response<string>>,
        IRequestHandler<EditPostCommand, Response<string>>,
        IRequestHandler<DeletePostCommand, Response<bool>>,
        IRequestHandler<ReactToPostCommand, Response<string>>

    {
        #region Feild
        private readonly IPostServices _postServices;
        private readonly IHttpContextAccessor _http;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IFileServices _fileServices;
        private readonly ICachServices _cachServices;
        private readonly IReactServices _reactServices;
        private readonly INotificationServices _notificationServices;
        private readonly IHubContext<ChatHup> _hub;
        #endregion
        #region Ctor 
        public AddPostCommandHandler(IPostServices postServices, IHttpContextAccessor http, UserManager<User> userManager, IMapper mapper, IFileServices fileServices, ICachServices cachServices, IReactServices reactServices, INotificationServices notificationServices, IHubContext<ChatHup> hub)
        {
            _postServices = postServices;
            _http = http;
            _userManager = userManager;
            _mapper = mapper;
            _fileServices = fileServices;
            _cachServices = cachServices;
            _reactServices = reactServices;
            _notificationServices = notificationServices;
            _hub = hub;
        }
        #endregion

        public async Task<Response<string>> Handle(AddPostCommand request, CancellationToken cancellationToken)
        {
            var user = GetCurrentUser();

            List<PostMedia>? media = null;
            if (request.Files != null && request.Files.Any())
                media = await _postServices.PostMediaProcess(request.Files);

            var post = _postServices.CreatePost(user, request.Caption, media);
            return Success("Created !");
        }

        public async Task<Response<string>> Handle(EditPostCommand request, CancellationToken cancellationToken)
        {
            var userid = GetCurrentUser();
            List<PostMedia>? PostMedia = null;

            var post = await _postServices.GetPostById(request.PostId);
            if (post == null)
                return NotFound<string>("Post Not Found ");

            if (post.UserId != userid)
                return UnAuthorize<string>(" not allowed to edit this post !");

            if (request.MediaRemoved != null && request.MediaRemoved.Any())
            {
                var media = post.Media.Where(c => request.MediaRemoved.Contains(c.Id)).ToList();
                await _postServices.DeletePostMedia(media);

                foreach (var item in media)
                {
                    await _fileServices.RemoveImage(item.Url);
                }
                post.Media=post.Media.Where(c=>!request.MediaRemoved.Contains(c.Id)).ToList();//
            }

            if (request.Files != null && request.Files.Any())
            {
                PostMedia = await _postServices.PostMediaProcess(request.Files);
                foreach (var item in PostMedia)
                {
                    post.Media.Add(item);
                }
            }

            post.Caption = request.Caption ?? post.Caption;

            await _postServices.UpdatePost(post);

            await _cachServices.IncrementKey($"feed:version:{userid}");
            return Success("Updated Post successfuly ");

        }

        public async  Task<Response<bool>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var userid = GetCurrentUser();
            var post =await _postServices.GetPostById(request.PostId);
            if (post == null)
                return NotFound<bool>("Post Not Found !");
            if (userid != post.UserId)
                return UnAuthorize<bool>("Not Allawed Deleted This Post !");
            //delete services in the post 
            if (post.Media != null)
            {
                foreach (var item in post.Media)
                {
                    await _fileServices.RemoveImage(item.Url);
                }
            }

            await _postServices.DeletePost(post);
            await _cachServices.IncrementKey($"feed:varsion:{userid}");


            return Success(true);
        }

        public async  Task<Response<string>> Handle(ReactToPostCommand request, CancellationToken cancellationToken)
        {
            var userid = GetCurrentUser();
            if (userid <= 0)
                return UnAuthorize<string>("Incorrect Please Try Again !");
            var response = await _reactServices.PostReactionAsync(request.PostId, userid, request.ReactType);
            if (!response.Notify || response.PostOwnerId == userid)
                return Success<string>("Done");

            await _notificationServices.CreateNotification(userid, response.PostOwnerId, NotifacationType.Like);
            await _hub.Clients.User(response.PostOwnerId.ToString()).SendAsync("ReceiveNotification", new
            {
                Message = $"{request.ReactType} On Your Post",
                FromUserId = userid,
                Type = NotifacationType.Like,
                PostId = request.PostId,
            });
            await _cachServices.IncrementKey($"post:version:{request.PostId}");
            return Success("Reaction Added Successfully");

        }

        private int GetCurrentUser()
        {
            ClaimsPrincipal claim = _http.HttpContext?.User;
            var userid = int.Parse(_userManager.GetUserId(claim));
            return userid;
        }

    }
}
