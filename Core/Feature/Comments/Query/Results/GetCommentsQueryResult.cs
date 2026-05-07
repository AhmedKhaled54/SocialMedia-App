using Data.Enums.Like;
using Data.Enums.Media;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Comments.Query.Results
{
    public class GetCommentsQueryResult
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public Dictionary<LikeType, int>? ReactsCount { get; set; }
        public Dictionary<LikeType, List<GetCommentUserQueryResult>>? ReactDetails { get; set; }
        public List<GetCommentMediaQueryResult>? Medias { get; set; } = new();

    }

    public class GetCommentUserQueryResult
    {
        public string UserName { get; set; }
        public int UserId { get; set; }
    }

    public class GetCommentMediaQueryResult
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public MedaiType Type { get; set; }
    }
}
