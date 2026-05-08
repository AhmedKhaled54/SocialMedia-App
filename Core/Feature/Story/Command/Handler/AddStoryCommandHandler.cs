using Core.Bases;
using Core.Feature.Story.Command.Models;
using Data.Entity.Stories;
using Data.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Services.FilesServices;
using Services.Services.StoriesServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Story.Command.Handler
{
    public class AddStoryCommandHandler : ResponseHanlder,
        IRequestHandler<AddStoryCommand, Response<string>>,
        IRequestHandler<DeleteStoryCommand, Response<string>>
    {
        #region Feild
        private readonly IStoryServices _storyServices;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _http;
        private readonly IFileServices _fileServices;
        #endregion
        #region Ctor 
        public AddStoryCommandHandler
            (
            IStoryServices storyServices,
            UserManager<User> userManager,
            IHttpContextAccessor http
,
            IFileServices fileServices)
        {
            _storyServices = storyServices;
            _userManager = userManager;
            _http = http;
            _fileServices = fileServices;
        }
        #endregion
        public async Task<Response<string>> Handle(AddStoryCommand request, CancellationToken cancellationToken)
        {
            List<StoryMedia> storyMedias = null;
            var userId = GetUserId();
            if (userId == 0)
                return UnAuthorize<string>("Unauthorized: Please try again.");
            if (request.Files != null && request.Files.Any())
                storyMedias = await _storyServices.StorymedaiProcess(request.Files);
            await _storyServices.AddStory(userId, storyMedias);

            return Success("Story Added Successfully");

        }

        public async Task<Response<string>> Handle(DeleteStoryCommand request, CancellationToken cancellationToken)
        {
            var userid = GetUserId();
            if (userid == 0)
                return UnAuthorize<string>("Unauthorized: Please try again.");
            var story = await _storyServices.GetStoryById(request.StoryId);
            if (story == null)
                return NotFound<string>("Story Not Found");
            if (story.UserId != userid)
                return UnAuthorize<string>("You are not allowed to access this story.");
            if (story.Media != null && story.Media.Any())
            {
                foreach (var media in story.Media)
                {
                    await _fileServices.RemoveImage(media.Url);
                }
            }

            await _storyServices.DeleteStory(story);
            return Success("Story Deleted Successfully");

        }

        private int GetUserId()
        {
            ClaimsPrincipal claim = _http.HttpContext?.User;
            var user = int.Parse(_userManager.GetUserId(claim));
            return user;
        }
    }
}
