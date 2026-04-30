using Core.Bases;
using Core.Feature.conversation.Command.Results;
using MediatR;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.conversation.Command.Models
{
    public  class EditMessageCommand:IRequest<Response<DirectMessageCommandResult>>
    {
        public int MessageId {  get; set; }
        public string Body { get; set; }
    }
}
