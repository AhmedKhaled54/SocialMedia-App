using Data.Entity.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specification.CommentsSpecification
{
    public class CommetWithmediaSepecification : BaseSepecification<Comment>
    {
        public CommetWithmediaSepecification(int commentid ) : base(c=>c.Id==commentid)
        {
            AddInclude(c=>c.Media);
        }
    }
}
