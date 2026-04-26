using Core.Feature.Authantication.command.Models;
using Core.Feature.Authantication.Query.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg.Sig;

namespace SocialMedia_App.Controllers
{
   
    public class AuthanticationController : BaseController
    {
        private readonly IMediator mediator;

        public AuthanticationController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult>Register(RegisterCommand cmd)
        {
            var result =await mediator.Send(cmd);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult>SignIn(LoginCommand cmd) 
        {
            var result = await mediator.Send(cmd);
            SetRefreshTokenInCookie(result.Data.AccessToken,result.Data.RefreshTokenExpire);
            return NewResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery query)
        {
            var result = await mediator.Send(query);
            return NewResult (result);
        }


        [HttpPut]
        public async Task<IActionResult> EditProfile([FromForm] EditProfileCommand cmd)
        {
            var result = await mediator.Send(cmd);
            return NewResult(result);
        }

        [HttpPost]
        public async Task<IActionResult>ForgetPassword(ForgetPasswordCommand cmd)
        {
            var result =await mediator.Send(cmd);
            return NewResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword (ResetPasswordCommand cmd)
        {
            var result =await mediator.Send(cmd);
            return NewResult(result);
        }


        [HttpPost]
        public async Task<IActionResult>RefreshToken([FromBody] RefreshTokenCommand cmd)
        {
            var token = cmd.Token ?? Request.Cookies["RefreshToken"];
            var result = await mediator.Send(cmd);
            return NewResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> RevokedToken()
        {
            var token = Request.Cookies["RefreshToken"];
            var cmd =new RevokedTokenCommand(token);
            var result =await mediator.Send(cmd);
            return NewResult(result);
        }


        private void  SetRefreshTokenInCookie(string token ,DateTime expire )
        {
            var cookieoption = new CookieOptions
            {
                HttpOnly = true,
                Expires = expire.ToLocalTime()

            };
            Response.Cookies.Append("RefreshToken",token,cookieoption);
        }

    }
}
