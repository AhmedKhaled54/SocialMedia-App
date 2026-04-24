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
        void DeleteAsync(T entity);

         IQueryable<T> GetTableAsNoTracking();
        Task<IEnumerable<T>> GetAllpridicated(Expression<Func<T, bool>> match, string[] include = null!);
        T Getpridicated(Expression<Func<T, bool>> match, string[] include = null!);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        IDbContextTransaction BeginTrnaction();

    }
}
