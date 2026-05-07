using Data.Entity;
using Data.Enums.Like;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Helper
{
    public  class  PostReactDto
    {
        public int PostId { get; set; }
        public int TotalCount { get; set; }
        public Dictionary<LikeType,int> CountType{ get; set; }
        public Dictionary<LikeType,List<ReactUserDto>> WhosReact { get; set; }
    }

    public class ReactUserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
