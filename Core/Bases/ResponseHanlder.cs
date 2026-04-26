using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bases
{
    public  class ResponseHanlder
    {

        public ResponseHanlder()
        {
             
        }

        public Response<T> Success<T> (T entity ,string message = null)
        {
            return new Response<T>()
            {
                Data = entity ,
                StatusCode=HttpStatusCode.OK,
                Message=message==null ?"Successfuly":message,
                Success=true

            };
        }


        public Response<T> BadRequest<T> (string message=null)
        {
            return new Response<T>()
            {
                Message = message == null ? "BadRequest" : message,
                StatusCode = HttpStatusCode.BadRequest,
                Success = false
            };
        }

        public Response<T> NotFound<T>(string message =null)
        {
            return new Response<T>()
            {
                Message = message == null ? "Not Found" : message,
                Success = false,
                StatusCode = HttpStatusCode.NotFound
            };
        }

        public Response<T> Created <T>(T entity)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = HttpStatusCode.Created,
                Success = true,
                Message = "Created Successfuly"
            };
        }

        public Response<T> UnAuthorize <T> (string message =null)
        {
            return new Response<T>()
            {
                Message = message == null ? "UnAthorize" : message,
                Success = false,
                StatusCode = HttpStatusCode.Unauthorized
            };
        }



    }



}
