using Infrastructure.Specification;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Abstract
{
    public  interface IGenericRepository<T> where T :class 
    {
        Task<IEnumerable<T>>GetAll();
        Task<T> GetById (int id);
        Task AddAsync(T entity);
        void  Update(T entity);
        void Delete(T entity);

         IQueryable<T> GetTableAsNoTracking();
         IQueryable<T> GetTable(Expression<Func<T, bool>> match);
        IQueryable<T> GetAllpridicated(Expression<Func<T, bool>> match, string[] include = null!);
        T Getpridicated(Expression<Func<T, bool>> match, string[] include = null!);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        IDbContextTransaction BeginTrnaction();
        Task<bool>IsAny(Expression<Func<T, bool>> match);

        //sepecification pattern 
        IQueryable<T> GetEntitiesWithSpecs(ISpecification<T> specification);
        Task<T> GetEntityByIdSepcs(ISpecification<T> specification);
        void RemoveRange (List<T> entity);

    }
}
