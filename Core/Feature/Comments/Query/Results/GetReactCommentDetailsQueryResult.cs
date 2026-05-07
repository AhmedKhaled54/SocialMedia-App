using Data.Enums.Like;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Comments.Query.Results
{
    public  class GetReactCommentDetailsQueryResult
    {
        public int CommentId { get; set; }
        public int TotalCountReacts { get; set; }
        public Dictionary<LikeType,int> ReactCount { get; set; }
        public Dictionary<LikeType,List<CommentReactUser>> ReactDetails  { get; set; }
    }

    public class CommentReactUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
