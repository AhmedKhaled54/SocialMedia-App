using Core.Bases;
using MediatR;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.conversation.Command.Models
{
    public  class MarkReadMessageCommand:IRequest<Response<bool>>
    {
        public int SenderId { get; set; }
    }
}
