using AutoMapper;
using Core.Bases;
using Core.Feature.ApplicationUser.Query.Model;
using Core.Feature.ApplicationUser.Query.Result;
using Data.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.ApplicationUser.Query.Handler
{
    public class ApplicationUserQueryHandler : ResponseHanlder,
        IRequestHandler<GetUserByIdQuery, Response<UserDetailsResult>>,
        IRequestHandler<GetAllUserQuery, Response<List<UserDetailsResult>>>
    {
        #region  Fields
        private readonly UserManager<User>_userManager;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor
        public ApplicationUserQueryHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        #endregion
        public async Task<Response<UserDetailsResult>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserID.ToString());
            if (user is null) return NotFound<UserDetailsResult>("User Not Found!");
            var result =_mapper.Map<UserDetailsResult>(user);
            return Success(result);
        }

        public async Task<Response<List<UserDetailsResult>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {

            var users = await _userManager.Users.ToListAsync();
            var result=_mapper.Map<List<UserDetailsResult>>(users);
            return Success(result);

        }
    }
}
