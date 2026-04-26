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
    public  class GetUserByIdQuery:IRequest<Response<UserDetailsResult>>
    {
        public int UserID { get; set; }

        public GetUserByIdQuery(int userID)
        {
            UserID = userID;
        }
    }
}
