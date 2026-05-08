using Core.Bases;
using Core.Feature.Story.Query.Results;
using MediatR;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Story.Query.Models
{
    public  class GetFollowingStoryQuery:IRequest<Response<List<GetUserStoriesQueryResult>>>
    {
        public int userid { get; set; }
    }
}
