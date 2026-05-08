using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Bases;
using Core.Feature.Story.Query.Models;
using Core.Feature.Story.Query.Results;
using Data.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Services.Services.StoriesServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Story.Query.Handler
{
    public class AddStoryQueryHandler : ResponseHanlder,
        IRequestHandler<GetUserStoriesQuery, Response<GetUserStoriesQueryResult>>,
        IRequestHandler<GetFollowingStoryQuery, Response<List<GetUserStoriesQueryResult>>>,
        IRequestHandler<GetStoryByIdQuery, Response<GetUserStoriesQueryResult>>
    {
        #region Feild
        private readonly IStoryServices _storyServices;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _http;
        private readonly UserManager<User> _userManager;
        #endregion
        #region Ctor
        public AddStoryQueryHandler(IStoryServices storyServices, IMapper mapper, IHttpContextAccessor http, UserManager<User> userManager)
        {
            _storyServices = storyServices;
            _mapper = mapper;
            _http = http;
            _userManager = userManager;
        }
        #endregion
        public async Task<Response<GetUserStoriesQueryResult>> Handle(GetUserStoriesQuery request, CancellationToken cancellationToken)
        {
            var userid = GetUserId();
            if (userid == 0)
                return UnAuthorize<GetUserStoriesQueryResult>("Unauthorized: Please try again.");

            var query = _storyServices.GetUserStories(request.userid);

            var result = await query
                .ProjectTo<GetUserStoriesQueryResult>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (result == null)
                return NotFound<GetUserStoriesQueryResult>("Not Found");

            return Success(result);
        }

        public async Task<Response<List<GetUserStoriesQueryResult>>> Handle(GetFollowingStoryQuery request, CancellationToken cancellationToken)
        {
            //var query = _storyServices.GetFollowingStory(request.userid);
            //var result = await query.ProjectTo<GetUserStoriesQueryResult>
            //    (_mapper.ConfigurationProvider).ToListAsync(); =>(* resolver not working ProjectTo)
            var userid = GetUserId();
            if (userid == 0)
                return UnAuthorize<List<GetUserStoriesQueryResult>>("Unauthorized: Please try again.");

            var query = _storyServices.GetFollowingStory(request.userid);
            var data = await query.ToListAsync();
            var result = _mapper.Map<List<GetUserStoriesQueryResult>>(data);
            return Success(result);
        }

        public async  Task<Response<GetUserStoriesQueryResult>> Handle(GetStoryByIdQuery request, CancellationToken cancellationToken)
        {
            var story =await  _storyServices.GetStoryById(request.StoryId);
            if (story == null)
                return NotFound<GetUserStoriesQueryResult>("Not Found Syory ");

            var result = _mapper.Map<GetUserStoriesQueryResult>(story);
            return Success(result);    
        }

        private int GetUserId()
        {
            ClaimsPrincipal claim = _http.HttpContext?.User;
            var userid = int.Parse(_userManager.GetUserId(claim));
            return userid;
        }
    }
}
