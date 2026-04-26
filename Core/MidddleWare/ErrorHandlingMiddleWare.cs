using Core.Bases;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.MidddleWare
{
    public  class ErrorHandlingMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleWare> _logger;
        private readonly IWebHostEnvironment webHost;

        public ErrorHandlingMiddleWare(RequestDelegate next, ILogger<ErrorHandlingMiddleWare> logger, IWebHostEnvironment webHost)
        {
            _next = next;
            _logger = logger;
            this.webHost = webHost;
        }

        public async Task Invoke (HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (Exception error )
            {
                _logger.LogError(error.Message);
                var response = context.Response;
                response.ContentType = "application/json";
                var responsemodel=new Response<string>() { Success=false,Message=error.Message};


                var (code, message) = error switch
                {
                    ValidationException e => (HttpStatusCode.UnprocessableEntity, GetMessage(e)),
                    KeyNotFoundException e => (HttpStatusCode.NotFound, GetMessage(e)),
                    UnauthorizedAccessException e => (HttpStatusCode.Unauthorized, GetMessage(e)),
                    DbUpdateException e => (HttpStatusCode.Unauthorized, GetMessage(e)),
                    Exception e => (HttpStatusCode.BadRequest,GetMessage(e)),

                    _ => (HttpStatusCode.InternalServerError, GetMessage(error))

                };

                response.StatusCode = (int)code;
                responsemodel.Message= message;
                responsemodel.StatusCode = code;

                var result =JsonSerializer.Serialize(responsemodel);
                await response.WriteAsync(result);
            }


            

        }

        private string GetMessage (Exception ex)
        {
            //Develop 
            if (webHost.IsDevelopment())
            {
                return ex.Message + (ex.InnerException != null ? "\n" + ex.InnerException.Message : "")
                    + ex.StackTrace;//=>details 
            }
            //production 
            return ex switch
            {
                ValidationException e => e.Message,

                Exception e => e.Message,

                _=>"Internal Server error"

            };



        }



       
    }
}
