using Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Authantication.command.Models
{
    public  class ForgetPasswordCommand:IRequest<Response<string>>
    {
        public string Email {get; set;}

    }
}
