using Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Authorization.Command.Models
{
    public  class AddNewRoleCommand:IRequest<Response<string>>
    {
        public string RoleName { get; set; }

    }
}
