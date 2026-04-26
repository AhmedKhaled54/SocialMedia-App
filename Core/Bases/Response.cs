using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bases
{
    public  class Response<T>
    {
        public Response()
        {
             
        }
        public Response(string message)
        {
            Message = message;
            Success = false;
        }
        public Response(T data ,string message =null )
        {
            Data = data;
            Message = message;
            Success = true;
             
        }



        public bool Success { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
        
    }
}
