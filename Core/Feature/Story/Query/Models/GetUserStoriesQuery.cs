using Core.Bases;
using Core.Feature.Story.Query.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Story.Query.Models
{
    public  class GetUserStoriesQuery:IRequest<Response<GetUserStoriesQueryResult>>
    {
        public int userid { get; set; }

    }
}
