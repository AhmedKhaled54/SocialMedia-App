using AutoMapper;
using Core.Feature.Authantication.command.Models;
using Data.Helper;
using Data.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Services.Services.AuthanticationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Authantication.command.Handler
{


    public class AddAuthanticateHanlder : IRequestHandler<RegisterCommand,string>,
        IRequestHandler<LoginCommand,AuthResult>,
        IRequestHandler<RefreshTokenCommand,AuthResult>

    {
        #region Faild
        private readonly IMapper _mapper;
        private readonly IAuthanticationServices _authservices;
        private readonly UserManager<User> _userManager;
        #endregion

        #region ctor 

        public AddAuthanticateHanlder(IMapper mapper,IAuthanticationServices authservices,UserManager<User> userManager)
        {
            _mapper = mapper;
            _authservices = authservices;
            _userManager = userManager;
        }
        #endregion
        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            var auth = await _authservices.Register(user, request.Password);
            if (!auth.Success)
                return auth.Message;
            return auth.Message;
        }

        public async Task<AuthResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var email  =await _userManager.FindByEmailAsync(request.Email);
            if (email is null || !await _userManager.CheckPasswordAsync(email, request.Password))
                return new AuthResult { Message="Incorrect Email Or Password "} ;
            if (!await _userManager.IsEmailConfirmedAsync(email))
                return new AuthResult { Message = " Email Not Confieremd Please Try Again  " };

            var result = await _authservices.GetToken(email);
            return result;


        }

        public async Task<AuthResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var RefreshToken = await _authservices.RefreshToken(request.Token);
            return RefreshToken;
            
        }
    }
}
