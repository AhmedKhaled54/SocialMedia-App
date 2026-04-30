using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.conversation.Query.Results
{
    public  class GetConvesationQueryResult
    {
        public int MessageId { get; set; }
        public string Body {  get; set; }
        public DateTime Date { get; set; }
        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public string ReceiveiId { get; set; }
        public string ReceiveName {  get; set; }
        public bool IsRead {  get; set; }

    }
}
