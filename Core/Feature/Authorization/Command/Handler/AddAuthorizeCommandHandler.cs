using AutoMapper;
using Core.Bases;
using Core.Feature.Authorization.Command.Models;
using Core.Feature.Authorization.Query.Models;
using Core.Feature.Authorization.Results;
using Data.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Authorization.Command.Handler
{
    public class AddAuthorizeCommandHandler : ResponseHanlder,
        IRequestHandler<AddNewRoleCommand, Response<string>>,
        IRequestHandler<EditRoleCommand, Response<string>>,
        IRequestHandler<DeleteRoleCommand, Response<string>>,
        IRequestHandler<AssignUserToRoleCommand, Response<string>>
  
        
    {
        #region Fields  
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        #endregion
        #region Ctor

        public AddAuthorizeCommandHandler(RoleManager<Role> roleManager, IMapper mapper, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
        }
        #endregion
        public async Task<Response<string>> Handle(AddNewRoleCommand request, CancellationToken cancellationToken)
        {
            if (await _roleManager.RoleExistsAsync(request.RoleName))
                return BadRequest<string>("Role Aready Exsit");

            var role =new Role();
            role.Name = request.RoleName;
           
            var result= await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
                return BadRequest<string>(string.Join(",", result.Errors.Select(c => c.Description)));

            return Success($"Role : {request.RoleName} Is Created ");
            

            


        }

        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {

            var role = await _roleManager.Roles.FirstOrDefaultAsync(c => c.Id == request.RoleID);
            if (role == null)
                return NotFound<string>("Not Found Role!");
            var map = _mapper.Map(request,role);
            var isupdate =await _roleManager.UpdateAsync(role);

            return isupdate.Succeeded ? Success("Update Role Successfuly ")
                : BadRequest<string>("Please Try Again!");

        }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(c => c.Id == request.RoleId);
            if (role == null)
                return NotFound<string>("Role Not Found !");
            var isdeleted=await _roleManager.DeleteAsync(role);
            return !isdeleted.Succeeded ?
                BadRequest<string>(string.Join(",", isdeleted.Errors.Select(c => c.Description)))
                : Success($"Role: {role.Name} Is Deleted ");
        }

        public async Task<Response<string>> Handle(AssignUserToRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(c => c.Id == request.UserId);
            if (user is null) return  NotFound<string>(" User Not Found! ");

               if( !await _roleManager.RoleExistsAsync(request.RoleName))
                return BadRequest<string>("Invalid Role Please Try Again !");
            if (await _userManager.IsInRoleAsync(user, request.RoleName))
                return BadRequest<string>($"User {user.UserName} Already a {request.RoleName}. ");
            await _userManager.AddToRoleAsync(user, request.RoleName);
            return Success($"Successfuly Added {user.UserName} In This  {request.RoleName}");

        }
    }
}
