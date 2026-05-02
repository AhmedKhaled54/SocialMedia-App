using AutoMapper;
using Core.Bases;
using Core.Feature.Follow.Query.Models;
using Core.Feature.Follow.Query.Results;
using Core.Wrappers;
using Data.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using Services.Services.FollowService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Follow.Query.Handler
{
    public class AddFollowQueryHandler : ResponseHanlder,
        IRequestHandler<GetAllFollowerQuery, Response<Pagination<GetFollowersQueryResult>>>,
        IRequestHandler<GetAllFollowingQuery, Response<Pagination<GetFollowersQueryResult>>>
    {

        #region Feild
        private readonly IMapper _mapper;
        private readonly UserManager<User> _usermanager;
        private readonly IHttpContextAccessor _http;
        private readonly IFollowServices _followServices;
        private readonly IConfiguration _confiq;
        #endregion

        #region Ctor 
        public AddFollowQueryHandler(IMapper mapper, UserManager<User> usermanager, IHttpContextAccessor http, IFollowServices followServices, IConfiguration confiq)
        {
            _mapper = mapper;
            _usermanager = usermanager;
            _http = http;
            _followServices = followServices;
            _confiq = confiq;
        }
        #endregion
        public async Task<Response<Pagination<GetFollowersQueryResult>>> Handle(GetAllFollowerQuery request, CancellationToken cancellationToken)
        {
            var user = GetCurrentUser();
            var follower = _followServices.GetAllFollowers(user, request.Search);
            var pagination = await _mapper.ProjectTo<GetFollowersQueryResult>(follower)
                .ToPaginationListAsync(request.PageNumber, request.PageSize);
            foreach ( var item in pagination.Data )
            {
                if (!string.IsNullOrEmpty(item.Image))
                    item.Image = _confiq["BaseUrl"]+item.Image;

            }

            return Success(pagination);

        }

        public async Task<Response<Pagination<GetFollowersQueryResult>>> Handle(GetAllFollowingQuery request, CancellationToken cancellationToken)
        {
            var user = GetCurrentUser();
            var following =_followServices.GetAllFollowing(user, request.Search);
            var pagination =await _mapper.ProjectTo<GetFollowersQueryResult>(following)
                .ToPaginationListAsync(request.PageNumber,request.PageSize);
            foreach( var item in pagination.Data)
            {
                if (!string.IsNullOrEmpty(item.Image))
                    item.Image = _confiq["BaseUrl"] + item.Image;
            }

            return Success(pagination);
        }

        private int GetCurrentUser()
        {
            ClaimsPrincipal claim = _http.HttpContext?.User;
            var userid = int.Parse(_usermanager.GetUserId(claim));
            return userid;
        }
    }
}
