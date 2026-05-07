using Data.Enums.Like;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Helper
{
    public  class CommentReactDto
    {

        public int CommentId { get; set; }
        public int TotalCount { get; set; }
        public Dictionary<LikeType,int> ReactsCount { get; set; }
        public Dictionary<LikeType,List<ReactUserDto>> WhosReacts {  get; set; }
    }

    
}
