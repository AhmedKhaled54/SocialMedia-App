using Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.conversation.Command.Models
{
    public  class DeleteMessageCommand:IRequest<Response<bool>>
    {
        public int MessageId { get; set; }
    }
}
