using AutoMapper;
using Core.Bases;
using Core.Feature.Notifications.Query.Models;
using Core.Feature.Notifications.Query.Results;
using Core.Wrappers;
using Data.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.RealTimeServices.NotificationsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Notifications.Query.Handler
{
    public class AddNotificationQueryHandler : ResponseHanlder,
        IRequestHandler<GetAllNotificationToUserQuery, Response<Pagination<GetNotificationQueryResult>>>,
        IRequestHandler<GetUnReadCountNotificationQuery, Response<int>>
    {
        #region Feild
        private readonly INotificationServices _notificationServices;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _http;
        private readonly UserManager<User> _userManager;
        #endregion
        #region Ctor
        public AddNotificationQueryHandler(INotificationServices notificationServices, IMapper mapper, IHttpContextAccessor http, UserManager<User> userManager)
        {
            _notificationServices = notificationServices;
            _mapper = mapper;
            _http = http;
            _userManager = userManager;
        }
        #endregion
        public async Task<Response<Pagination<GetNotificationQueryResult>>> Handle(GetAllNotificationToUserQuery request, CancellationToken cancellationToken)
        {
            var userid = GetUserId();
            var query = _notificationServices.GetUserNotifications(userid);
            var data = await _mapper.ProjectTo<GetNotificationQueryResult>(query)
                .ToPaginationListAsync(request.Page, request.PageSize);

            return Success(data);
        }

        public async Task<Response<int>> Handle(GetUnReadCountNotificationQuery request, CancellationToken cancellationToken)
        {
            var userid = GetUserId();
            var result = await _notificationServices.UnReadCount(userid);
            return Success(result);
        }

        private int GetUserId()
        {
            ClaimsPrincipal claim = _http.HttpContext?.User;
            var userId = int.Parse(_userManager.GetUserId(claim));
            return userId;
        }
    }
}
