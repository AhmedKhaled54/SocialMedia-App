using Data.Entity.Comments;
using Data.Entity.Posts;
using Data.Enums.Like;
using Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.RactsServices
{
    public  interface IReactServices
    {
        Task<ReactDto> PostReactionAsync(int PostId, int UserId, LikeType type);
        Task<PostReactDto>GetPostsDetails(int PostId);

        Task<(bool notify,int ownerid)> CommentReactAsync(int commentid, int userid, LikeType type);
        Task<CommentReactDto> GetCommentDetails(int CommentId);

    }
}
