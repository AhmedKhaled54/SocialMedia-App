using Microsoft.AspNetCore.Connections.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Infrastructure.Specification
{
    public class BaseSepecification<T> : ISpecification<T> where T : class
    {
        public BaseSepecification(Expression<Func<T, bool>> creiteria)
        {
            Creiteria = creiteria;
        }
        public Expression<Func<T, bool>> Creiteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public List<Func<IQueryable<T>, IQueryable<T>>> InculdesWithThen { get; set; } = new List<Func<IQueryable<T>, IQueryable<T>>>();

        protected void AddInclude(Expression<Func<T, object>> include)
            =>Includes.Add(include);

        protected void AddOrderBy (Expression<Func<T, object>> orderby)
            =>OrderBy=orderby;
        protected void AddOrderByDesc (Expression<Func<T, object>> orderbydesc)
            =>OrderByDescending=orderbydesc;

        protected void AddIncludeWithThen(Func<IQueryable<T>, IQueryable<T>> include)
            =>InculdesWithThen.Add(include);

    }
}
