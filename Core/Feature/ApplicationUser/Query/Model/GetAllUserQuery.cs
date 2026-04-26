using Core.Bases;
using Core.Feature.ApplicationUser.Query.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.ApplicationUser.Query.Model
{
    public  class GetAllUserQuery:IRequest<Response<List<UserDetailsResult>>>
    {
        
    }
}
