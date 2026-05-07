using Infrastructure.Abstract;
using Infrastructure.Data;
using Infrastructure.Specification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAll()
            => await _context.Set<T>().ToListAsync();
        public async Task<T> GetById(int id)
            => await _context.Set<T>().FindAsync(id);
        public async Task AddAsync(T entity)
            =>await _context.Set<T>().AddAsync(entity);
        public void Update(T entity)
            =>_context.Set<T>().Update(entity);


        public void Delete(T entity)
            =>_context.Set<T>().Remove(entity);

        public async Task<T> FindAsync(Expression<Func<T, bool>> match)
            => await _context.Set<T>().FirstOrDefaultAsync(match);

       

        public IQueryable<T> GetAllpridicated(Expression<Func<T, bool>> match, string[] include = null)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();
            if (query == null)
                return null;
            if (include != null)
                foreach (var includes  in include)
                    query= query.Include(includes);

            return query.Where(match);
            
        }

        

        public T Getpridicated(Expression<Func<T, bool>> match, string[] include = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetTableAsNoTracking()
            => _context.Set<T>().AsNoTracking().AsQueryable();
        public IQueryable<T> GetTable(Expression<Func<T,bool>> match)
            => _context.Set<T>().Where(match).AsQueryable();
        public IDbContextTransaction BeginTrnaction()
            =>_context.Database.BeginTransaction();

        public async Task<bool> IsAny(Expression<Func<T, bool>> match)
            =>await _context.Set<T>().AnyAsync(match);

        public IQueryable<T> GetEntitiesWithSpecs(ISpecification<T> specification)
            =>ApplyQuery(specification);



        private IQueryable<T> ApplyQuery(ISpecification<T> specification)
            =>EvaluationSpecifications<T>.GetQuery(_context.Set<T>().AsQueryable(), specification);

        public void RemoveRange(List<T> entity)
            =>_context.Set<T>().RemoveRange(entity);

        public async Task<T> GetEntityByIdSepcs(ISpecification<T> specification)
            =>await ApplyQuery(specification).FirstOrDefaultAsync();
    }
}
