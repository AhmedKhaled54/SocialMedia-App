using Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Authantication.command.Models
{
    public  class ResetPasswordCommand:IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public string NewPassword { get; set; }

    }
}
