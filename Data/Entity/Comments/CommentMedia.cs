using Data.Entity.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity.Comments
{
    public  class CommentMedia:BaseMedia
    {
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}
