using Core.Bases;
using Core.Feature.conversation.Query.Results;
using Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.conversation.Query.Models
{
    public  class GetConversationQuery:IRequest<Response<Pagination<GetConvesationQueryResult>>>
    {
        //public int SenderId { get; set; } =>get Currentuser jwt 
        public int ReceiveId { get; set; }
        public int PageNumber {  get; set; }
        public int PageSize { get; set; } = 10;
        public string? Search {  get; set; }


    }
}
