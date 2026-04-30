using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.conversation.Command.Results
{
    public  class DirectMessageCommandResult
    {
        public int MessageId { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead {  get; set; }
    }
}
