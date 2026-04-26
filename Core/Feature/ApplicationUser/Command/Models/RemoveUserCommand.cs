using Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.ApplicationUser.Command.Models
{
    public  class RemoveUserCommand:IRequest<Response<string>>
    {
        public int UserId { get; set; }
        public RemoveUserCommand(int userId)
        {
            UserId = userId;
        }
    }
}
