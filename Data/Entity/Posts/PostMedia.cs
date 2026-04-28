using Data.Entity.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity.Posts
{
    public  class PostMedia:BaseMedia
    {
        public int PostId { get; set; }
        public Post Post { get; set; }  
    }
}
