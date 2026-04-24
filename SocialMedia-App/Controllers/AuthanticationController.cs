using Core.Feature.Authantication.command.Models;
using Core.Feature.Authantication.Query.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SocialMedia_App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthanticationController : ControllerBase
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
            SetRefreshTokenInCookie(result.AccessToken,result.RefreshTokenExpire);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult>RefreshToken([FromBody] RefreshTokenCommand cmd)
        {
            var token = cmd.Token ?? Request.Cookies["RefreshToken"];
            var result = await mediator.Send(cmd);
            return Ok(result);
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
