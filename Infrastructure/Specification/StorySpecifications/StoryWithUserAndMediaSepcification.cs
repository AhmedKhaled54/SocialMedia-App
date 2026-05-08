using Data.Entity.Stories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specification.StorySpecifications
{
    public class StoryWithUserAndMediaSepcification : BaseSepecification<Story>
    {
        public StoryWithUserAndMediaSepcification(int storyid ) : base(c=>c.Id==storyid)
        {
            AddInclude(c => c.User);
            AddInclude(c=>c.Media);
        }
    }
}
