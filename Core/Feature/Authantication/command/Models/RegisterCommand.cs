using Data.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Authantication.command.Models
{
    public  class RegisterCommand:IRequest<string>
    {

        public string UserName { get; set; }
        public string Email { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }


    }
}
