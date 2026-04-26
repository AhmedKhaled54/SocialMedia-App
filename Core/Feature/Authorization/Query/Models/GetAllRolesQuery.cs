using Core.Bases;
using Core.Feature.Authorization.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Authorization.Query.Models
{
    public  class GetAllRolesQuery:IRequest<Response<IEnumerable<RoleDeatailsResult>>>
    {
    }
}
