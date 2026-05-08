using Data.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specification.UsersSpecifications
{
    public class UserWithStorySpecification : BaseSepecification<User>
    {
        public UserWithStorySpecification(int userid) : base(c => c.Id == userid)
        {
            AddIncludeWithThen(c => c.Include(c => c.Stories).ThenInclude(c => c.Media));
        }


        public UserWithStorySpecification(int userid, List<int> followingIds) : base(c => followingIds.Contains(c.Id))
        {
            AddIncludeWithThen(c => c.Include(c => c.Stories).ThenInclude(c => c.Media));
            AddOrderByDesc(c => c.Stories.Max(s => s.CreatedAt));
        }
    }
}
