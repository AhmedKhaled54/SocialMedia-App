using AutoMapper;
using Core.Bases;
using Core.Feature.Authantication.command.Models;
using Data.Helper;
using Data.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.FilesServices;
using Services.Services.AuthanticationServices;
using Services.Services.EmailServices;
using Services.Services.OtpService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Authantication.command.Handler
{


    public class AddAuthanticateCommandHanlder :ResponseHanlder,
        IRequestHandler<RegisterCommand,Response<string>>,
        IRequestHandler<LoginCommand,Response<AuthResult>>,
        IRequestHandler<RefreshTokenCommand,Response<AuthResult>>,
        IRequestHandler<RevokedTokenCommand,Response<string>>,
        IRequestHandler<EditProfileCommand,Response<string>>,
        IRequestHandler<ForgetPasswordCommand,Response<string>>,
        IRequestHandler<ResetPasswordCommand,Response<string>>

    {
        #region Faild
        private readonly IMapper _mapper;
        private readonly IAuthanticationServices _authservices;
        private readonly UserManager<User> _userManager;
        private readonly IFileServices _fileServices;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IOtpServices _OtpServices;
        private readonly IEmailServices _emailServices;
        #endregion

        #region ctor 

        public AddAuthanticateCommandHanlder(IMapper mapper,
            IAuthanticationServices authservices,
            UserManager<User> userManager,
            IFileServices fileServices
,
            IHttpContextAccessor httpContext,
            IOtpServices otpServices,
            IEmailServices emailServices)
        {
            _mapper = mapper;
            _authservices = authservices;
            _userManager = userManager;
            _fileServices = fileServices;
            _httpContext = httpContext;
            _OtpServices = otpServices;
            _emailServices = emailServices;
        }
        #endregion
        public async Task<Response<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            var auth = await _authservices.Register(user, request.Password);
            if (!auth.Success)
                return BadRequest<string>(auth.Message);
            return Success<string>(auth.Message);
        }

        public async Task<Response<AuthResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var email  =await _userManager.FindByEmailAsync(request.Email);
            if (email is null || !await _userManager.CheckPasswordAsync(email, request.Password))
                return BadRequest<AuthResult>("Incorrect Email Or Password ");
            if (!await _userManager.IsEmailConfirmedAsync(email))
                return UnAuthorize<AuthResult>(" Email Not Confieremd Please Try Again");

            var result = await _authservices.GetToken(email);
            return Created(result);

        }

        public async Task<Response<AuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var RefreshToken = await _authservices.RefreshToken(request.Token);
            if (RefreshToken.Message != null)
                return BadRequest<AuthResult>(RefreshToken.Message);
            return Success( RefreshToken);
            
        }

        public async Task<Response<string>> Handle(RevokedTokenCommand request, CancellationToken cancellationToken)
        {
            var token =await _authservices.RevokedToken(request.Token);
            if (!token)
                return BadRequest<string>("Invalid Token !");
            return Success("Revoked Token Successfully");

        }

        public async Task<Response<string>> Handle(EditProfileCommand request, CancellationToken cancellationToken)
        {
            var userid=GetUserId();
            var user = await _userManager.Users.FirstOrDefaultAsync(c => c.Id.ToString() == userid);
            if (user == null)
                return NotFound<string>("Invalid User!");
            var usermap = _mapper.Map(request,user);
            if (request.Image != null)
            {
                var path= await _fileServices.UploadImage(request.Image, "Images/Users");
                user.Image = path;
            }
           var isupdated = await _userManager.UpdateAsync(user);
            return isupdated.Succeeded ? Success("Update Your Frofile Successfuly") 
                : BadRequest<string>("Please Try Again! ");

        }

        public async Task<Response<string>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            //verfy email 
            var user =await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                return NotFound<string>("Email Not Found !");
            var otp = GenerateOtpCode();
            await _OtpServices.SaveOtp(request.Email, otp);
            var body =GenerateEmailBody(otp, user.UserName);
            var sendemail = new EmailDto
            {
                To = request.Email,
                Subject = "Forget Password ",
                Body =body 
            };
            await _emailServices.SendEmail(sendemail);
            return Success("Check Tour Email Verying Otp Code !");


        }

        public async  Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            //getotpcode 
            var otp = await  _OtpServices.GetOtp(request.Email);
            if (otp is null) 
                return BadRequest<string>(" Verification code has expired. Please request a new one");
            if (otp?.Trim().Replace("\"", "") != request.Code)
                return BadRequest<string>("IInvalid verification code. Please check and try again");
            //chech email and hashpassword 
            var email = await _userManager.FindByEmailAsync(request.Email);
            if (email is null)
                return NotFound<string>("Email Not Found !");
            var HashPassword = _userManager.PasswordHasher.HashPassword(email, request.NewPassword);
            email.PasswordHash = HashPassword;
            //change password 
            await _userManager.UpdateAsync(email);
            await _OtpServices.DeleteOtp(request.Email);
            return Success("Your password has been reset successfully. You can now log in with your new password.");
            
        }


        private string GetUserId()
        {
            ClaimsPrincipal user = _httpContext.HttpContext?.User;
            var userid =_userManager.GetUserId(user);
            return userid;
        }


        private string GenerateOtpCode()
        {
            var otp = new Random();
            string code = string.Empty;
            for (var r =0; r<6; r++)
            {
                code += otp.Next(1,10).ToString();
            }
            return code;
        }


        static string GenerateEmailBody(string resetCode, string name)
        {
            return $@"
        <html>
        <body>
            <h2>Password Reset Request</h2>
            <p>Dear {name},</p>
            <p>We received a request to reset your password. Please use the following code to reset your password:</p>
            <h3>{resetCode}</h3>
            <p>If you did not request a password reset, please ignore this email.</p>
            <p>Thank you,<br> Ahmed Khaled [Owner]</p>
        </body>
        </html>";
        }

        
    }
}
