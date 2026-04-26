using AutoMapper;
using Core.Bases;
using Core.Feature.ApplicationUser.Command.Models;
using Core.Feature.Authorization.Query.Models;
using Core.Feature.Authorization.Results;
using Data.Identity;
using Infrastructure.SeedData;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Authorization.Query.Hanlder
{
    public class AddAuthorizeQueryHandler : ResponseHanlder,
        IRequestHandler<GetAllRolesQuery, Response<IEnumerable<RoleDeatailsResult>>>
    {

        #region Fields
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        #endregion
        #region Ctor
        public AddAuthorizeQueryHandler(RoleManager<Role> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        #endregion
        public async Task<Response<IEnumerable<RoleDeatailsResult>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleManager.Roles.ToListAsync();
            if (!roles.Any())
                return BadRequest<IEnumerable<RoleDeatailsResult>>("No Roles Available!");
            var result  =_mapper.Map<IEnumerable<RoleDeatailsResult>>(roles);
            return Success(result);

        }


    }
}
