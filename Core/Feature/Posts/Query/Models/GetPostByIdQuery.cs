using Core.Bases;
using Core.Feature.Posts.Query.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Posts.Query.Models
{
    public class GetPostByIdQuery:IRequest<Response<GetpostsFeedQueryResult>>
    {
        public int PostId { get; set; }

        public GetPostByIdQuery(int postId)
        {
            PostId = postId;
        }
    }
}
