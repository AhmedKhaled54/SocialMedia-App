using Core.Bases;
using Core.Feature.Posts.Query.Result;
using MediatR;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Posts.Query.Models
{
    public  class GetDetailsReactPostQuery:IRequest<Response<GetPostReactDetailQueryResult>>
    {
        public int PostId { get; set; }

        public GetDetailsReactPostQuery(int postId)
        {
            PostId = postId;
        }
    }
}
