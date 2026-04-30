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
    public  class DirectMessageCommand:IRequest<Response<DirectMessageCommandResult>>
    {
        public int SnderId { get; set; }
        public int RecivedId { get; set; }
        public string Message { get; set; }


    }
}
