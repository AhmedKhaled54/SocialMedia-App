using Core.Bases;
using Core.Feature.ApplicationUser.Command.Models;
using Data.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Services.FilesServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.ApplicationUser.Handler
{
    public class ApplicationUserCommandHandler : ResponseHanlder,
        IRequestHandler<RemoveUserCommand, Response<string>>
    {

        #region Fields
        private readonly UserManager<User> _usermanager;
        private readonly IFileServices _fileServices;
        #endregion
        #region Ctor
        public ApplicationUserCommandHandler(UserManager<User> usermanager, IFileServices fileServices)
        {
            _usermanager = usermanager;
            _fileServices = fileServices;
        }
        #endregion

        public async Task<Response<string>> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {

            var user = await _usermanager.FindByIdAsync(request.UserId.ToString());
            if(user is null )return NotFound<string>("User Not Found !");
           
            var result = await _usermanager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                var error = string.Join(",", result.Errors.Select(c => c.Description));
                return BadRequest<string>(error);
            }
            if (!string.IsNullOrEmpty(user.Image))
                await _fileServices.RemoveImage(user.Image);

            return Success("Delete User Succesfully");

        }
    }
}
