using Core.Bases;
using Data.Enums.Authantication;
using Data.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Authantication.command.Models
{
    public  class RegisterCommand:IRequest<Response<string>>
    {

        public string UserName { get; set; }
        public string Email { get; set; }
        public DateOnly BirthDate { get; set; } 
        public Gender Gender { get; set; }
        public string Password { get; set; }
        [Compare(nameof(Password),ErrorMessage ="Password Not Matching !")]
        public string ConfirmPassword { get; set; }


    }
}
