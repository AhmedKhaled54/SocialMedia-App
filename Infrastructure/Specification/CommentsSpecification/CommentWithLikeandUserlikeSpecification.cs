using Data.Entity.Comments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specification.CommentsSpecification
{
    public class CommentWithLikeandUserlikeSpecification : BaseSepecification<Comment>
    {
        public CommentWithLikeandUserlikeSpecification(int PostId ) 
            : base(c=>c.PostId==PostId)
        {

            //AddInclude(c=>c.Likes);
            //AddInclude(c=>c.Likes.Select(c=>c.User));
            AddIncludeWithThen(c=>c.Include(c=>c.Likes).ThenInclude(c=>c.User));//using then include not expression linq 

          
        }
    }
}
