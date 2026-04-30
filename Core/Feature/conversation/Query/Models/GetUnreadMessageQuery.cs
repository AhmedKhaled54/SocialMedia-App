using Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.conversation.Query.Models
{
    public  class GetUnreadMessageQuery:IRequest<Response<int>>
    {
        public int SenderId { get; set; }

        public GetUnreadMessageQuery(int senderId)
        {
            SenderId = senderId;
        }
    }
}
