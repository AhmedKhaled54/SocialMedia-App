using Data.Enums.Like;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Posts.Query.Result
{
    public  class GetPostReactDetailQueryResult
    {
        public int PostId { get; set; }
        public int TotalCountReact { get; set; }
        public Dictionary <LikeType,int >ReactCount { get; set; }
        public Dictionary <LikeType,List<GetPostUserDetails>> ReactDetails { get; set; } 


    }

    public class GetPostUserDetails
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
