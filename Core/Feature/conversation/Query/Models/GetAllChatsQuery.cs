using Core.Bases;
using Core.Feature.conversation.Query.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.conversation.Query.Models
{
    public  class GetAllChatsQuery:IRequest<Response<List<GetAllCahtsQueryResult>>>
    {
        public string? Search {  get; set; }
    }
}
