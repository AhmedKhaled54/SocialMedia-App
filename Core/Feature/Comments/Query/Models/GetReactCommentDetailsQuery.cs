using Core.Bases;
using Core.Feature.Comments.Query.Results;
using MediatR;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Comments.Query.Models
{
    public  class GetReactCommentDetailsQuery:IRequest<Response<GetReactCommentDetailsQueryResult>>
    {
        public int CommentId { get; set; }

        public GetReactCommentDetailsQuery(int commentId)
        {
            CommentId = commentId;
        }
    }
}
