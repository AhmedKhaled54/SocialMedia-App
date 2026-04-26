using Core.Bases;
using Core.Feature.Authorization.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Authorization.Command.Models
{
    public  class EditRoleCommand:IRequest<Response<string>>
    {
        public int RoleID   { get; set; }
        public string RoleName { get; set; }
    }
}
