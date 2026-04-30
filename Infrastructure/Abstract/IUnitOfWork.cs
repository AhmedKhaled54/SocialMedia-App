using Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Abstract
{
    public interface IUnitOfWork: IDisposable
    {
        Task<int> Complete();
        public IGenericRepository<T> Repository<T>()where T : class;
        Task<IDbContextTransaction> BeginTransactionAsync();
        AppDbContext Context { get; }

    }
}
