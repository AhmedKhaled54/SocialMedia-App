using AutoMapper;
using Core.Bases;
using Core.Feature.Posts.Query.Models;
using Core.Feature.Posts.Query.Result;
using Core.Wrappers;
using Data.Identity;
using Infrastructure.Specification.PostSpecifications;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Services.Services.CachServices;
using Services.Services.PostsServices;
using Services.Services.RactsServices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Posts.Query.Hanlder
{
    public class AddPostQueryHandler : ResponseHanlder,
        IRequestHandler<GetPostsFeedQuery, Response<Pagination<GetpostsFeedQueryResult>>>,
        IRequestHandler<GetPostByIdQuery, Response<GetpostsFeedQueryResult>>,
        IRequestHandler<GetDetailsReactPostQuery, Response<GetPostReactDetailQueryResult>>
    {

        #region Feild 
        private readonly IPostServices _postServices;
        private readonly IHttpContextAccessor _http;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly ICachServices _cachservices;
        private readonly IReactServices _reactServices;
        #endregion
        #region Ctor 

        public AddPostQueryHandler(IPostServices postServices, IHttpContextAccessor http, UserManager<User> userManager, IMapper mapper, IConfiguration config, ICachServices cachservices, IReactServices reactServices)
        {
            _postServices = postServices;
            _http = http;
            _userManager = userManager;
            _mapper = mapper;
            _config = config;
            _cachservices = cachservices;
            _reactServices = reactServices;
        }
        #endregion
        public async Task<Response<Pagination<GetpostsFeedQueryResult>>> Handle(GetPostsFeedQuery request, CancellationToken cancellationToken)
        {
            var userid = GetCurrentUser();
            var version = await _cachservices.GetResponse($"feed:version:{userid}");
            if (string.IsNullOrEmpty(version))
                version = "1";
            var CachKey = $"feed:Post:{userid}:{version}:{request.PageNumber}:{request.Pagesize}";

            var CachData = await _cachservices.GetResponseGeneric<Pagination<GetpostsFeedQueryResult>>(CachKey);
            if (CachData != null)
                return Success(CachData);

            var spec = new PostSpecification
            {
                userid = userid
            };
            var post = _postServices.getPostSepecification(spec);
            var paginaiton = await _mapper.ProjectTo<GetpostsFeedQueryResult>(post)
                .ToPaginationListAsync(request.PageNumber, request.Pagesize);
            foreach (var item in paginaiton.Data)
            {
                if (!string.IsNullOrEmpty(item.UserImage))
                    item.UserImage = _config["BaseUrl"] + item.UserImage;
                if (item.MediaUrls.Any() && item.MediaUrls != null)
                {
                    foreach (var url in item.MediaUrls)
                    {
                        if (!string.IsNullOrEmpty(url.Url))
                            url.Url = _config["BaseUrl"] + url.Url;
                    }

                }

            }
            await _cachservices.SetResponse(CachKey, paginaiton, TimeSpan.FromMinutes(5));

            return Success(paginaiton);
        }

        public async Task<Response<GetpostsFeedQueryResult>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var userid = GetCurrentUser();
            var post = await _postServices.GetPostById(request.PostId);
            if (post == null)
                return NotFound<GetpostsFeedQueryResult>("Not Found Post");
            if (post.UserId != userid)
                return UnAuthorize<GetpostsFeedQueryResult>("InCorrect Please Try Agian !");

            var result = _mapper.Map<GetpostsFeedQueryResult>(post);
            if (!string.IsNullOrEmpty(result.UserImage))
                result.UserImage = _config["BaseUrl"] + result.UserImage;
            
            if (result.MediaUrls != null && result.MediaUrls.Any())
            {
                foreach (var url in result.MediaUrls)
                    url.Url = _config["BaseUrl"] + url.Url;
            }

            return Success(result);
        }

        public async Task<Response<GetPostReactDetailQueryResult>> Handle(GetDetailsReactPostQuery request, CancellationToken cancellationToken)
        {
            var version = await _cachservices.GetResponse($"post:version:{request.PostId}");
            if (string.IsNullOrEmpty(version))
                version = "1";
            var Key = $"post:reaction:{version}:{request.PostId}";
            var CachData = await _cachservices.GetResponseGeneric<GetPostReactDetailQueryResult>(Key);
            if (CachData!=null)
                return Success(CachData);

            var posts = await _reactServices.GetPostsDetails(request.PostId);
            if (posts == null)
                return NotFound<GetPostReactDetailQueryResult>("Post Not Found");

            var result =_mapper.Map<GetPostReactDetailQueryResult>(posts);
            await _cachservices.SetResponse(Key, result, TimeSpan.FromMinutes(5));
            return Success(result);
        }

        private int GetCurrentUser()
        {
            ClaimsPrincipal claim = _http.HttpContext?.User;
            var userid = int.Parse(_userManager.GetUserId(claim));
            return userid;
        }
    }
}
