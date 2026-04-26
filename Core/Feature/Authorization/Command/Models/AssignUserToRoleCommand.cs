using Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Authorization.Command.Models
{
    public  class AssignUserToRoleCommand:IRequest<Response<string>>
    {
        public int UserId { get; set; }
        public string RoleName { get; set; }
    }
}
