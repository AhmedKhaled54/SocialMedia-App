using Data.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Authantication.command.Models
{
    public  class RefreshTokenCommand:IRequest<AuthResult>
    {
        public string Token { get; set; }
    }
}
