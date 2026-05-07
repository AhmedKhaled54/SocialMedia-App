using Core.Bases;
using Core.Feature.Posts.Query.Result;
using Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Posts.Query.Models
{
    public class GetPostsFeedQuery : IRequest<Response<Pagination<GetpostsFeedQueryResult>>>
    {
        public string ?Search { get; set; }
        public int Pagesize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;

    }
}
