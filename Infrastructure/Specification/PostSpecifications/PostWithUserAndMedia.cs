using Data.Entity.Posts;
using Data.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specification.PostSpecifications
{
    public class PostWithUserAndMedia : BaseSepecification<Post>
    {
        public PostWithUserAndMedia(PostSpecification spec)
        : base(p =>
            (spec.FollowingId.Contains(p.UserId) || p.UserId == spec.userid) &&
        (!spec.PostMedia.HasValue || p.Media.Any(m => (int)m.Type == spec.PostMedia.Value) &&
        (string.IsNullOrEmpty(spec.Search)||p.Caption.Trim().ToLower().Contains(spec.Search)))
        )
        {
            AddInclude(p => p.Media);
            AddInclude(p => p.user);
            AddInclude(c => c.Comments);

            // optional filter (image / video)
            if (spec.PostMedia.HasValue)
            {
                AddInclude(p => p.Media.Where(m => (int)m.Type == spec.PostMedia));
            }




        }


        public PostWithUserAndMedia(int postid) : base(c => c.Id == postid)
        {
            AddInclude(c => c.Media);
            AddInclude(p => p.user);
            AddInclude(c => c.Comments);
        }
    }
}
