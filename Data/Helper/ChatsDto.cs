using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Helper
{
    public  class ChatsDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string? LastMessage { get; set; }
        public DateTime LastMessageDate { get; set; }
        public int UnReadMessage { get; set; }
    }
}
