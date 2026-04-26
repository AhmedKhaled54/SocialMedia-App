using Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.CompilerServices;

namespace SocialMedia_App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private IMediator mediator;
        protected IMediator _Mediator =>mediator??HttpContext.RequestServices.GetRequiredService<IMediator>();

        protected ObjectResult NewResult<T>(Response<T> response)
            => response.StatusCode switch
            {
                HttpStatusCode.OK => new OkObjectResult(response),
                HttpStatusCode.Unauthorized => new UnauthorizedObjectResult(response),
                HttpStatusCode.Created => new CreatedResult(string.Empty, response),
                HttpStatusCode.NotFound => new NotFoundObjectResult(response),
                HttpStatusCode.BadRequest => new BadRequestObjectResult(response),
                _ => new BadRequestObjectResult(response)
            };




    }
}
