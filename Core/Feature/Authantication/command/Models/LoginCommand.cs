using Core.Bases;
using Data.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Authantication.command.Models
{
    public  class LoginCommand:IRequest<Response<AuthResult>>
    {
        public string Email {  get; set; }
        public string Password { get; set; }
    }
}
