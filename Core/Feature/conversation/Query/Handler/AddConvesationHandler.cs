using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Bases;
using Core.Feature.conversation.Query.Models;
using Core.Feature.conversation.Query.Results;
using Core.Wrappers;
using Data.Entity;
using Data.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MimeKit.Tnef;
using Services.RealTimeServices.MessageServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.conversation.Query.Handler
{
    public class AddConvesationHandler : ResponseHanlder,
        IRequestHandler<GetConversationQuery, Response<Pagination<GetConvesationQueryResult>>>,
        IRequestHandler<GetUnreadMessageQuery,Response<int>>,
        IRequestHandler<GetAllChatsQuery,Response<List<GetAllCahtsQueryResult>>>
    {
        private readonly IPrivateMessageServices _messageServices;
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public AddConvesationHandler
            (
            IPrivateMessageServices messageServices,
            IMapper mapper
,
            IHttpContextAccessor httpContext,
            UserManager<User> userManager)
        {
            _messageServices = messageServices;
            _mapper = mapper;
            _httpContext = httpContext;
            _userManager = userManager;
        }
        public async Task<Response<Pagination<GetConvesationQueryResult>>> Handle(GetConversationQuery request, CancellationToken cancellationToken)
        {
            //Expression<Func<PrivateMessage,GetConvesationQueryResult>>expression=
            //    c=> new GetConvesationQueryResult(c.Id,c.SendId)
            var CuurentUser = getcuurentUserId();

            var query = _messageServices.GetConversation(CuurentUser, request.ReceiveId, request.Search);
            var paginationList =await  _mapper.ProjectTo<GetConvesationQueryResult>(query)
                .ToPaginationListAsync(request.PageNumber,request.PageSize);
            return Success(paginationList);
        }

        public async Task<Response<int>> Handle(GetUnreadMessageQuery request, CancellationToken cancellationToken)
        {

            var CurrentUser = getcuurentUserId();
            var result = await _messageServices.GetCountUnReadMessagePerConvarsation(request.SenderId, CurrentUser);
            return Success(result);
        }

        public async Task<Response<List<GetAllCahtsQueryResult>>> Handle(GetAllChatsQuery request, CancellationToken cancellationToken)
        {
            var CurrentUser = getcuurentUserId();
            var resutl = _messageServices.GetAllChats(CurrentUser, request.Search);
            var map = _mapper.Map<List<GetAllCahtsQueryResult>>(resutl.ToList());
         
            return Success(map);

        }

        private int getcuurentUserId()
        {
            ClaimsPrincipal claim = _httpContext.HttpContext?.User;
            var user = int.Parse(_userManager.GetUserId(claim));
            return user;
        }
    }
}
