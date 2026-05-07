using Core.Bases;
using Core.Feature.Comments.Query.Results;
using Core.Wrappers;
using MediatR;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Comments.Query.Models
{
    public  class GetCommentsQuery:IRequest<Response<Pagination<GetCommentsQueryResult>>>
    {
        public int PostId { get; set; }

        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
   
    }
}
