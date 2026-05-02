using Core.Bases;
using Core.Feature.Follow.Query.Results;
using Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Follow.Query.Models
{
    public  class GetAllFollowingQuery:IRequest<Response<Pagination<GetFollowersQueryResult>>>
    {
        public string?Search {  get; set; }
        public int PageNumber{  get; set; }
        public int PageSize {  get; set; } = 10;
    }
}
