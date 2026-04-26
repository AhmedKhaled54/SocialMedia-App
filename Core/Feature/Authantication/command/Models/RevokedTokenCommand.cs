using Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Authantication.command.Models
{
    public  class RevokedTokenCommand:IRequest<Response<string>>
    {
        
        public string Token { get; set; }

        public RevokedTokenCommand(string token)
        {
            Token = token;
        }
    }
}
