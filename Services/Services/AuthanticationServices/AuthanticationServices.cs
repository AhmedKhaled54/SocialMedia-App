using Data.Helper;
using Data.Identity;
using Infrastructure.Abstract;
using Infrastructure.Data;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Validations;
using Services.Services.EmailServices;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace Services.Services.AuthanticationServices
{
    public class AuthanticationServices : IAuthanticationServices
    {
        private readonly UserManager<User> userManager;
        private readonly IUnitOfWork _UOW;
        private readonly IHttpContextAccessor httpContext;
        private readonly IUrlHelper urlHelper;
        private readonly IEmailServices emailServices;
        private readonly AppDbContext context;
        private readonly JWT _jwt;

        public AuthanticationServices(UserManager<User> userManager,
            JWT jwt, 
            IUnitOfWork UOW ,
            IHttpContextAccessor httpContext,
            IUrlHelper urlHelper,
            IEmailServices emailServices,
            AppDbContext context
            )
        {
            this.userManager = userManager;
            _UOW = UOW;
            this.httpContext = httpContext;
            this.urlHelper = urlHelper;
            this.emailServices = emailServices;
            this.context = context;
            _jwt = jwt;//bind
        }
        public async Task<AuthResult> GetToken(User user)
        {
            var response = new AuthResult();
            var token = await GenerateJwtToken(user);
            var refreshtoken = GenerateRefreshToken(user.Id);

            var refresh = new RefreshToken
            {
                UserId = user.Id,
                Token = refreshtoken.Token,
                ExpireOn=refreshtoken.ExpireOn,
                CreatedAt=refreshtoken.CreatedAt
            };
            await _UOW.Repository<RefreshToken>().AddAsync(refresh);
            await _UOW.Complete();



            //user.RefreshTokens?.Add(refreshtoken);
            ////user.RefreshTokens?.Add(refreshtoken);
            //await userManager.UpdateAsync(user);
            ////await uOW.Repository<RefreshToken>().AddAsync(refreshtoken);
            ////await uOW.Complete();
            response.AccessToken = token;
            response.RefreshToken = refreshtoken.Token;
            response.RefreshTokenExpire = refreshtoken.ExpireOn;
            return response;




            //    if (user.RefreshTokens.Any(c=>c.IsActive))
            //    {
            //        var ActiveToken = user.RefreshTokens.Single(c => c.IsActive);
            //        response.RefreshToken = ActiveToken.Token;
            //        response.RefreshTokenExpire=ActiveToken.ExpireOn;

            //    }

            //        var refreshtoken = GenerateRefreshToken(user.Id);
            //        response.RefreshToken = refreshtoken.Token;
            //        response.RefreshTokenExpire = refreshtoken.ExpireOn;
            //        user.RefreshTokens?.Add(refreshtoken);
            //        await userManager.UpdateAsync(user);


            //    response.AccessToken = token;
            //    response.RefreshToken=refreshtoken.Token;
            //    response.RefreshTokenExpire=refreshtoken.ExpireOn;
            //    return response;
            //}
        }
        public async Task<AuthResult> RefreshToken(string token)
        {

            var user = await userManager.Users.SingleOrDefaultAsync(c => c.RefreshTokens.Any(c => c.Token == token));
            if (user == null)
                return new AuthResult { Message = "Invalid Token !" };


            var refreshtoken =await _UOW.Repository<RefreshToken>().FindAsync(c=>c.Token == token);
            if (!refreshtoken.IsActive)
                return new AuthResult { Message = "InActive Token !" };
            refreshtoken.RevokedOn= DateTime.Now;
            var NewRefreshToken =GenerateRefreshToken(refreshtoken.UserId);
            refreshtoken.Token = NewRefreshToken.Token;
            //user.RefreshTokens.Add(refreshtoken);
            await _UOW.Complete();

            var accesstoken = await GenerateJwtToken(user);
            return new AuthResult
            {
                AccessToken = accesstoken,
                RefreshToken = NewRefreshToken.Token,
                RefreshTokenExpire = refreshtoken.ExpireOn
            };


            //var refreshtoken =user.RefreshTokens.Single(c=>c.Token == token);
            //if (!refreshtoken.IsActive)
            //    return new AuthResult { Message = "INActive Token !" };

            //refreshtoken.RevokedOn=DateTime.Now;
            //var newrefreshToken =GenerateRefreshToken(user.Id);
            //user.RefreshTokens.Add(newrefreshToken);
            //await userManager.UpdateAsync(user);
            //var accesstoken = await GenerateJwtToken(user);
            //return new AuthResult
            //{
            //    AccessToken = accesstoken,
            //    RefreshToken = newrefreshToken.Token,
            //    RefreshTokenExpire = refreshtoken.ExpireOn
            //};


        }



        private RefreshToken GenerateRefreshToken(int userid)
        {
            var rndom =new byte[32];
            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(rndom);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(rndom),
                CreatedAt = DateTime.UtcNow,
                ExpireOn = DateTime.UtcNow.AddDays(7),
                UserId=userid
            };

        }


        private async Task<string> GenerateJwtToken(User user)
        {
            var Claims = await GetClaim(user);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(

                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: Claims,
                signingCredentials: SigningCredentials,
                expires: DateTime.Now.AddDays(_jwt.DurationInDayes

                ));
            var AccessToken =new JwtSecurityTokenHandler().WriteToken(jwt);
            return AccessToken;

        }



        private async Task<List<Claim>>GetClaim(User user)
        {
            var claims = new List<Claim>()
            {

                new Claim(ClaimTypes.Name,user.UserName!),
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };
            var roles = await userManager.GetRolesAsync(user);
            foreach(var itmerole in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, itmerole.ToString()));
            }

            return claims;
        }

        public async Task<ResponseDto> Register(User user, string password)
        {
           
            var trans = context.Database.BeginTransaction();
            try
            {
                if (await userManager.FindByEmailAsync(user.Email) != null)
                      return new ResponseDto { Message = " Email  Is Exsit!" };
                 
                if ( await userManager.FindByNameAsync(user.UserName) != null)
                    return new ResponseDto { Message = " Email  Is Exsit!" };
                var result = await userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    var error= string.Join(",", result.Errors.Select(c => c.Description));
                    return  new ResponseDto { Message = error}; ;
                }

                await userManager.AddToRoleAsync(user, "user");
                trans.CreateSavepoint("save and add to role");
                //confirm email 
                var Code  =await userManager.GenerateEmailConfirmationTokenAsync(user);
                var accessor = httpContext.HttpContext.Request;
                var Url = accessor.Scheme + "://" + accessor.Host +
                    urlHelper.Action("ConfirmEmail", "Authantication", new { userid = user.Id, code = Code});

                var message = $"To  Confirm Email  Click link :<a href= '{Url}'> Link Of Confirmation Email</a> ";

                var Email = new EmailDto
                {
                    To = user.Email,
                    Subject = "Confirm Email ",
                    Body = message
                };

                await emailServices.SendEmail(Email);
                await trans.CommitAsync();
                return new ResponseDto { Message = "Check Your Emial Please Confirmed", Success = true };
            }
            catch (Exception ex )
            {
                await trans.RollbackAsync();
                return new ResponseDto { Message = ex.Message,Success=false};
            }
           ;



        }

        public async  Task<ResponseDto> ConfirmEmail(int? userid, string? code)
        {
            if (userid == null || code == null)
                return new ResponseDto() { Message = "Error when confirm email !" };
            var user = await userManager.FindByIdAsync(userid.ToString());
            var confirmemail = await userManager.ConfirmEmailAsync(user, code);
            if (!confirmemail.Succeeded)
                return new ResponseDto() { Message = "Error when confirm email !" };
            return new ResponseDto
            {
                Success = true,
                Message = "Email confirm successfuly "
            };

        }
    }
}
